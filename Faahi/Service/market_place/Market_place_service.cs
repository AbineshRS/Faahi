using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Dto.am_users;
using Faahi.Dto.mk_blacklisted;
using Faahi.Dto.om_Orders;
using Faahi.Migrations;
using Faahi.Model.am_users;
using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.im_products;
using Faahi.Model.Order;
using Faahi.Model.site_settings;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Service.Email;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Faahi.Service.market_place
{
    public class Market_place_service : IMarket_place_service
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Market_place_service> _logger;
        private readonly IConfiguration _configuration;
        public Market_place_service(ApplicationDbContext context, ILogger<Market_place_service> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
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
                    source_name = a.First().source_name,
                    order_no = a.First().order_no,
                    zone_name = a.First().zone_name,

                    om_CustomerOrdersLine_Dtos = a.Where(a => a.customer_order_line_id != null)
                    .GroupBy(a => a.customer_order_line_id).Select(l => new om_CustomerOrdersLine_dto
                    {
                        customer_order_line_id = l.Key,
                        product_id = l.First().product_id,
                        title = l.First().title,
                        variant_id = l.First().variant_id,
                        image_url = a.First().image_url,
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
    }
}
