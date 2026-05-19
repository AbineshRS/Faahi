using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.am_users;
using Faahi.Dto.mk_blacklisted;
using Faahi.Dto.om_Orders;
using Faahi.Dto.om_Orders;
using Faahi.Migrations;
using Faahi.Model.am_users;
using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.im_products;
using Faahi.Model.Order;
using Faahi.Model.pos_tables;
using Faahi.Model.sales;
using Faahi.Model.Shared_tables;
using Faahi.Model.site_settings;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Service.Email;
using Faahi.Service.im_products.sales;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace Faahi.Service.market_place
{
    public class Market_place_service : IMarket_place_service
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Market_place_service> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;
        private readonly Isales _salesService;
        public Market_place_service(
            ApplicationDbContext context,
            ILogger<Market_place_service> logger,
            IConfiguration configuration,
            IAmazonS3 s3Client,
            Isales salesService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _salesService = salesService;
        } 


        public async Task<ServiceResult<am_users>> Add_users(am_users users)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                bool isNewUser = false;
                var genrate_password = "";
                var existingUser = await _context.am_users.Include(u => u.am_roles).ThenInclude(r => r.am_user_roles)
                                   .FirstOrDefaultAsync(u => u.email == users.email);
                if (existingUser == null)
                {
                    isNewUser = true;
                    users.userId = Guid.CreateVersion7();
                    users.userName = users.email;
                    users.firstName = users.firstName;
                    users.lastName = users.lastName;
                    users.fullName = users.firstName + " " + users.lastName;
                    genrate_password = GeneratePassword(users.fullName);
                    users.password = BCrypt.Net.BCrypt.HashPassword(genrate_password);
                    users.email = users.email;
                    users.created_at = DateTime.Now;
                    users.edit_date_time = DateTime.Now;
                    users.phoneNumber = users.phoneNumber;
                    users.status = "T";
                    foreach (var role in users.am_roles)
                    {
                        role.role_id = Guid.CreateVersion7();
                        role.user_ids = users.userId;
                        role.role_code = "MK";
                        role.role_group = "MK";
                        role.role_name = "Marketplace User";
                        role.description = "Marketplace user with access to marketplace features";
                        role.is_system_role = "F";
                        role.status = "T";

                        foreach (var userRole in role.am_user_roles)
                        {
                            var co_business = await _context.co_business.FirstOrDefaultAsync(b => b.company_id == userRole.business_id);
                            st_stores? st_store = null;
                            if (co_business == null)
                            {
                                st_store = await _context.st_stores.FirstOrDefaultAsync(s => s.store_id == userRole.store_id);
                            }
                            userRole.user_role_id = Guid.CreateVersion7();
                            userRole.user_id = users.userId;
                            userRole.role_id = role.role_id;
                            userRole.store_user_id = null;
                            userRole.created_at = DateTime.Now;
                            userRole.business_id = co_business?.company_id ?? st_store?.company_id;
                            userRole.store_id = st_store?.store_id;
                            //foreach (var mk_profile in userRole.mk_customer_profiles)
                            //{
                            //    mk_profile.customer_profile_id = Guid.CreateVersion7();
                            //    mk_profile.user_role_id = userRole.user_role_id;
                            //    mk_profile.user_id = users.userId;
                            //    mk_profile.customer_code = "";
                            //    mk_profile.gender = mk_profile.gender;
                            //    mk_profile.date_of_birth = mk_profile.date_of_birth;
                            //    mk_profile.created_at = DateTime.Now;
                            //    mk_profile.updated_at = DateTime.Now;
                            //    mk_profile.status = "T";

                            //    foreach (var address in mk_profile.mk_customer_addresses)
                            //    {
                            //        address.address_id = Guid.CreateVersion7();
                            //        address.customer_profile_id = mk_profile.customer_profile_id;
                            //        address.user_id = users.userId;
                            //        address.address_type = address.address_type;
                            //        address.contact_name = address.contact_name;
                            //        address.contact_phone = address.contact_phone;
                            //        address.address_line1 = address.address_line1;
                            //        address.address_line2 = address.address_line2;
                            //        address.city = address.city;
                            //        address.state_region = address.state_region;
                            //        address.postal_code = address.postal_code;
                            //        address.country_code = address.country_code;
                            //        address.created_at = DateTime.Now;
                            //        address.updated_at = DateTime.Now;
                            //        address.is_default = "T";
                            //        address.status = "T";
                            //    }
                            //}
                        }
                    }
                    _context.am_users.Add(users);
                }
                else
                {
                    var existingRoles = existingUser.am_roles.Where(r => r.role_code == "MK" && r.user_ids == existingUser.userId).FirstOrDefault();
                    if (existingRoles != null)
                    {
                        return new ServiceResult<am_users>
                        {
                            Data = null,
                            Message = "User with the same email already exists with marketplace role",
                            Status = 409
                        };
                    }




                    foreach (var role in users.am_roles)
                    {
                        role.role_id = Guid.CreateVersion7();
                        role.user_ids = existingUser.userId;
                        role.role_code = "MK";
                        role.role_group = "MK";
                        role.role_name = "Marketplace User";
                        role.description = "Marketplace user with access to marketplace features";
                        role.is_system_role = "F";
                        role.status = "T";

                        foreach (var userRole in role.am_user_roles)
                        {
                            var co_business = await _context.co_business.FirstOrDefaultAsync(b => b.company_id == userRole.business_id);
                            st_stores? st_store = null;
                            if (co_business == null)
                            {
                                st_store = await _context.st_stores.FirstOrDefaultAsync(s => s.store_id == userRole.store_id);
                            }
                            userRole.user_role_id = Guid.CreateVersion7();
                            userRole.user_id = existingUser.userId;
                            userRole.role_id = role.role_id;
                            userRole.store_user_id = null;
                            userRole.created_at = DateTime.Now;
                            userRole.business_id = co_business?.company_id ?? st_store?.company_id;
                            userRole.store_id = st_store?.store_id;

                            //foreach (var mk_profile in userRole.mk_customer_profiles)
                            //{
                            //    var table = "mk_customer_profiles";
                            //    var am_table = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == userRole.business_id);
                            //    var key = Convert.ToInt16(am_table.next_key);

                            //    mk_profile.customer_profile_id = Guid.CreateVersion7();
                            //    mk_profile.user_role_id = userRole.user_role_id;
                            //    mk_profile.user_id = existingUser.userId;
                            //    mk_profile.customer_code = "MK" + "-" + Convert.ToString(key + 1);
                            //    mk_profile.gender = mk_profile.gender;
                            //    mk_profile.date_of_birth = mk_profile.date_of_birth;
                            //    mk_profile.created_at = DateTime.Now;
                            //    mk_profile.updated_at = DateTime.Now;
                            //    mk_profile.status = "T";

                            //    foreach (var address in mk_profile.mk_customer_addresses)
                            //    {
                            //        address.address_id = Guid.CreateVersion7();
                            //        address.customer_profile_id = mk_profile.customer_profile_id;
                            //        address.user_id = existingUser.userId;
                            //        address.address_type = address.address_type;
                            //        address.contact_name = address.contact_name;
                            //        address.contact_phone = address.contact_phone;
                            //        address.address_line1 = address.address_line1;
                            //        address.address_line2 = address.address_line2;
                            //        address.city = address.city;
                            //        address.state_region = address.state_region;
                            //        address.postal_code = address.postal_code;
                            //        address.country_code = address.country_code;
                            //        address.created_at = DateTime.Now;
                            //        address.updated_at = DateTime.Now;
                            //        address.is_default = "T";
                            //        address.status = "T";


                            //    }
                            //    if (am_table != null)
                            //    {
                            //        am_table.next_key = key + 1;
                            //        _context.am_table_next_key.Update(am_table);
                            //    }
                            //}
                        }
                        _context.am_roles.Add(role);
                        existingUser.am_roles.Add(role);
                    }
                    _context.am_users.Update(existingUser);
                }
                if (isNewUser)
                {
                    string baseUrl = _configuration["MailSettings:BaseUrl"];
                    string subject = $"Welcome to the Marketplace ✨";

                    string body = $@"
                                    <!DOCTYPE html>
                                    <html>
                                    <head>
                                        <meta charset='UTF-8'>
                                        <style>
                                            body {{
                                                margin: 0; padding: 0; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f0f2f5;
                                            }}
                                            .card {{
                                                background: #ffffff; 
                                                border-radius: 12px; 
                                                max-width: 600px; 
                                                margin: 40px auto; 
                                                overflow: hidden; 
                                                box-shadow: 0 8px 25px rgba(0, 0, 0, 0.1);
                                            }}
                                            .header {{
                                                background: linear-gradient(90deg, #6a11cb 0%, #2575fc 100%);
                                                color: #fff; 
                                                padding: 30px; 
                                                text-align: center;
                                            }}
                                            .header h1 {{
                                                margin: 0;
                                                font-size: 28px;
                                            }}
                                            .header p {{
                                                margin: 5px 0 0;
                                                font-size: 16px;
                                            }}
                                            .content {{
                                                padding: 30px; 
                                                color: #333;
                                            }}
                                            .content p {{
                                                font-size: 16px; 
                                                line-height: 1.6;
                                                margin: 15px 0;
                                            }}
                                            .info-box {{
                                                background: #f9fafb; 
                                                border: 1px solid #eee; 
                                                padding: 15px; 
                                                border-radius: 6px; 
                                                margin: 20px 0;
                                            }}
                                            .info-box p {{
                                                margin: 5px 0;
                                                font-size: 14px;
                                            }}
                                            .btn {{
                                                display: inline-block; 
                                                padding: 14px 28px; 
                                                margin: 20px 0; 
                                                background: #7b3fe4; 
                                                color: #fff; 
                                                text-decoration: none; 
                                                border-radius: 8px; 
                                                font-size: 16px;
                                                transition: all 0.3s ease;
                                            }}
                                            .btn:hover {{
                                                background: #5a2db8;
                                            }}
                                            .footer {{
                                                text-align: center; 
                                                padding: 20px; 
                                                font-size: 12px; 
                                                color: #999;
                                                background: #f0f2f5;
                                            }}
                                        </style>
                                    </head>
                                    <body>
                                        <div class='card'>
                                            <div class='header'>
                                                <h1>Welcome to the Marketplace</h1>
                                                <p>Your premium account is ready ✨</p>
                                            </div>
                                            <div class='content'>
                                                <p>Hello <strong>{users.fullName}</strong>,</p>
                                                <p>Your marketplace account has been successfully created. You can now start exploring and enjoying all the features we offer.</p>

                                                <!-- Login Info -->
                                                <div class='info-box'>
                                                    <p><strong>Email:</strong> {users.email}</p>
                                                    <p><strong>Password:</strong> {genrate_password}</p>
                                                </div>

                                                <!-- Login Button -->
                                                <p style='text-align:center;'>
                                                    <a href='{baseUrl}' class='btn'>Login Now</a>
                                                </p>

                                                <p>We recommend changing your password after your first login for security purposes.</p>
                                            </div>
                                            <div class='footer'>
                                                © {DateTime.UtcNow.Year} Marketplace. All rights reserved.
                                            </div>
                                        </div>
                                    </body>
                                    </html>
                                    ";

                    var emailService = new EmailService(_configuration);
                    await emailService.SendEmailAsync(users.email, subject, body);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<am_users>
                {
                    Data = users,
                    Message = "User added successfully",
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error adding user: {Message}", ex.Message);
                await transaction.RollbackAsync();
                return new ServiceResult<am_users>
                {
                    Data = null,
                    Message = $"Error adding user: {ex.Message}",
                    Status = 500
                };
            }
        }

        public string GeneratePassword(string input)
        {

            string prefix = new string(input.Where(char.IsLetter).Take(3).ToArray()).ToLower();

            if (prefix.Length < 3)
                prefix = prefix.PadRight(3, 'x');

            // Random generators
            var random = new Random();

            string numbers = random.Next(100, 999).ToString(); // 3-digit number

            string specialChars = "!@#$%^&*";
            char special = specialChars[random.Next(specialChars.Length)];
            string final = prefix + numbers + special;

            return final;
        }

        public async Task<ServiceResult<om_OrderSources>> Add_source(om_OrderSources om_OrderSources)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (om_OrderSources == null)
                {
                    return new ServiceResult<om_OrderSources>
                    {
                        Data = null,
                        Message = "No data found",
                        Status = 400
                    };
                }
                var table = "om_OrderSources";
                var am_table = await _context.am_table_next_key.FirstOrDefaultAsync(a => a.name == table && a.business_id == om_OrderSources.business_id);
                var key = Convert.ToInt16(am_table.next_key);
                var existingSource = await _context.om_OrderSources.FirstOrDefaultAsync(s => s.source_name == om_OrderSources.source_name && s.business_id == om_OrderSources.business_id);
                if (existingSource != null)
                {
                    return new ServiceResult<om_OrderSources>
                    {
                        Data = null,
                        Message = "Order source with the same name already exists for this business",
                        Status = 409
                    };
                }
                om_OrderSources.source_id = Guid.CreateVersion7();
                om_OrderSources.business_id = om_OrderSources.business_id;
                om_OrderSources.store_id = om_OrderSources.store_id;
                om_OrderSources.source_code = "S" + "-" + Convert.ToString(key + 1);
                om_OrderSources.source_name = om_OrderSources.source_name;
                om_OrderSources.platform_name = om_OrderSources.platform_name;
                om_OrderSources.description = om_OrderSources.description;
                om_OrderSources.created_at = DateTime.Now;
                om_OrderSources.updated_at = DateTime.Now;
                om_OrderSources.status = "T";
                _context.om_OrderSources.Add(om_OrderSources);
                am_table.next_key = key + 1;
                _context.am_table_next_key.Update(am_table);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<om_OrderSources>
                {
                    Data = om_OrderSources,
                    Message = "Order source added successfully",
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogInformation("Error adding order source: {Message}", ex.Message);
                return new ServiceResult<om_OrderSources>
                {
                    Data = null,
                    Message = $"Error adding order source: {ex.Message}",
                    Status = 500
                };
            }

        }

        public async Task<ServiceResult<List<am_users_dto>>> Get_market_place_users(string search_text)
        {
            try
            {
                //var users = await _context.am_users.Where(a => a.status == "T").ToListAsync();
                var rows = await _context.Database
                             .SqlQueryRaw<string>(
                                 "EXEC dbo.sp_get_am_users @opr=@opr, @search_text=@search_text",
                                 new SqlParameter("@opr", 1),
                                 new SqlParameter("@search_text", search_text)
                             )
                             .ToListAsync();

                var jsonresult = rows.FirstOrDefault();
                if (jsonresult == null)
                {
                    return new ServiceResult<List<am_users_dto>>
                    {
                        Success = false,
                        Status = 300,
                        Message = "No users found",
                    };
                }
                var customer = JsonConvert.DeserializeObject<List<am_users_dto>>(jsonresult);
                return new ServiceResult<List<am_users_dto>>
                {
                    Data = customer,
                    Message = "Marketplace users retrieved successfully",
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error retrieving marketplace users: {Message}", ex.Message);
                return new ServiceResult<List<am_users_dto>>
                {
                    Data = null,
                    Message = $"Error retrieving marketplace users: {ex.Message}",
                    Status = 500
                };
            }
        }

        public async Task<ServiceResult<List<om_OrderSources>>> Get_sources(Guid business_id)
        {
            try
            {
                var sources = await _context.om_OrderSources.Where(s => s.business_id == business_id && s.status == "T").ToListAsync();
                if (sources == null || sources.Count == 0)
                {
                    return new ServiceResult<List<om_OrderSources>>
                    {
                        Data = null,
                        Message = "No order sources found for the given business and store",
                        Status = 404
                    };
                }
                return new ServiceResult<List<om_OrderSources>>
                {
                    Data = sources,
                    Message = "Order sources retrieved successfully",
                    Status = 200
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error retrieving order sources: {Message}", ex.Message);
                return new ServiceResult<List<om_OrderSources>>
                {
                    Data = null,
                    Message = $"Error retrieving order sources: {ex.Message}",
                    Status = 500
                };
            }
        }

        public async Task<ServiceResult<mk_customer_addresses>> Add_shipping_address(mk_customer_addresses mk_Customer_Addresses)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (mk_Customer_Addresses == null)
                {
                    return new ServiceResult<mk_customer_addresses>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var exising_address = await _context.mk_customer_addresses.FirstOrDefaultAsync(a => a.contact_phone == mk_Customer_Addresses.contact_phone);
                if (exising_address != null)
                {
                    return new ServiceResult<mk_customer_addresses>
                    {
                        Status = 300,
                        Success = false,
                        Message = "Already exist"
                    };
                }
                mk_Customer_Addresses.address_id = Guid.CreateVersion7();
                mk_Customer_Addresses.address_type = mk_Customer_Addresses.address_type;
                mk_Customer_Addresses.zone_id = mk_Customer_Addresses.zone_id;
                mk_Customer_Addresses.contact_name = mk_Customer_Addresses.contact_name;
                mk_Customer_Addresses.contact_phone = mk_Customer_Addresses.contact_phone;
                mk_Customer_Addresses.address_line1 = mk_Customer_Addresses.address_line1;
                mk_Customer_Addresses.Land_mark = mk_Customer_Addresses.Land_mark;
                mk_Customer_Addresses.city = mk_Customer_Addresses.city;
                mk_Customer_Addresses.state_region = mk_Customer_Addresses.state_region;
                mk_Customer_Addresses.postal_code = mk_Customer_Addresses.postal_code;
                mk_Customer_Addresses.country_code = mk_Customer_Addresses.country_code;
                mk_Customer_Addresses.delevery_start_time = mk_Customer_Addresses.delevery_start_time;
                mk_Customer_Addresses.delevery_end_time = mk_Customer_Addresses.delevery_end_time;
                mk_Customer_Addresses.latitude = mk_Customer_Addresses.latitude;
                mk_Customer_Addresses.longitude = mk_Customer_Addresses.longitude;
                mk_Customer_Addresses.created_at = mk_Customer_Addresses.created_at;
                mk_Customer_Addresses.updated_at = mk_Customer_Addresses.updated_at;
                mk_Customer_Addresses.is_default = "T";
                mk_Customer_Addresses.status = "T";
                _context.mk_customer_addresses.Add(mk_Customer_Addresses);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<mk_customer_addresses>
                {
                    Status = 200,
                    Success = true,
                    Message = "Inserted",
                    Data = mk_Customer_Addresses
                };


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ServiceResult<mk_customer_addresses>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<mk_customer_addresses>>> Get_shipping(string search_text)
        {
            try
            {
                var jsonresult = _context.Database.SqlQueryRaw<string>(
                   "EXEC dbo.sp_get_am_users @search_text=@search_text,@opr=@opr",
                   new SqlParameter("@search_text", search_text),
                   new SqlParameter("@opr", 1)).AsEnumerable().FirstOrDefault();
                if (jsonresult == null)
                {
                    return new ServiceResult<List<mk_customer_addresses>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var mk_customer = JsonConvert.DeserializeObject<List<mk_customer_addresses>>(jsonresult);

                if (mk_customer == null)
                {
                    return new ServiceResult<List<mk_customer_addresses>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }

                return new ServiceResult<List<mk_customer_addresses>>
                {
                    Success = true,
                    Status = 200,
                    Message = "Shipping addresses retrieved successfully",
                    Data = mk_customer
                };


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving shipping addresses");
                return new ServiceResult<List<mk_customer_addresses>>
                {
                    Success = false,
                    Status = 500,
                    Message = $"Error retrieving shipping addresses: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ServiceResult<mk_business_zones>> Add_Zones(mk_business_zones mk_Business_Zones)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (mk_Business_Zones == null)
                {
                    return new ServiceResult<mk_business_zones>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                mk_Business_Zones.zone_id = Guid.CreateVersion7();
                mk_Business_Zones.zone_name = mk_Business_Zones.zone_name;
                mk_Business_Zones.description = mk_Business_Zones.description;
                mk_Business_Zones.created_at = DateTime.Now;
                mk_Business_Zones.updated_at = DateTime.Now;
                mk_Business_Zones.is_active = "T";
                _context.mk_business_zones.Add(mk_Business_Zones);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new ServiceResult<mk_business_zones>
                {
                    Status = 200,
                    Success = true,
                    Message = "Inserted",
                    Data = mk_Business_Zones
                };

            }
            catch (Exception ex)
            {
                return new ServiceResult<mk_business_zones>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServiceResult<List<mk_business_zones>>> get_zones(Guid company_id)
        {
            try
            {
                if (company_id == Guid.Empty)
                {
                    return new ServiceResult<List<mk_business_zones>>
                    {
                        Success = false,
                        Status = 300,
                        Message = "No data found"
                    };
                }
                var zone_list = await _context.mk_business_zones.Where(a => a.business_id == company_id).ToListAsync();
                if (zone_list.Count == 0)
                {
                    return new ServiceResult<List<mk_business_zones>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "no data found"
                    };
                }
                return new ServiceResult<List<mk_business_zones>>
                {
                    Success = true,
                    Status = 200,
                    Message = "Success",
                    Data = zone_list
                };

            } catch (Exception ex)
            {
                return new ServiceResult<List<mk_business_zones>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<om_CustomerOrders_dto>>> Get_order_list(Guid company_id)
        {
            try
            {
                var items = await _context.Set<om_CustomerOrders_dto>()
                    .FromSqlRaw(
                        "EXEC dbo.om_orders  @opr=@opr, @business_id=@business_id",
                        new SqlParameter("@business_id", company_id),
                        new SqlParameter("@opr", 1)
                    )
                .ToListAsync();
                if (items.Count == 0)
                {
                    return new ServiceResult<List<om_CustomerOrders_dto>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var result = items.GroupBy(a => a.customer_order_id).Select(a => new om_CustomerOrders_dto
                {
                    customer_order_id = a.Key,
                    business_id = a.First().business_id,
                    sales_id = a.First().sales_id,
                    source_id = a.First().source_id,
                    order_date = a.First().order_date,
                    payment_status = a.First().payment_status,
                    sub_total = a.First().sub_total,
                    delivery_contact_name = a.First().delivery_contact_name,
                    delivery_contact_no = a.First().delivery_contact_no,
                    delivery_address1 = a.First().delivery_address1,
                    delivery_city = a.First().delivery_city,
                    delivery_postal_code = a.First().delivery_postal_code,
                    delivery_latitude = a.First().delivery_latitude,
                    delivery_longitude = a.First().delivery_longitude,
                    delevery_end_time = a.First().delevery_end_time,
                    delevery_start_time = a.First().delevery_start_time,
                    delivery_status = a.First().delivery_status,
                    currency_code = a.First().currency_code,
                    platform_name = a.First().platform_name,
                    delevery_date = a.First().delevery_date,
                    source_name = a.First().source_name,
                    order_no = a.First().order_no,
                    zone_name = a.First().zone_name,
                    urget_delivery = a.First().urget_delivery,

                    om_CustomerOrdersLine_Dtos = a.Where(a => a.customer_order_line_id != null)
                    .GroupBy(a => a.customer_order_line_id).Select(l => new om_CustomerOrdersLine_dto
                    {
                        customer_order_line_id = l.Key,
                        product_id = l.First().product_id,
                        title = l.First().title,
                        variant_id = l.First().variant_id,
                        image_url = l.First().image_url,
                        batch_id = l.First().batch_id,
                        ordered_qty = l.First().ordered_qty,
                        unit_price = l.First().unit_price,
                        line_total = l.First().line_total,

                    }).ToList()
                }).OrderBy(a => a.order_no).ToList();

                return new ServiceResult<List<om_CustomerOrders_dto>>
                {
                    Status = 200,
                    Message = "success",
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<om_CustomerOrders_dto>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<mk_blacklisted_numbers_dto>> check_black_list(Guid business_id, string phone_number)
        {
            try
            {
                if (business_id == null)
                {
                    return new ServiceResult<mk_blacklisted_numbers_dto>
                    {
                        Success = false,
                        Message = "No data found"
                    };
                }
                var existing = await _context.mk_blacklisted_numbers.Where(a=>a.business_id==business_id&& a.phone_number==phone_number && a.is_active=="T").ToListAsync();
                if (existing.Any())
                {
                    return new ServiceResult<mk_blacklisted_numbers_dto>
                    {
                        Status = 200,
                        Success = true,
                        Message = "Number in Black list"
                    };
                }
                return new ServiceResult<mk_blacklisted_numbers_dto>
                {
                    
                };
            }
            catch(Exception ex)
            {
                return new ServiceResult<mk_blacklisted_numbers_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<om_CustomerOrders_dto>>> Get_order_list_customer_order_id(Guid customer_order_id)
        {
            try
            {
                var items = await _context.Set<om_CustomerOrders_dto>()
                    .FromSqlRaw(
                        "EXEC dbo.om_orders  @opr=@opr, @customer_order_id =@customer_order_id ",
                        new SqlParameter("@customer_order_id ", customer_order_id),
                        new SqlParameter("@opr", 2)
                    )
                .ToListAsync();
                if (items.Count == 0)
                {
                    return new ServiceResult<List<om_CustomerOrders_dto>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }
                var result = items.GroupBy(a => a.customer_order_id).Select(a => new om_CustomerOrders_dto
                {
                    customer_order_id = a.Key,
                    business_id = a.First().business_id,
                    source_id = a.First().source_id,
                    sales_id = a.First().sales_id,
                    order_date = a.First().order_date,
                    payment_status = a.First().payment_status,
                    sub_total = a.First().sub_total,
                    delivery_contact_name = a.First().delivery_contact_name,
                    delivery_contact_no = a.First().delivery_contact_no,
                    delivery_address1 = a.First().delivery_address1,
                    delivery_city = a.First().delivery_city,
                    delivery_postal_code = a.First().delivery_postal_code,
                    delivery_latitude = a.First().delivery_latitude,
                    delivery_longitude = a.First().delivery_longitude,
                    delevery_end_time = a.First().delevery_end_time,
                    delevery_start_time = a.First().delevery_start_time,
                    delivery_status = a.First().delivery_status,
                    currency_code = a.First().currency_code,
                    platform_name = a.First().platform_name,
                    delevery_date = a.First().delevery_date,
                    source_name = a.First().source_name,
                    order_no = a.First().order_no,
                    zone_name = a.First().zone_name,
                    urget_delivery = a.First().urget_delivery,

                    om_CustomerOrdersLine_Dtos = a.Where(a => a.customer_order_line_id != null)
                    .GroupBy(a => a.customer_order_line_id).Select(l => new om_CustomerOrdersLine_dto
                    {
                        customer_order_line_id = l.Key,
                        product_id = l.First().product_id,
                        title = l.First().title,
                        variant_id = l.First().variant_id,
                        image_url = l.First().image_url,
                        batch_id = l.First().batch_id,
                        ordered_qty = l.First().ordered_qty,
                        unit_price = l.First().unit_price,
                        line_total = l.First().line_total,

                    }).ToList()
                }).OrderBy(a => a.order_no).ToList();

                return new ServiceResult<List<om_CustomerOrders_dto>>
                {
                    Status = 200,
                    Message = "success",
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<om_CustomerOrders_dto>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<om_FulfillmentOrders>> Create_fulfillment(om_FulfillmentOrders model)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (model == null)
                {
                    return new ServiceResult<om_FulfillmentOrders>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found"
                    };
                }

                model.fulfillment_id = Guid.CreateVersion7();
                model.created_at = DateTime.Now;
                model.fulfillment_status = model.fulfillment_status ?? "PENDING";

                _context.om_FulfillmentOrders.Add(model);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ServiceResult<om_FulfillmentOrders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Fulfillment created successfully",
                    Data = model
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating fulfillment");

                return new ServiceResult<om_FulfillmentOrders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<om_FulfillmentOrders>>> Get_fulfillments(Guid business_id)
        {
            try
            {
                var data = await _context.om_FulfillmentOrders
                    .Where(x => x.business_id == business_id)
                    .OrderByDescending(x => x.created_at)
                    .ToListAsync();

                if (data.Count == 0)
                {
                    return new ServiceResult<List<om_FulfillmentOrders>>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }

                return new ServiceResult<List<om_FulfillmentOrders>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<om_FulfillmentOrders>>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<om_FulfillmentOrders>> Get_fulfillment_by_id(Guid fulfillment_id)
        {
            try
            {
                var data = await _context.om_FulfillmentOrders
                    .FirstOrDefaultAsync(x => x.fulfillment_id == fulfillment_id);

                if (data == null)
                {
                    return new ServiceResult<om_FulfillmentOrders>
                    {
                        Status = 300,
                        Success = false,
                        Message = "No data found"
                    };
                }

                return new ServiceResult<om_FulfillmentOrders>
                {
                    Status = 200,
                    Success = true,
                    Message = "Success",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<om_FulfillmentOrders>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<update_order_details_result_dto>> Update_order_details(update_order_details_dto model)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();
            var debugStep = "init";
            try
            {
                debugStep = "validate_input";
                if (model == null)
                {
                    return new ServiceResult<update_order_details_result_dto>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found"
                    };
                }

                debugStep = "load_order";
                var now = DateTime.Now;
                var order = await _context.om_CustomerOrders
                    .FirstOrDefaultAsync(x =>
                        x.customer_order_id == model.customer_order_id &&
                        x.business_id == model.business_id &&
                        x.store_id == model.store_id);

                if (order == null)
                {
                    return new ServiceResult<update_order_details_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Order not found"
                    };
                }

                var previousDeliveryStatus = order.delivery_status;

                if (!string.IsNullOrWhiteSpace(model.expected_payment_method))
                    order.expected_payment_method = model.expected_payment_method.Trim();

                if (!string.IsNullOrWhiteSpace(model.delivery_status))
                    order.delivery_status = model.delivery_status.Trim().ToUpperInvariant();

                if (model.notes != null)
                    order.notes = model.notes;

                if (model.internal_notes != null)
                    order.internal_notes = model.internal_notes;

                if (model.delivered_at.HasValue)
                    order.delevery_date = model.delivered_at.Value;

                var receiptFiles = (model.receipt_files ?? new List<IFormFile>())
                    .Where(f => f != null && f.Length > 0)
                    .ToList();

                var otherFiles = (model.other_files ?? new List<IFormFile>())
                    .Where(f => f != null && f.Length > 0)
                    .ToList();

                var hasAnyUploadedFile = receiptFiles.Count > 0 || otherFiles.Count > 0;

                // Business rule: once delivery proof files are uploaded, mark delivery as completed.
                if (hasAnyUploadedFile)
                {
                    model.delivery_status = "DELIVERED";
                    order.delivery_status = "DELIVERED";
                    if (string.IsNullOrWhiteSpace(model.sales_mode))
                        model.sales_mode = "DELIVERED";
                    if (!model.delivered_at.HasValue)
                        model.delivered_at = now;
                    order.delevery_date = model.delivered_at.Value;
                }

                Guid? insertedSalePaymentId = null;
                var hasInsertedPayment = false;
                var salesId = order.sales_id;
                string computedPaymentStatus = order.payment_status;
                so_SalesHeaders? salesHeaderRecord = null;
                if (salesId.HasValue && salesId.Value != Guid.Empty)
                {
                    salesHeaderRecord = await _context.so_SalesHeaders
                        .FirstOrDefaultAsync(x => x.sales_id == salesId.Value);
                }

                var resolvedSalesMode = string.IsNullOrWhiteSpace(model.sales_mode)
                    ? null
                    : model.sales_mode.Trim().ToUpperInvariant();

                if (string.IsNullOrWhiteSpace(resolvedSalesMode) && hasAnyUploadedFile)
                    resolvedSalesMode = "DELIVERED";

                if (!string.IsNullOrWhiteSpace(resolvedSalesMode))
                {
                    if (!salesId.HasValue || salesId.Value == Guid.Empty)
                    {
                        return new ServiceResult<update_order_details_result_dto>
                        {
                            Status = 409,
                            Success = false,
                            Message = "Sales id not found for this order. Cannot update sales mode."
                        };
                    }

                    if (salesHeaderRecord == null)
                    {
                        return new ServiceResult<update_order_details_result_dto>
                        {
                            Status = 404,
                            Success = false,
                            Message = "Sales header not found for this order. Cannot update sales mode."
                        };
                    }

                    salesHeaderRecord.sales_mode = resolvedSalesMode;
                    _context.so_SalesHeaders.Update(salesHeaderRecord);
                }

                Guid? resolvedPaymentMethodId = model.payment_method_id;
                if (!resolvedPaymentMethodId.HasValue && !string.IsNullOrWhiteSpace(model.expected_payment_method))
                {
                    var payCode = model.expected_payment_method.Trim().ToUpperInvariant();
                    resolvedPaymentMethodId = await _context.so_Payment_Types
                        .Where(x => x.business_id == order.business_id
                                    && x.is_avilable == "T"
                                    && x.PayTypeCode != null
                                    && x.PayTypeCode.ToUpper() == payCode)
                        .Select(x => x.payment_type_id)
                        .FirstOrDefaultAsync();

                    if (resolvedPaymentMethodId == Guid.Empty)
                        resolvedPaymentMethodId = null;
                }

                // Fallback for upload flow: if client did not pass a payment method, pick the first active one.
                if (!resolvedPaymentMethodId.HasValue && hasAnyUploadedFile)
                {
                    resolvedPaymentMethodId = await _context.so_Payment_Types
                        .Where(x => x.business_id == order.business_id && x.is_avilable == "T")
                        .OrderByDescending(x => x.cash_types == "T")
                        .ThenBy(x => x.Order ?? int.MaxValue)
                        .Select(x => x.payment_type_id)
                        .FirstOrDefaultAsync();

                    if (resolvedPaymentMethodId == Guid.Empty)
                        resolvedPaymentMethodId = null;
                }

                decimal resolvedAmount = model.amount ?? 0m;
                if (resolvedAmount <= 0m && hasAnyUploadedFile && salesId.HasValue && salesId.Value != Guid.Empty)
                {
                    var alreadyPaid = await _context.pos_SalePayments
                        .Where(x => x.sale_id == salesId.Value && x.is_voided != "T")
                        .SumAsync(x => x.amount);
                    resolvedAmount = order.grand_total - alreadyPaid;
                    if (resolvedAmount < 0m)
                        resolvedAmount = 0m;
                }

                var shouldAttemptPaymentInsert =
                    hasAnyUploadedFile ||
                    (model.amount.HasValue && model.amount.Value > 0m) ||
                    model.payment_method_id.HasValue ||
                    !string.IsNullOrWhiteSpace(model.expected_payment_method);

                if (shouldAttemptPaymentInsert)
                {
                    debugStep = "insert_sale_payment";
                    if (!salesId.HasValue || salesId.Value == Guid.Empty)
                    {
                        return new ServiceResult<update_order_details_result_dto>
                        {
                            Status = 409,
                            Success = false,
                            Message = "Sales id not found for this order. Cannot add payment."
                        };
                    }

                    if (!resolvedPaymentMethodId.HasValue)
                    {
                        return new ServiceResult<update_order_details_result_dto>
                        {
                            Status = 400,
                            Success = false,
                            Message = "Unable to resolve payment method for payment insert."
                        };
                    }

                    if (resolvedAmount <= 0m)
                    {
                        return new ServiceResult<update_order_details_result_dto>
                        {
                            Status = 400,
                            Success = false,
                            Message = "Unable to resolve payment amount for payment insert."
                        };
                    }

                    var existingLines = await _context.pos_SalePayments
                        .Where(x => x.sale_id == salesId.Value)
                        .ToListAsync();
                    var nextLineNo = (existingLines.Select(x => x.line_no ?? 0).DefaultIfEmpty(0).Max()) + 1;

                    var paymentRow = new pos_SalePayments
                    {
                        sale_payment_id = Guid.CreateVersion7(),
                        business_id = order.business_id,
                        store_id = order.store_id,
                        sale_id = salesId.Value,
                        payment_method_id = resolvedPaymentMethodId.Value,
                        receipt_no = salesHeaderRecord?.invoice_no,
                        line_no = nextLineNo,
                        currency_code = salesHeaderRecord?.base_currency_code ?? order.currency_code,
                        fx_rate = salesHeaderRecord?.fx_rate_to_base > 0 ? salesHeaderRecord.fx_rate_to_base : 1m,
                        amount = resolvedAmount,
                        base_amount = resolvedAmount,
                        reference_no = model.reference_no,
                        notes = model.payment_note ?? model.notes,
                        is_voided = "F",
                        created_at = now
                    };

                    _context.pos_SalePayments.Add(paymentRow);
                    insertedSalePaymentId = paymentRow.sale_payment_id;
                    hasInsertedPayment = true;
                }

                if (salesId.HasValue && salesId.Value != Guid.Empty)
                {
                    debugStep = "recompute_payment_status";
                    var totalPaid = await _context.pos_SalePayments
                        .Where(x => x.sale_id == salesId.Value && x.is_voided != "T")
                        .SumAsync(x => x.amount);

                    var totalDue = order.grand_total;
                    if (totalPaid <= 0m)
                        computedPaymentStatus = "UNPAID";
                    else if (totalPaid >= totalDue)
                        computedPaymentStatus = "PAID";
                    else
                        computedPaymentStatus = "PARTIAL";

                    // Business rule for delivery update flow:
                    // when files are uploaded, consider order paid and reflect that in order/lines.
                    if (hasAnyUploadedFile)
                        computedPaymentStatus = "PAID";

                    var ordersBySales = await _context.om_CustomerOrders
                        .Where(x => x.sales_id == salesId.Value)
                        .ToListAsync();

                    foreach (var orderBySales in ordersBySales)
                    {
                        orderBySales.payment_status = computedPaymentStatus;
                        orderBySales.updated_at = now;
                        orderBySales.updated_by = model.updated_by;
                    }

                    if (ordersBySales.Count > 0)
                        _context.om_CustomerOrders.UpdateRange(ordersBySales);

                    var affectedOrderIds = ordersBySales
                        .Select(x => x.customer_order_id)
                        .Distinct()
                        .ToList();

                    if (affectedOrderIds.Count > 0)
                    {
                        // Avoid local-list Contains translation (OPENJSON with '$') for older SQL Server compatibility.
                        var lines = await _context.om_CustomerOrderLines
                            .Join(
                                _context.om_CustomerOrders.Where(o => o.sales_id == salesId.Value),
                                line => line.customer_order_id,
                                header => header.customer_order_id,
                                (line, _) => line)
                            .ToListAsync();

                        foreach (var line in lines)
                        {
                            line.line_status = computedPaymentStatus;
                            line.updated_at = now;
                            line.updated_by = model.updated_by;
                        }

                        if (lines.Count > 0)
                            _context.om_CustomerOrderLines.UpdateRange(lines);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(model.payment_status))
                {
                    computedPaymentStatus = model.payment_status.Trim().ToUpperInvariant();
                    order.payment_status = computedPaymentStatus;
                }

                order.payment_status = computedPaymentStatus;
                order.updated_at = now;
                order.updated_by = model.updated_by;
                _context.om_CustomerOrders.Update(order);

                var eventStatus = string.IsNullOrWhiteSpace(model.status_type)
                    ? null
                    : model.status_type.Trim().ToUpperInvariant();

                if (string.IsNullOrWhiteSpace(eventStatus))
                {
                    if (hasInsertedPayment || !string.IsNullOrWhiteSpace(model.payment_status))
                        eventStatus = "PAYMENT";
                    else if (!string.IsNullOrWhiteSpace(model.delivery_status) || model.out_for_delivery_at.HasValue || model.delivered_at.HasValue)
                        eventStatus = "DELIVERY";
                    else if (model.fulfillment_id.HasValue)
                        eventStatus = "FULFILLMENT";
                }

                if (!string.IsNullOrWhiteSpace(eventStatus))
                {
                    debugStep = "insert_status_history";
                    var latestHistory = await _context.om_OrderStatusHistories
                        .Where(x => x.customer_order_id == order.customer_order_id)
                        .OrderByDescending(x => x.changed_at)
                        .FirstOrDefaultAsync();

                    _context.om_OrderStatusHistories.Add(new om_OrderStatusHistory
                    {
                        order_status_history_id = Guid.CreateVersion7(),
                        customer_order_id = order.customer_order_id,
                        old_status = latestHistory?.new_status ?? string.Empty,
                        new_status = eventStatus,
                        status_type = eventStatus,
                        changed_by = model.updated_by,
                        changed_at = now
                    });
                }

                om_FulfillmentOrders? fulfillment = null;
                if (model.fulfillment_id.HasValue)
                {
                    fulfillment = await _context.om_FulfillmentOrders
                        .FirstOrDefaultAsync(x =>
                            x.fulfillment_id == model.fulfillment_id.Value &&
                            x.customer_order_id == order.customer_order_id &&
                            x.business_id == model.business_id &&
                            x.store_id == model.store_id);

                    if (fulfillment == null)
                    {
                        return new ServiceResult<update_order_details_result_dto>
                        {
                            Status = 404,
                            Success = false,
                            Message = "Fulfillment not found"
                        };
                    }
                }
                else
                {
                    fulfillment = await _context.om_FulfillmentOrders
                        .Where(x => x.customer_order_id == order.customer_order_id)
                        .OrderByDescending(x => x.created_at)
                        .FirstOrDefaultAsync();
                }

                if (fulfillment != null)
                {
                    if (model.out_for_delivery_at.HasValue)
                        fulfillment.out_for_delivery_at = model.out_for_delivery_at.Value;

                    if (model.collected_amount.HasValue)
                        fulfillment.collected_amount = model.collected_amount.Value;

                    if (!string.IsNullOrWhiteSpace(model.delivery_status))
                        fulfillment.fulfillment_status = model.delivery_status.Trim().ToUpperInvariant();

                    fulfillment.updated_at = now;
                    fulfillment.updated_by = model.updated_by;
                    _context.om_FulfillmentOrders.Update(fulfillment);
                }

                var business = await _context.co_business
                    .FirstOrDefaultAsync(x => x.company_id == model.business_id);

                if (business == null)
                {
                    return new ServiceResult<update_order_details_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Business not found"
                    };
                }

                if ((receiptFiles.Count > 0 || otherFiles.Count > 0) && (!salesId.HasValue || salesId.Value == Guid.Empty))
                {
                    return new ServiceResult<update_order_details_result_dto>
                    {
                        Status = 409,
                        Success = false,
                        Message = "Sales id not found for this order. Cannot upload files."
                    };
                }

                var sourceId = salesId ?? Guid.Empty;
                string? proofUrl = null;
                var receiptCount = 0;
                var otherCount = 0;

                if (receiptFiles.Count > 0)
                {
                    debugStep = "deactivate_old_receipts";
                    var existingReceipts = await _context.sys_Images
                        .Where(x =>
                            x.business_id == order.business_id &&
                            x.source_id == sourceId &&
                            x.source_type == "PAYMENT" &&
                            x.status == "T")
                        .ToListAsync();

                    foreach (var oldReceipt in existingReceipts)
                    {
                        oldReceipt.status = "F";
                        oldReceipt.updated_at = now;
                    }

                    if (existingReceipts.Count > 0)
                        _context.sys_Images.UpdateRange(existingReceipts);
                }

                foreach (var file in receiptFiles)
                {
                    debugStep = "insert_receipt_images";
                    var invoiceNo = salesHeaderRecord?.invoice_no ?? order.order_no?.ToString() ?? order.customer_order_id.ToString();
                    var imageUrl = await UploadOrderFileAsync(file, business, sourceId, invoiceNo, "PAYMENT");
                    _context.sys_Images.Add(new sys_Images
                    {
                        image_id = Guid.CreateVersion7(),
                        source_id = sourceId,
                        business_id = order.business_id,
                        source_type = "PAYMENT",
                        image_url = imageUrl,
                        created_at = now,
                        updated_at = now,
                        status = "T"
                    });
                    receiptCount++;
                    if (proofUrl == null)
                        proofUrl = imageUrl;
                }

                foreach (var file in otherFiles)
                {
                    debugStep = "insert_other_images";
                    var invoiceNo = salesHeaderRecord?.invoice_no ?? order.order_no?.ToString() ?? order.customer_order_id.ToString();
                    var imageUrl = await UploadOrderFileAsync(file, business, sourceId, invoiceNo, "PAYMENT");
                    _context.sys_Images.Add(new sys_Images
                    {
                        image_id = Guid.CreateVersion7(),
                        source_id = sourceId,
                        business_id = order.business_id,
                        source_type = "PAYMENT",
                        image_url = imageUrl,
                        created_at = now,
                        updated_at = now,
                        status = "T"
                    });
                    otherCount++;
                }

                if (fulfillment != null && !string.IsNullOrWhiteSpace(proofUrl))
                {
                    fulfillment.proof_of_delivery_image_url = proofUrl;
                    fulfillment.updated_at = now;
                    fulfillment.updated_by = model.updated_by;
                    _context.om_FulfillmentOrders.Update(fulfillment);
                }

                // Inventory update on first delivery completion:
                // Reduce on_hand_quantity and committed_quantity by ordered qty
                // using store_variant_inventory_id from order lines.
                var isFirstDeliveryCompletion =
                    hasAnyUploadedFile &&
                    !string.Equals(previousDeliveryStatus, "DELIVERED", StringComparison.OrdinalIgnoreCase);

                if (isFirstDeliveryCompletion)
                {
                    debugStep = "update_store_variant_inventory";

                    var inventoryReductions = await _context.om_CustomerOrderLines
                        .Where(x => x.customer_order_id == order.customer_order_id && x.store_variant_inventory_id.HasValue)
                        .GroupBy(x => x.store_variant_inventory_id!.Value)
                        .Select(g => new
                        {
                            store_variant_inventory_id = g.Key,
                            qty_to_reduce = g.Sum(x => x.ordered_qty)
                        })
                        .ToListAsync();

                    foreach (var reduction in inventoryReductions)
                    {
                        var inv = await _context.im_StoreVariantInventory
                            .FirstOrDefaultAsync(x => x.store_variant_inventory_id == reduction.store_variant_inventory_id);

                        if (inv == null) continue;

                        var reduceBy = reduction.qty_to_reduce < 0m ? 0m : reduction.qty_to_reduce;
                        var currentOnHand = inv.on_hand_quantity ?? 0m;
                        var currentCommitted = inv.committed_quantity ?? 0m;

                        var nextOnHand = currentOnHand - reduceBy;
                        var nextCommitted = currentCommitted - reduceBy;

                        inv.on_hand_quantity = nextOnHand < 0m ? 0m : nextOnHand;
                        inv.committed_quantity = nextCommitted < 0m ? 0m : nextCommitted;

                        _context.im_StoreVariantInventory.Update(inv);
                    }
                }

                if (hasAnyUploadedFile && salesId.HasValue && salesId.Value != Guid.Empty)
                {
                    debugStep = "insert_journal_header";
                    var journalExists = await _context.gl_JournalHeaders
                        .AnyAsync(x => x.SourceId == salesId.Value && x.SourceType == "POS");

                    if (!journalExists)
                    {
                        var journal = await _salesService.Add_Journal_header(salesId.Value);
                        if (!journal.Success)
                        {
                            return new ServiceResult<update_order_details_result_dto>
                            {
                                Status = 500,
                                Success = false,
                                Message = journal.Message ?? "Failed to create journal header"
                            };
                        }
                    }
                }

                debugStep = "save_changes";
                await _context.SaveChangesAsync();
                debugStep = "commit";
                await tx.CommitAsync();

                return new ServiceResult<update_order_details_result_dto>
                {
                    Status = 200,
                    Success = true,
                    Message = "Order details updated successfully",
                    Data = new update_order_details_result_dto
                    {
                        customer_order_id = order.customer_order_id,
                        fulfillment_id = fulfillment?.fulfillment_id,
                        receipt_file_count = receiptCount,
                        other_file_count = otherCount,
                        proof_of_delivery_image_url = proofUrl ?? fulfillment?.proof_of_delivery_image_url,
                        sale_payment_id = insertedSalePaymentId,
                        payment_status = computedPaymentStatus
                    }
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error updating order details");
                return new ServiceResult<update_order_details_result_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = $"[{debugStep}] {ex.Message}"
                };
            }
        }

        public async Task<ServiceResult<update_sales_mode_result_dto>> Update_sales_mode(update_sales_mode_dto model)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                if (model == null)
                {
                    return new ServiceResult<update_sales_mode_result_dto>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found"
                    };
                }

                var resolvedSalesMode = string.IsNullOrWhiteSpace(model.sales_mode)
                    ? "DELIVERED"
                    : model.sales_mode.Trim().ToUpperInvariant();

                var order = await _context.om_CustomerOrders
                    .FirstOrDefaultAsync(x =>
                        x.customer_order_id == model.customer_order_id &&
                        x.business_id == model.business_id &&
                        x.store_id == model.store_id);

                if (order == null)
                {
                    return new ServiceResult<update_sales_mode_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Order not found"
                    };
                }

                if (!order.sales_id.HasValue || order.sales_id.Value == Guid.Empty)
                {
                    return new ServiceResult<update_sales_mode_result_dto>
                    {
                        Status = 409,
                        Success = false,
                        Message = "Sales id not found for this order."
                    };
                }

                var salesHeader = await _context.so_SalesHeaders
                    .FirstOrDefaultAsync(x => x.sales_id == order.sales_id.Value);

                if (salesHeader == null)
                {
                    return new ServiceResult<update_sales_mode_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Sales header not found"
                    };
                }

                salesHeader.sales_mode = resolvedSalesMode;
                _context.so_SalesHeaders.Update(salesHeader);
                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                return new ServiceResult<update_sales_mode_result_dto>
                {
                    Status = 200,
                    Success = true,
                    Message = "Sales mode updated successfully",
                    Data = new update_sales_mode_result_dto
                    {
                        customer_order_id = order.customer_order_id,
                        sales_id = order.sales_id.Value,
                        sales_mode = salesHeader.sales_mode
                    }
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error updating sales mode");
                return new ServiceResult<update_sales_mode_result_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResult<update_quantity_result_dto>> Update_order_quantity(update_quantity_dto model)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();
            var debugStep = "init";
            try
            {
                debugStep = "validate_input";
                if (model == null)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No data found"
                    };
                }

                if (model.new_ordered_qty < 0m)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 400,
                        Success = false,
                        Message = "new_ordered_qty cannot be negative"
                    };
                }

                if (model.new_ordered_qty == 0m && !model.confirm_delete)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 400,
                        Success = false,
                        Message = "To set quantity to zero, please send confirm_delete = true."
                    };
                }

                debugStep = "load_order";
                var now = DateTime.Now;
                var order = await _context.om_CustomerOrders.FirstOrDefaultAsync(x =>
                    x.customer_order_id == model.customer_order_id &&
                    x.business_id == model.business_id &&
                    x.store_id == model.store_id);

                if (order == null)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Order not found"
                    };
                }

                debugStep = "load_order_line";
                var orderLine = await _context.om_CustomerOrderLines.FirstOrDefaultAsync(x =>
                    x.customer_order_line_id == model.customer_order_line_id &&
                    x.customer_order_id == model.customer_order_id);

                if (orderLine == null)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Order line not found"
                    };
                }

                var oldQty = orderLine.ordered_qty;
                var newQty = model.new_ordered_qty;

                if (newQty == oldQty)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 200,
                        Success = true,
                        Message = "Quantity unchanged",
                        Data = new update_quantity_result_dto
                        {
                            customer_order_line_id = orderLine.customer_order_line_id,
                            old_qty = oldQty,
                            new_qty = newQty,
                            released_qty = 0m
                        }
                    };
                }

                if (!order.sales_id.HasValue || order.sales_id.Value == Guid.Empty)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 409,
                        Success = false,
                        Message = "Sales id not found for this order"
                    };
                }

                var salesId = order.sales_id.Value;
                var changedQty = Math.Abs(newQty - oldQty);
                var isIncrease = newQty > oldQty;
                if (changedQty <= 0m)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 400,
                        Success = false,
                        Message = "No quantity change detected"
                    };
                }

                debugStep = "load_sales_header";
                var salesHeader = await _context.so_SalesHeaders.FirstOrDefaultAsync(x => x.sales_id == salesId);
                if (salesHeader == null)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Sales header not found"
                    };
                }

                debugStep = "load_sales_line";
                var salesLineQuery = _context.so_SalesLines.Where(x =>
                    x.sales_id == salesId &&
                    x.product_id == orderLine.product_id &&
                    x.variant_id == orderLine.variant_id);

                if (orderLine.store_variant_inventory_id.HasValue)
                {
                    var invId = orderLine.store_variant_inventory_id.Value;
                    salesLineQuery = salesLineQuery.Where(x => x.store_variant_inventory_id == invId);
                }
                if (orderLine.batch_id.HasValue)
                {
                    var batchId = orderLine.batch_id.Value;
                    salesLineQuery = salesLineQuery.Where(x => x.batch_id == batchId);
                }

                var salesLine = await salesLineQuery.FirstOrDefaultAsync()
                    ?? await _context.so_SalesLines.FirstOrDefaultAsync(x =>
                        x.sales_id == salesId &&
                        x.product_id == orderLine.product_id &&
                        x.variant_id == orderLine.variant_id);

                if (salesLine == null)
                {
                    return new ServiceResult<update_quantity_result_dto>
                    {
                        Status = 404,
                        Success = false,
                        Message = "Matching sales line not found"
                    };
                }

                var updatedUser = model.updated_by.HasValue
                    ? await _context.am_users.FirstOrDefaultAsync(x => x.userId == model.updated_by.Value)
                    : null;
                var updatedByName = TruncateText(updatedUser?.fullName ?? "SYSTEM", 50);
                var safeRemarks255 = TruncateText(model.remarks, 255);
                var safeReason50 = TruncateText(
                    string.IsNullOrWhiteSpace(model.remarks)
                        ? (isIncrease ? "Order quantity increased" : "Order quantity decreased")
                        : model.remarks,
                    50);

                var oldSalesQty = salesLine.quantity;
                var perUnitOrderTax = oldQty > 0m ? orderLine.tax_amount / oldQty : 0m;
                var perUnitSalesTax = oldSalesQty > 0m ? salesLine.tax_amount / oldSalesQty : 0m;
                var perUnitSalesTaxBase = oldSalesQty > 0m ? salesLine.tax_amount_base / oldSalesQty : 0m;

                debugStep = "update_order_line";
                orderLine.ordered_qty = newQty;
                orderLine.line_total = Round4(newQty * orderLine.unit_price);
                orderLine.tax_amount = Round4(newQty * perUnitOrderTax);
                if (!isIncrease)
                {
                    orderLine.returned_qty += changedQty;
                }
                orderLine.updated_at = now;
                orderLine.updated_by = model.updated_by;
                _context.om_CustomerOrderLines.Update(orderLine);

                debugStep = "update_sales_line";
                salesLine.quantity = newQty;
                salesLine.line_total = Round4(newQty * salesLine.unit_price);
                salesLine.tax_amount = Round4(newQty * perUnitSalesTax);
                salesLine.line_total_base = Round4(newQty * salesLine.unit_price_base);
                salesLine.tax_amount_base = Round4(newQty * perUnitSalesTaxBase);
                if (!isIncrease)
                {
                    salesLine.return_qty += changedQty;
                    salesLine.returned_quantity += changedQty;
                }
                // Keep non-zero value to satisfy row-level check constraint (fx_rate_to_base > 0).
                if (salesLine.fx_rate_to_base <= 0m)
                    salesLine.fx_rate_to_base = 1m;
                salesLine.remarks = safeRemarks255;
                _context.so_SalesLines.Update(salesLine);

                if (!isIncrease)
                {
                    debugStep = "insert_sales_return";
                    var returnLineSubTotal = Round4(changedQty * salesLine.unit_price);
                    var returnLineTax = Round4(changedQty * perUnitSalesTax);
                    var returnLineSubTotalBase = Round4(changedQty * salesLine.unit_price_base);
                    var returnLineTaxBase = Round4(changedQty * perUnitSalesTaxBase);

                    var returnHeader = new so_SalesReturnHeaders
                    {
                        sales_return_id = Guid.CreateVersion7(),
                        sales_id = salesId,
                        business_id = salesHeader.business_id,
                        store_id = salesHeader.store_id,
                        customer_id = salesHeader.customer_id,
                        payment_term_id = salesHeader.payment_term_id,
                        return_no = TruncateText(await GetNextSalesReturnNoAsync(salesHeader.business_id, salesHeader.store_id), 50),
                        return_date = now,
                        doc_type = string.IsNullOrWhiteSpace(salesHeader.doc_type) ? "SALE" : TruncateText(salesHeader.doc_type, 10),
                        return_type = TruncateText("RETURN", 10),
                        return_reason = TruncateText(
                            string.IsNullOrWhiteSpace(model.remarks) ? "Quantity decreased from order details" : model.remarks,
                            255),
                        doc_currency_code = TruncateText(salesHeader.doc_currency_code, 15),
                        base_currency_code = TruncateText(salesHeader.base_currency_code, 15),
                        fx_rate_to_base = salesHeader.fx_rate_to_base,
                        sub_total = returnLineSubTotal,
                        discount_total = 0m,
                        tax_total = returnLineTax,
                        grand_total = returnLineSubTotal + returnLineTax,
                        sub_total_base = returnLineSubTotalBase,
                        discount_total_base = 0m,
                        tax_total_base = returnLineTaxBase,
                        grand_total_base = returnLineSubTotalBase + returnLineTaxBase,
                        notes = safeRemarks255,
                        created_at = now,
                        created_by = TruncateText(updatedByName, 130)
                    };

                    var returnLine = new so_SalesReturnLines
                    {
                        sales_return_line_id = Guid.CreateVersion7(),
                        sales_return_id = returnHeader.sales_return_id,
                        business_id = salesLine.business_id,
                        store_id = salesLine.store_id,
                        product_id = salesLine.product_id,
                        variant_id = salesLine.variant_id,
                        store_variant_inventory_id = salesLine.store_variant_inventory_id,
                        batch_id = salesLine.batch_id,
                        barcode = salesLine.barcode,
                        product_sku = salesLine.product_sku,
                        track_expiry = salesLine.track_expiry,
                        item_description = salesLine.item_description,
                        return_qty = changedQty,
                        unit_price = salesLine.unit_price,
                        discount_amount = salesLine.discount_amount,
                        discount_percent = salesLine.discount_percent,
                        tax_amount = returnLineTax,
                        line_total = returnLineSubTotal,
                        unit_price_base = salesLine.unit_price_base,
                        discount_amount_base = salesLine.discount_amount_base,
                        tax_amount_base = returnLineTaxBase,
                        line_total_base = returnLineSubTotalBase,
                        tax_class = TruncateText(salesLine.tax_class, 50),
                        return_reason = safeRemarks255,
                        created_at = now,
                        line_status = TruncateText("RETURNED", 10)
                    };

                    _context.so_SalesReturnHeaders.Add(returnHeader);
                    _context.so_SalesReturnLines.Add(returnLine);
                }

                debugStep = "update_fulfillment_lines";
                var fulfillmentLinesQuery = _context.om_FulfillmentLines
                    .Where(x => x.customer_order_line_id == orderLine.customer_order_line_id);
                if (model.fulfillment_id.HasValue)
                {
                    var fulfillmentId = model.fulfillment_id.Value;
                    fulfillmentLinesQuery = fulfillmentLinesQuery.Where(x => x.fulfillment_id == fulfillmentId);
                }

                var fulfillmentLines = await fulfillmentLinesQuery.ToListAsync();
                foreach (var fulfillmentLine in fulfillmentLines)
                {
                    if (isIncrease)
                    {
                        fulfillmentLine.ordered_qty += changedQty;
                    }
                    else
                    {
                        fulfillmentLine.returned_qty += changedQty;
                    }

                    if (!string.IsNullOrWhiteSpace(model.remarks))
                    {
                        fulfillmentLine.remarks = TruncateText(model.remarks, 200);
                    }
                }
                if (fulfillmentLines.Count > 0)
                {
                    _context.om_FulfillmentLines.UpdateRange(fulfillmentLines);
                }

                debugStep = "update_fulfillment_orders";
                var fulfillmentOrders = await _context.om_FulfillmentOrders
                    .Where(x => x.customer_order_id == order.customer_order_id)
                    .ToListAsync();

                foreach (var fulfillmentOrder in fulfillmentOrders)
                {
                    var totals = await _context.om_FulfillmentLines
                        .Where(x => x.fulfillment_id == fulfillmentOrder.fulfillment_id)
                        .GroupBy(_ => 1)
                        .Select(g => new
                        {
                            total_ordered_qty = g.Sum(x => x.ordered_qty),
                            total_returned_qty = g.Sum(x => x.returned_qty)
                        })
                        .FirstOrDefaultAsync();

                    fulfillmentOrder.total_ordered_qty = totals?.total_ordered_qty ?? 0m;
                    fulfillmentOrder.total_returned_qty = totals?.total_returned_qty ?? 0m;
                    fulfillmentOrder.updated_at = now;
                    fulfillmentOrder.updated_by = model.updated_by;
                }
                if (fulfillmentOrders.Count > 0)
                {
                    _context.om_FulfillmentOrders.UpdateRange(fulfillmentOrders);
                }

                debugStep = "insert_inventory_transaction";
                _context.im_InventoryTransactions.Add(new im_InventoryTransactions
                {
                    transaction_id = Guid.CreateVersion7(),
                    sales_line_id = salesLine.sales_line_id,
                    store_id = salesLine.store_id,
                    variant_id = salesLine.variant_id,
                    batch_id = salesLine.batch_id,
                    trans_date = now,
                    trans_type = TruncateText(isIncrease ? "SALE" : "RETURNSALE", 20),
                    trans_reason = safeReason50,
                    quantity_change = changedQty,
                    unit_cost = salesLine.unit_price,
                    total_cost = Round4(changedQty * salesLine.unit_price),
                    source_doc_type = TruncateText("ORDER_QTY_UPDATE", 20),
                    remarks = safeRemarks255,
                    created_date_time = now
                });

                debugStep = "insert_inventory_reservation";
                _context.im_InventoryReservations.Add(new im_InventoryReservations
                {
                    reservation_id = Guid.CreateVersion7(),
                    business_id = order.business_id,
                    store_id = order.store_id,
                    customer_order_id = order.customer_order_id,
                    customer_order_line_id = orderLine.customer_order_line_id,
                    product_id = orderLine.product_id,
                    variant_id = orderLine.variant_id,
                    batch_id = orderLine.batch_id,
                    reserved_qty = isIncrease ? changedQty : 0m,
                    released_qty = isIncrease ? 0m : changedQty,
                    consumed_qty = 0m,
                    reserved_at = isIncrease ? now : null,
                    released_at = isIncrease ? null : now,
                    remarks = safeRemarks255,
                    created_by = updatedByName,
                    created_user_id = model.updated_by,
                    created_at = now,
                    updated_at = now,
                    updated_by = model.updated_by,
                    reservation_status = TruncateText(isIncrease ? "ACTIVE" : "RELEASED", 20)
                });

                if (orderLine.store_variant_inventory_id.HasValue)
                {
                    debugStep = "update_store_variant_inventory";
                    var storeInventory = await _context.im_StoreVariantInventory
                        .FirstOrDefaultAsync(x => x.store_variant_inventory_id == orderLine.store_variant_inventory_id.Value);

                    if (storeInventory != null)
                    {
                        var currentCommitted = storeInventory.committed_quantity ?? 0m;
                        storeInventory.committed_quantity = isIncrease
                            ? currentCommitted + changedQty
                            : Math.Max(0m, currentCommitted - changedQty);
                        _context.im_StoreVariantInventory.Update(storeInventory);
                    }
                }

                if (!isIncrease && orderLine.batch_id.HasValue)
                {
                    debugStep = "update_item_batch";
                    var itemBatch = await _context.im_itemBatches
                        .FirstOrDefaultAsync(x => x.item_batch_id == orderLine.batch_id.Value);
                    if (itemBatch != null)
                    {
                        var currentOnHand = itemBatch.on_hand_quantity ?? 0m;
                        itemBatch.on_hand_quantity = Math.Max(0m, currentOnHand - changedQty);
                        _context.im_itemBatches.Update(itemBatch);
                    }
                }

                debugStep = "recalculate_sales_header";
                var salesLineTotals = await _context.so_SalesLines
                    .Where(x => x.sales_id == salesId)
                    .GroupBy(_ => 1)
                    .Select(g => new
                    {
                        sub_total = g.Sum(x => x.line_total),
                        tax_total = g.Sum(x => x.tax_amount),
                        sub_total_base = g.Sum(x => x.line_total_base),
                        tax_total_base = g.Sum(x => x.tax_amount_base)
                    })
                    .FirstOrDefaultAsync();

                var salesSubTotal = salesLineTotals?.sub_total ?? 0m;
                var salesTaxTotal = salesLineTotals?.tax_total ?? 0m;
                var salesSubTotalBase = salesLineTotals?.sub_total_base ?? 0m;
                var salesTaxTotalBase = salesLineTotals?.tax_total_base ?? 0m;

                salesHeader.sub_total = salesSubTotal;
                salesHeader.tax_total = salesTaxTotal;
                salesHeader.grand_total = salesSubTotal + salesTaxTotal;
                salesHeader.total_taxable_value = salesSubTotal;
                salesHeader.sub_total_base = salesSubTotalBase;
                salesHeader.tax_total_base = salesTaxTotalBase;
                salesHeader.grand_total_base = salesSubTotalBase + salesTaxTotalBase;
                salesHeader.total_taxable_base = salesSubTotalBase;
                _context.so_SalesHeaders.Update(salesHeader);

                debugStep = "recalculate_customer_order";
                var ordersBySales = await _context.om_CustomerOrders
                    .Where(x => x.sales_id == salesId)
                    .ToListAsync();

                foreach (var orderBySales in ordersBySales)
                {
                    var orderTotals = await _context.om_CustomerOrderLines
                        .Where(x => x.customer_order_id == orderBySales.customer_order_id)
                        .GroupBy(_ => 1)
                        .Select(g => new
                        {
                            sub_total = g.Sum(x => x.line_total),
                            tax_total = g.Sum(x => x.tax_amount)
                        })
                        .FirstOrDefaultAsync();

                    var orderSubTotal = orderTotals?.sub_total ?? 0m;
                    var orderTaxTotal = orderTotals?.tax_total ?? 0m;

                    orderBySales.sub_total = orderSubTotal;
                    orderBySales.tax_amount = orderTaxTotal;
                    orderBySales.grand_total = orderSubTotal
                        + orderTaxTotal
                        + orderBySales.delivery_charge
                        + orderBySales.other_charges
                        - orderBySales.discount_amount;
                    orderBySales.updated_at = now;
                    orderBySales.updated_by = model.updated_by;
                }
                if (ordersBySales.Count > 0)
                {
                    _context.om_CustomerOrders.UpdateRange(ordersBySales);
                }

                debugStep = "save_changes";
                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                return new ServiceResult<update_quantity_result_dto>
                {
                    Status = 200,
                    Success = true,
                    Message = isIncrease ? "Quantity increased successfully" : "Quantity decreased successfully",
                    Data = new update_quantity_result_dto
                    {
                        customer_order_line_id = orderLine.customer_order_line_id,
                        old_qty = oldQty,
                        new_qty = newQty,
                        released_qty = isIncrease ? 0m : changedQty
                    }
                };
            }
            catch (DbUpdateException ex)
            {
                await tx.RollbackAsync();
                var innerMessage = GetInnermostExceptionMessage(ex);
                _logger.LogError(ex, "Error updating order quantity at step {DebugStep}. DB error: {InnerMessage}", debugStep, innerMessage);
                return new ServiceResult<update_quantity_result_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = $"[{debugStep}] {innerMessage}"
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error updating order quantity");
                return new ServiceResult<update_quantity_result_dto>
                {
                    Status = 500,
                    Success = false,
                    Message = $"[{debugStep}] {ex.Message}"
                };
            }
        }

        private static decimal Round4(decimal value)
        {
            return decimal.Round(value, 4, MidpointRounding.AwayFromZero);
        }

        private static string TruncateText(string? value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            var text = value.Trim();
            return text.Length <= maxLength ? text : text[..maxLength];
        }

        private static string GetInnermostExceptionMessage(Exception ex)
        {
            var current = ex;
            while (current.InnerException != null)
            {
                current = current.InnerException;
            }

            return string.IsNullOrWhiteSpace(current.Message) ? ex.Message : current.Message;
        }

        private async Task<string> GetNextSalesReturnNoAsync(Guid? businessId, Guid storeId)
        {
            var now = DateTime.Now;
            var store = await _context.st_stores.FirstOrDefaultAsync(x => x.store_id == storeId);
            var prefix = string.IsNullOrWhiteSpace(store?.default_invoice_init) ? "RET" : store.default_invoice_init;
            var fallback = $"{prefix}-RET-{now:yyyyMMddHHmmss}";

            if (!businessId.HasValue || businessId.Value == Guid.Empty)
            {
                return fallback;
            }

            var tableKey = await _context.am_table_next_key
                .FirstOrDefaultAsync(x => x.name == "so_SalesReturnLines" && x.business_id == businessId.Value);

            if (tableKey == null)
            {
                return fallback;
            }

            var next = Convert.ToInt32(tableKey.next_key) + 1;
            tableKey.next_key = next;
            _context.am_table_next_key.Update(tableKey);

            return $"{prefix}-{next}";
        }

        private async Task<string> UploadOrderFileAsync(IFormFile file, co_business business, Guid sourceId, string invoiceNo, string sourceType)
        {
            var bucketName = _configuration["Wasabi:BucketName"];
            if (string.IsNullOrWhiteSpace(bucketName))
                throw new InvalidOperationException("Wasabi bucket is not configured");

            var extension = Path.GetExtension(file.FileName);
            var safeFileName = $"{sourceType}_{sourceId}_{Guid.NewGuid()}{extension}";
            var key = $"faahi/company/{business.company_code}/{sourceType}/{invoiceNo}/{safeFileName}";

            using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = stream,
                ContentType = string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };

            await _s3Client.PutObjectAsync(request);
            return $"https://cdn.faahi.com/{key}";
        }
    }
}
