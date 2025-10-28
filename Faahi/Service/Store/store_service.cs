using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Service.Auth;
using Faahi.Service.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Faahi.Service.Store
{
    public class store_service : Istore
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<store_service> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthService _authService;
        public store_service(ApplicationDbContext context, ILogger<store_service> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AuthService authService)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public async Task<ServiceResult<Store_users>> Create_sellers(Store_users Store_users)
        {
            if (Store_users == null)
            {
                _logger.LogWarning(_logger.ToString(), "Create_sellers: st_sellers is null");
                return new ServiceResult<Store_users>
                {
                    Success = false,
                    Status = -1,
                    Message = "st_sellers cannot be null",
                };
            }
            try
            {
                var existingSeller = await _context.st_Users.Where(s => s.company_id == Store_users.company_id).ToListAsync();
                var co_business = await _context.co_business.FirstOrDefaultAsync(c => c.company_id == Store_users.company_id);
                //if (existingSeller.Count >= 1)
                //{
                //    return new ServiceResult<st_Users>
                //    {
                //        Success = false,
                //        Status = -1,
                //        Message = "Email Already exists"
                //    };
                //}
                if (existingSeller.Count>= co_business.sites_users_allowed)
                {
                    return new ServiceResult<Store_users>
                    {
                        Success = false,
                        Status = 0,
                        Message = "Seller limit exists",
                    };
                }
                st_Users st_Users = new st_Users();
                st_Users.user_id = Guid.CreateVersion7();
                st_Users.company_id = Store_users.company_id;
                st_Users.Full_name = Store_users.Full_name;
                st_Users.email = Store_users.email;
                st_Users.phone = Store_users.phone;
                if (st_Users.password == null)
                {
                    st_Users.password = null;
                }
                else
                {
                    st_Users.password = PasswordHelper.HashPassword(st_Users.password);

                }
                st_Users.account_type = Store_users.account_type;
                st_Users.registration_date = DateTime.Now;
                st_Users.status = Store_users.status;

                await _context.st_Users.AddAsync(st_Users);

                st_UserStoreAccess st_UserStoreAccess = new st_UserStoreAccess();
                st_UserStoreAccess.store_access_id = Guid.CreateVersion7();
                st_UserStoreAccess.user_id = st_Users.user_id;
                st_UserStoreAccess.store_id = Store_users.store_id;
                st_UserStoreAccess.role_id = Store_users.role_id;
                st_UserStoreAccess.created_at = DateTime.Now;
                await _context.st_UserStoreAccess.AddAsync(st_UserStoreAccess);

                await _context.SaveChangesAsync();

                var email_exist = await _context.st_Users.FirstOrDefaultAsync(a => a.email == st_Users.email);
                if (email_exist != null)
                {
                    var email_auth = await _authService.email_verification(st_Users.email, "st-seller");

                }


                return new ServiceResult<Store_users>
                {
                    Success = true,
                    Status = 1,
                    Message = "Seller created successfully",
                    Data = Store_users
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create_sellers: Exception occurred while creating seller");
                return new ServiceResult<Store_users>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while creating the seller",
                };
            }


        }

        public async Task<ServiceResult<st_stores>> Create_stores(st_stores st_stores)
        {
            if (st_stores == null)
            {
                _logger.LogWarning(_logger.ToString(), "Create_stores: st_stores is null");
                return new ServiceResult<st_stores>
                {
                    Success = false,
                    Status = -1,
                    Message = "st_stores cannot be null",
                };
            }
            try
            {
                var existingStore = await _context.st_stores.Where(s => s.company_id == st_stores.company_id).ToListAsync();
                var co_business = await _context.co_business.FirstOrDefaultAsync(c => c.company_id == st_stores.company_id);
                if (existingStore.Count >= co_business.sites_allowed)
                {
                    return new ServiceResult<st_stores>
                    {
                        Success = false,
                        Status = -1,
                        Message = "Store limit reached for this company",
                    };
                }

                st_stores.store_id = Guid.CreateVersion7();
                st_stores.company_id = st_stores.company_id;
                st_stores.store_name = st_stores.store_name;
                st_stores.store_location = st_stores.store_location;
                st_stores.store_type = st_stores.store_type;
                st_stores.created_at = DateTime.Now;
                st_stores.status = st_stores.status;
                await _context.st_stores.AddAsync(st_stores);
                await _context.SaveChangesAsync();

                if (co_business != null)
                {
                    co_business.createdSites += 1;
                    _context.co_business.Update(co_business);
                    _context.SaveChanges();
                }

                return new ServiceResult<st_stores>
                {
                    Success = true,
                    Status = 1,
                    Message = "Store created successfully",
                    Data = st_stores
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create_stores: Exception occurred while creating store");
                return new ServiceResult<st_stores>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while creating the store",
                };
            }
        }
        public async Task<ServiceResult<List<st_stores>>> Get_store(Guid company_id)
        {
            try
            {
                var stores = await _context.st_stores.Where(s => s.company_id == company_id).ToListAsync();
                if (stores == null || stores.Count == 0)
                {
                    return new ServiceResult<List<st_stores>>
                    {
                        Success = false,
                        Status = 0,
                        Message = "No stores found for the given company_id",
                    };
                }
                return new ServiceResult<List<st_stores>>
                {
                    Success = true,
                    Status = 1,
                    Message = "Stores retrieved successfully",
                    Data = stores
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get_store: Exception occurred while retrieving stores");
                return new ServiceResult<List<st_stores>>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while retrieving the stores",
                };
            }
        }
        public async Task<ServiceResult<List<st_Users>>> Get_seller(Guid company_id)
        {
            try
            {
                var sellers = await _context.st_Users.Where(s => s.company_id == company_id).ToListAsync();
                if (sellers == null || sellers.Count == 0)
                {
                    return new ServiceResult<List<st_Users>>
                    {
                        Success = false,
                        Status = 0,
                        Message = "No sellers found for the given company_id",
                    };
                }
                return new ServiceResult<List<st_Users>>
                {
                    Success = true,
                    Status = 1,
                    Message = "Sellers retrieved successfully",
                    Data = sellers
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get_seller: Exception occurred while retrieving sellers");
                return new ServiceResult<List<st_Users>>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while retrieving the sellers",
                };
            }
        }
        public async Task<ServiceResult<st_Users>> Seller_update_password(string token, string email, string password)
        {
            try
            {
                var emailVerification = await _context.am_emailVerifications
                    .FirstOrDefaultAsync(ev => ev.email == email && ev.token == token && ev.userType == "st-seller");
                if (emailVerification == null)
                {
                    return new ServiceResult<st_Users>
                    {
                        Success = false,
                        Status = -1,
                        Message = "Invalid token or email",
                    };
                }
                var seller = await _context.st_Users.FirstOrDefaultAsync(s => s.email == email);
                var co_business = await _context.co_business.FirstOrDefaultAsync(a => a.company_id == seller.company_id);
                if (co_business != null)
                {
                    co_business.createdSites_users += 1;
                    _context.co_business.Update(co_business);
                }
                if (seller == null)
                {
                    return new ServiceResult<st_Users>
                    {
                        Success = false,
                        Status = -2,
                        Message = "Seller not found",
                    };
                }
                seller.password = PasswordHelper.HashPassword(password);
                _context.st_Users.Update(seller);
                await _context.SaveChangesAsync();
                var emailService = new EmailService(_configuration);

                string subject = "Your Faahi Seller Account Credentials";

                string body = $@"
                                        <html>
                                        <head>
                                            <style>
                                                body {{
                                                    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                                    background-color: #f9fafc;
                                                    color: #333;
                                                    margin: 0;
                                                    padding: 0;
                                                }}
                                                .container {{
                                                    max-width: 600px;
                                                    margin: 40px auto;
                                                    background: #ffffff;
                                                    border-radius: 10px;
                                                    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
                                                    padding: 30px;
                                                }}
                                                h2 {{
                                                    color: #2c3e50;
                                                    text-align: center;
                                                }}
                                                .info {{
                                                    background: #f2f6fc;
                                                    border: 1px solid #e3eaf5;
                                                    border-radius: 8px;
                                                    padding: 15px;
                                                    margin-top: 20px;
                                                }}
                                                .info p {{
                                                    font-size: 15px;
                                                    margin: 8px 0;
                                                }}
                                                .footer {{
                                                    text-align: center;
                                                    font-size: 12px;
                                                    color: #999;
                                                    margin-top: 30px;
                                                }}
                                                .highlight {{
                                                    color: #007bff;
                                                    font-weight: 600;
                                                }}
                                            </style>
                                        </head>
                                        <body>
                                            <div class='container'>
                                                <h2>Faahi Seller Account Updated</h2>
                                                <p>Hello <strong>{seller.Full_name}</strong>,</p>
                                                <p>Your seller account password has been successfully updated. Please find your login credentials below:</p>

                                                <div class='info'>
                                                    <p><strong>Username:</strong> <span class='highlight'>{email}</span></p>
                                                    <p><strong> Password:</strong> <span class='highlight'>{password}</span></p>
                                                </div>

                                                <p>We recommend you log in and change your password again for security purposes.</p>

                                                <p style='margin-top:20px;'>Click below to log in to your account:</p>
                                                <p style='text-align:center;'>
                                                    <a href='{_configuration["MailSettings:SellerLoginUrl"]}' 
                                                       style='background-color:#007bff;color:white;padding:10px 20px;text-decoration:none;border-radius:5px;'>
                                                       Login to Seller Dashboard
                                                    </a>
                                                </p>

                                                <div class='footer'>
                                                    <p>© {DateTime.UtcNow.Year} Faahi. All rights reserved.</p>
                                                </div>
                                            </div>
                                        </body>
                                        </html>";

                await emailService.SendEmailAsync(email, subject, body);
                return new ServiceResult<st_Users>
                {
                    Success = true,
                    Status = 1,
                    Message = "Password updated successfully",
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Seller_update_password: Exception occurred while updating password");
                return new ServiceResult<st_Users>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while updating the password",
                };
            }
        }
        public async Task<ServiceResult<st_UserRoles>> Create_roles(st_UserRoles st_UserRoles)
        {
            if (st_UserRoles == null)
            {
                _logger.LogWarning(_logger.ToString(), "Create_roles: st_UserRoles is null");
                return new ServiceResult<st_UserRoles>
                {
                    Success = false,
                    Status = -1,
                    Message = "st_UserRoles cannot be null",
                };
            }
            try
            {
                var existingRole = await _context.st_UserRoles.FirstOrDefaultAsync(r => r.role_name == st_UserRoles.role_name && r.company_id == st_UserRoles.company_id);
                if (existingRole != null)
                {
                    return new ServiceResult<st_UserRoles>
                    {
                        Success = false,
                        Status = -1,
                        Message = "Role already exists for this company",
                    };
                }

                st_UserRoles.role_id = Guid.CreateVersion7();
                st_UserRoles.company_id = st_UserRoles.company_id;
                st_UserRoles.role_name = st_UserRoles.role_name;
                st_UserRoles.description = st_UserRoles.description;
                await _context.st_UserRoles.AddAsync(st_UserRoles);
                await _context.SaveChangesAsync();
                return new ServiceResult<st_UserRoles>
                {
                    Success = true,
                    Status = 1,
                    Message = "Role created successfully",
                    Data = st_UserRoles
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create_roles: Exception occurred while creating role");
                return new ServiceResult<st_UserRoles>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while creating the role",
                };
            }
        }
        public async Task<ServiceResult<List<st_UserRoles>>> Get_roles_by_company_id(Guid company_id)
        {
            try
            {
                var roles = await _context.st_UserRoles.Where(a=>a.company_id==company_id).ToListAsync();
                if (roles == null || roles.Count == 0)
                {
                    return new ServiceResult<List<st_UserRoles>>
                    {
                        Success = false,
                        Status = 0,
                        Message = "No roles found",
                    };
                }
                return new ServiceResult<List<st_UserRoles>>
                {
                    Success = true,
                    Status = 1,
                    Message = "Roles retrieved successfully",
                    Data = roles
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get_roles: Exception occurred while retrieving roles");
                return new ServiceResult<List<st_UserRoles>>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while retrieving the roles",
                };
            }
        }
        public async Task<ServiceResult<st_UserStoreAccess>> Create_store_access(st_UserStoreAccess st_UserStoreAccess)
        {
            if (st_UserStoreAccess == null)
            {
                _logger.LogWarning(_logger.ToString(), "Create_store_access: st_UserStoreAccess is null");
                return new ServiceResult<st_UserStoreAccess>
                {
                    Success = false,
                    Status = -1,
                    Message = "st_UserStoreAccess cannot be null",
                };
            }
            try
            {

                st_UserStoreAccess.store_access_id = Guid.CreateVersion7();
                st_UserStoreAccess.user_id = st_UserStoreAccess.user_id;
                st_UserStoreAccess.store_id = st_UserStoreAccess.store_id;
                st_UserStoreAccess.role_id = st_UserStoreAccess.role_id;
                st_UserStoreAccess.created_at = DateTime.Now;
                await _context.st_UserStoreAccess.AddAsync(st_UserStoreAccess);
                await _context.SaveChangesAsync();
                return new ServiceResult<st_UserStoreAccess>
                {
                    Success = true,
                    Status = 1,
                    Message = "Store access created successfully",
                    Data = st_UserStoreAccess
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create_store_access: Exception occurred while creating store access");
                return new ServiceResult<st_UserStoreAccess>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while creating the store access",
                };
            }
        }
    }

}
