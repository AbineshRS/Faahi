using AutoMapper;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Migrations;
using Faahi.Model.Email_verify;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Service.Auth;
using Faahi.Service.Email;
using Faahi.View_Model.store;
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
        private readonly IMapper _mapper;
        public store_service(ApplicationDbContext context, ILogger<store_service> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, AuthService authService, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _mapper = mapper;
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

                var st_sotore_access = await _context.st_UserStoreAccess.Where(a => a.store_id == Store_users.store_id).ToListAsync();
                var user_ids = st_sotore_access.Select(s => s.user_id).ToList();

                var st_store_users = await _context.st_UserStoreAccess.Where(a => a.user_id == Store_users.user_id).ToListAsync();


                List<st_UserStoreAccess> existing_users = new List<st_UserStoreAccess>();
                var existingSeller = await _context.st_Users.FirstOrDefaultAsync(s => s.email == Store_users.email);
                if (existingSeller != null)
                {
                    existing_users = await _context.st_UserStoreAccess.Where(a => a.user_id == existingSeller.user_id).ToListAsync();

                }



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
                if (user_ids.Count >= co_business.sites_users_allowed)
                {
                    return new ServiceResult<Store_users>
                    {
                        Success = false,
                        Status = -3,
                        Message = "Seller limit exists",
                    };
                }
                var existingUserIds = existing_users.Select(u => u.user_id).ToList();

                var st_userAccesstore = _context.st_UserStoreAccess
                    .Where(a => a.store_id == Store_users.store_id &&
                                a.role_id == Store_users.role_id)
                    .AsEnumerable()
                    .FirstOrDefault(a => existingUserIds.Contains(a.user_id));

                if (st_userAccesstore != null)
                {
                    return new ServiceResult<Store_users>
                    {
                        Success = false,
                        Status = -2,
                        Message = "Seller with this role already exists for the store",
                    };
                }

                if (existing_users.Count >= 1)
                {
                    st_UserStoreAccess st_UserStoreAccess = new st_UserStoreAccess();
                    st_UserStoreAccess.store_access_id = Guid.CreateVersion7();
                    st_UserStoreAccess.user_id = existingSeller.user_id;
                    st_UserStoreAccess.store_id = Store_users.store_id;
                    st_UserStoreAccess.role_id = Store_users.role_id;
                    st_UserStoreAccess.created_at = DateTime.Now;
                    st_UserStoreAccess.status = Store_users.status;
                    await _context.st_UserStoreAccess.AddAsync(st_UserStoreAccess);
                }
                else
                {
                    st_Users st_Users = new st_Users();
                    st_Users.user_id = Guid.CreateVersion7();
                    st_Users.company_id = Store_users.company_id;
                    st_Users.Full_name = Store_users.Full_name;
                    st_Users.email = Store_users.email;
                    st_Users.phone = Store_users.phone;
                    if (co_business != null)
                    {
                        co_business.createdSites_users += 1;
                        _context.co_business.Update(co_business);
                    }
                    else if (st_Users.password == null)
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
                    st_UserStoreAccess.status = Store_users.status;
                    await _context.st_UserStoreAccess.AddAsync(st_UserStoreAccess);
                    var email_exist = await _context.st_Users.FirstOrDefaultAsync(a => a.email == st_Users.email);
                    if (email_exist == null)
                    {
                        var email_auth = await _authService.email_verification(st_Users.email, "st-seller");

                    }
                }


                await _context.SaveChangesAsync();

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

        public async Task<ServiceResult<st_stores>> Create_stores(st_stores store_Add)
        {
            if (store_Add == null)
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
                var existingStore = await _context.st_stores.Where(s => s.company_id == store_Add.company_id).ToListAsync();
                var co_business = await _context.co_business.FirstOrDefaultAsync(c => c.company_id == store_Add.company_id);
                if (existingStore.Count >= co_business.sites_allowed)
                {
                    return new ServiceResult<st_stores>
                    {
                        Success = false,
                        Status = -1,
                        Message = "Store limit reached for this company",
                    };
                }
                st_stores st_Stores = new st_stores();
                st_Stores.store_id = Guid.CreateVersion7();
                st_Stores.company_id = store_Add.company_id;
                st_Stores.store_name = store_Add.store_name;
                st_Stores.store_location = store_Add.store_location;
                st_Stores.store_type = store_Add.store_type;
                st_Stores.created_at = DateTime.Now;
                st_Stores.status = store_Add.status;

                store_Add.store_id = st_Stores.store_id;
                store_Add.created_at = st_Stores.created_at;

                st_Stores.st_StoresAddres = new List<st_StoresAddres>();
                foreach (var st_address in store_Add.st_StoresAddres)
                {
                    st_StoresAddres _StoresAddres = new st_StoresAddres();

                    _StoresAddres.store_address_id = Guid.CreateVersion7();
                    _StoresAddres.store_id = st_Stores.store_id;
                    _StoresAddres.line1 = st_address.line1;
                    _StoresAddres.line2 = st_address.line2;
                    _StoresAddres.city = st_address.city;
                    _StoresAddres.region = st_address.region;
                    _StoresAddres.postal_code = st_address.postal_code;
                    _StoresAddres.country = st_address.country;
                    _StoresAddres.address_type = st_address.address_type;
                    _StoresAddres.valid_from = DateTime.Now;
                    _StoresAddres.created_at = DateTime.Now;
                    _StoresAddres.is_current = "T";
                    st_Stores.st_StoresAddres.Add(_StoresAddres);

                }

                await _context.st_stores.AddAsync(st_Stores);
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
                    Data = store_Add
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
        public async Task<ServiceResult<List<st_store_view>>> Get_store(Guid company_id)
        {
            if (company_id == null)
            {
                _logger.LogWarning("Get_store called with null company_id");
                return new ServiceResult<List<st_store_view>>
                {
                    Success = false,
                    Message = "company_id cannot be null",
                    Status = -1,
                };
            }
            try
            {

                var st_Stores = await _context.st_stores.Where(s => s.company_id == company_id && s.status == "T").ToListAsync();

                List<st_store_view> storeViews_list = new List<st_store_view>();
                foreach (var store in st_Stores)
                {
                    var store_view = _mapper.Map<st_store_view>(store);
                    store_view.st_StoreCategories = new List<st_StoreCategories_view>();

                    var storecategories = await _context.st_StoreCategories.Where(c => c.store_id == store.store_id).ToListAsync();

                    foreach (var category in storecategories)
                    {
                        var category_view = _mapper.Map<st_StoreCategories_view>(category);
                        category_view.im_ProductCategories_view = new List<im_ProductCategories_view>();

                        var category_details = await _context.im_ProductCategories.Where(p => p.category_id == category.category_id).ToListAsync();

                        foreach (var detail in category_details)
                        {
                            var detail_view = _mapper.Map<im_ProductCategories_view>(detail);
                            category_view.im_ProductCategories_view.Add(detail_view);
                        }
                        store_view.st_StoreCategories.Add(category_view);
                    }
                    storeViews_list.Add(store_view);
                }


                if (storeViews_list == null || storeViews_list.Count == 0)
                {
                    return new ServiceResult<List<st_store_view>>
                    {
                        Success = false,
                        Status = 0,
                        Message = "No stores found for the given company_id",
                    };
                }
                return new ServiceResult<List<st_store_view>>
                {
                    Success = true,
                    Status = 1,
                    Message = "Stores retrieved successfully",
                    Data = storeViews_list
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get_store: Exception occurred while retrieving stores");
                return new ServiceResult<List<st_store_view>>
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
                var stores = await _context.st_stores.Where(s => s.company_id == company_id).ToListAsync();
                var st_store_acces = _context.st_UserStoreAccess.AsEnumerable().Where(a => stores.Select(s => s.store_id).Contains(a.store_id)).ToList();
                var user_ids = st_store_acces.Select(s => s.user_id).ToList();
                var sellers = _context.st_Users.AsEnumerable().Where(a => a.user_id != null && user_ids.Contains(a.user_id.Value)).ToList();

                //var sellers = await _context.st_Users.Where(s => s.user_id == company_id).ToListAsync();
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
        public async Task<ServiceResult<List<st_StoreCategories>>> Create_StoreCategories(List<st_StoreCategories> st_StoreCategories)
        {
            if (st_StoreCategories == null)
            {
                _logger.LogWarning("Create_StoreCategories called with null st_StoreCategories");
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = false,
                    Message = "NO data found to insert",
                    Status = -1,
                };
            }
            try
            {
                var existingcategories = await _context.st_StoreCategories.Where(a => a.store_id == st_StoreCategories.First().store_id && a.category_id == st_StoreCategories.First().category_id).ToListAsync();
                if (existingcategories.Any())
                {
                    return new ServiceResult<List<st_StoreCategories>>
                    {
                        Success = false,
                        Message = "Some categories are already assigned to this store.",
                        Status = -2
                    };
                }

                foreach (var category in st_StoreCategories)
                {
                    category.store_category_id = Guid.CreateVersion7();
                    category.store_id = category.store_id;
                    category.category_id = category.category_id;
                    category.is_selected = category.is_selected;
                    _context.st_StoreCategories.Add(category);

                }

                await _context.SaveChangesAsync();
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = st_StoreCategories
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating store category");
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
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
        public async Task<ServiceResult<List<st_UserRoles>>> Get_roles_by_company_id()
        {
            try
            {
                var roles = await _context.st_UserRoles.ToListAsync();
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
        public async Task<ServiceResult<List<st_stores>>> Get_store_by_email(string email)
        {
            try
            {
                List<st_stores> storeList = new List<st_stores>();
                var storeAccessList = await _context.st_Users.Where(a => a.email == email).ToListAsync();
                if (storeAccessList.Count >= 1)
                {
                    var st_store = await _context.st_UserStoreAccess.Where(s => s.user_id == storeAccessList.FirstOrDefault().user_id).ToListAsync();
                    var storeIds = st_store.Select(s => s.store_id).ToList();

                    storeList = _context.st_stores.AsEnumerable()
                          .Where(a => storeIds.Contains(a.store_id))
                          .ToList();
                }

                return new ServiceResult<List<st_stores>>
                {
                    Success = true,
                    Status = 1,
                    Message = "Store access retrieved successfully",
                    Data = storeList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get_store_by_email: Exception occurred while retrieving store access");
                return new ServiceResult<List<st_stores>>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while retrieving the store access",
                };
            }
        }
        public async Task<ServiceResult<st_UserRoles>> Get_userrole(Guid user_id, Guid store_id)
        {
            try
            {
                var storeAccess = await _context.st_UserStoreAccess
                    .FirstOrDefaultAsync(s => s.user_id == user_id && s.store_id == store_id);
                if (storeAccess == null)
                {
                    return new ServiceResult<st_UserRoles>
                    {
                        Success = false,
                        Status = 0,
                        Message = "No store access found for the given user_id and store_id",
                    };
                }
                var store = await _context.st_UserRoles.FirstOrDefaultAsync(s => s.role_id == storeAccess.role_id);
                return new ServiceResult<st_UserRoles>
                {
                    Success = true,
                    Status = 1,
                    Message = "Store access retrieved successfully",
                    Data = store
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get_userrole: Exception occurred while retrieving store access");
                return new ServiceResult<st_UserRoles>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while retrieving the store access",
                };
            }
        }
        public async Task<ServiceResult<st_store_view>> Get_store_by_storeid(Guid store_id)
        {
            try
            {
                var store = await _context.st_stores.Include(a => a.st_StoresAddres.Where(a=>a.is_current=="T")).FirstOrDefaultAsync(s => s.store_id == store_id);
                if (store == null)
                {
                    return new ServiceResult<st_store_view>
                    {
                        Success = false,
                        Status = 0,
                        Message = "No store found for the given store_id",
                    };
                }
                st_store_view storeViews_list = new st_store_view();
                List<st_store_view> storeViews = new List<st_store_view>();
                var store_view = _mapper.Map<st_store_view>(store);
                store_view.st_StoreCategories = new List<st_StoreCategories_view>();
                var storecategories = await _context.st_StoreCategories.Where(c => c.store_id == store.store_id).ToListAsync();
                foreach (var store_cat in storecategories)
                {
                    var store_category_view = _mapper.Map<st_StoreCategories_view>(store_cat);
                    store_category_view.im_ProductCategories_view = new List<im_ProductCategories_view>();

                    var category_details = await _context.im_ProductCategories.Where(p => p.category_id == store_cat.category_id).ToListAsync();
                    foreach (var detail in category_details)
                    {
                        var detail_view = _mapper.Map<im_ProductCategories_view>(detail);
                        store_category_view.im_ProductCategories_view.Add(detail_view);
                    }
                    store_view.st_StoreCategories.Add(store_category_view);

                }



                return new ServiceResult<st_store_view>
                {
                    Success = true,
                    Status = 1,
                    Message = "Store retrieved successfully",
                    Data = store_view
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get_store_by_storeid: Exception occurred while retrieving store");
                return new ServiceResult<st_store_view>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while retrieving the store",
                };
            }
        }
        public async Task<ServiceResult<st_stores>> Update_store(Guid store_id, st_stores st_Stores)
        {
            if (st_Stores == null || store_id == null)
            {
                _logger.LogWarning(_logger.ToString(), "Update_store: st_Stores is null,store_id is null");
                return new ServiceResult<st_stores>
                {
                    Success = false,
                    Status = -1,
                    Message = "st_Stores cannot be null",
                };
            }

            var existingStore = await _context.st_stores.Include(a => a.st_StoresAddres).FirstOrDefaultAsync(s => s.store_id == store_id);
            if (existingStore == null)
            {
                return new ServiceResult<st_stores>
                {
                    Success = false,
                    Status = 0,
                    Message = "Store not found",
                };
            }
            try
            {
                st_stores st_Stores1 = new st_stores();
                st_Stores1 = existingStore;
                existingStore.store_name = st_Stores.store_name;
                existingStore.store_location = st_Stores.store_location;
                existingStore.store_type = st_Stores.store_type;
                existingStore.status = st_Stores.status;
                //existingStore.st_StoresAddres = new List<st_StoresAddres>();
                foreach (var st_address in st_Stores.st_StoresAddres)
                {
                    var existingAddress = existingStore.st_StoresAddres.FirstOrDefault(a => a.store_address_id == st_address.store_address_id);
                    if (existingAddress != null)
                    {
                        existingAddress.line1 = st_address.line1;
                        existingAddress.line2 = st_address.line2;
                        existingAddress.city = st_address.city;
                        existingAddress.region = st_address.region;
                        existingAddress.postal_code = st_address.postal_code;
                        existingAddress.country = st_address.country;
                        existingAddress.address_type = st_address.address_type;
                        _context.st_StoresAddres.Update(existingAddress);
                    }
                    else
                    {
                        st_StoresAddres st_StoresAddres = new st_StoresAddres();
                        st_StoresAddres.store_address_id = Guid.CreateVersion7();
                        st_StoresAddres.store_id = existingStore.store_id;
                        st_StoresAddres.address_type = st_address.address_type;
                        st_StoresAddres.line1 = st_address.line1;
                        st_StoresAddres.line2 = st_address.line2;
                        st_StoresAddres.city = st_address.city;
                        st_StoresAddres.region = st_address.region;
                        st_StoresAddres.country = st_address.country;
                        st_StoresAddres.postal_code = st_address.postal_code;
                        st_StoresAddres.valid_from = DateTime.Now;
                        st_StoresAddres.created_at = DateTime.Now;
                        st_StoresAddres.is_current = "T";
                        _context.st_StoresAddres.Add(st_StoresAddres);
                        st_Stores1.st_StoresAddres.Add(st_StoresAddres);
                    }

                }
                existingStore.st_StoresAddres = st_Stores1.st_StoresAddres;

                _context.st_stores.Update(existingStore);
                await _context.SaveChangesAsync();
                return new ServiceResult<st_stores>
                {
                    Success = true,
                    Status = 1,
                    Message = "Store updated successfully",
                    Data = st_Stores
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update_store: Exception occurred while updating store");
                return new ServiceResult<st_stores>
                {
                    Success = false,
                    Status = -1,
                    Message = "An error occurred while updating the store",
                };
            }
        }
        public async Task<ServiceResult<List<st_StoreCategories>>> update_category(List<st_StoreCategories> st_StoreCategories)
        {
            if (st_StoreCategories == null)
            {
                _logger.LogWarning("update_category called with null st_StoreCategories");
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = false,
                    Message = "NO data found to update",
                    Status = -1,
                };
            }
            try
            {
                //Delete removed categories
                var storeId = st_StoreCategories.First().store_id;
                var existingcategories = await _context.st_StoreCategories.Where(a => a.store_id == storeId).ToListAsync();
                var newCategoryIds = st_StoreCategories.Select(c => c.category_id).ToList();
                var deletedCategories = existingcategories.Where(c => !newCategoryIds.Contains(c.category_id)).ToList();
                if (deletedCategories.Any())
                {
                    foreach (var category in deletedCategories)
                    {
                        _context.st_StoreCategories.Remove(category);
                    }
                    await _context.SaveChangesAsync();
                }


                foreach (var category in st_StoreCategories)
                {
                    var existingCategory = await _context.st_StoreCategories.FirstOrDefaultAsync(c => c.store_category_id == category.store_category_id);
                    if (existingCategory == null)
                    {
                        //Add new category
                        category.store_category_id = Guid.CreateVersion7();
                        category.category_id = category.category_id;
                        category.store_id = category.store_id;
                        category.is_selected = "T";
                        _context.st_StoreCategories.Add(category);
                        await _context.SaveChangesAsync();


                    }
                    if (existingCategory != null)
                    {
                        //Update existing category
                        existingCategory.category_id = category.category_id;
                        _context.st_StoreCategories.Update(existingCategory);
                        await _context.SaveChangesAsync();
                    }
                }
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = st_StoreCategories
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating store category");
                return new ServiceResult<List<st_StoreCategories>>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }
        }
        public async Task<ServiceResult<st_StoresAddres>> add_sub_address(Guid store_id, st_StoresAddres st_StoresAddres)
        {
            if (store_id == null)
            {
                return new ServiceResult<st_StoresAddres>
                {
                    Status = -1,
                    Success = false,
                    Message = "NO data found",
                };
            }
            try
            {
                var exising_data = await _context.st_stores.Include(a => a.st_StoresAddres).FirstOrDefaultAsync(a => a.store_id == store_id);
                if (exising_data == null)
                {
                    return new ServiceResult<st_StoresAddres>
                    {
                        Status = -2,
                        Success = false
                    };

                }

                foreach (var st_address in exising_data.st_StoresAddres)
                {
                    var exising_address = exising_data.st_StoresAddres.FirstOrDefault(a => a.store_address_id == st_address.store_address_id);
                    if (exising_address != null)
                    {
                        exising_address.valid_to = DateTime.Now;
                        exising_address.is_current = "F";
                        _context.st_StoresAddres.Update(exising_address);
                        await _context.SaveChangesAsync();

                    }

                }

                st_stores st_Stores1 = new st_stores();
                st_Stores1 = exising_data;
                st_StoresAddres st_StoresAddress = new st_StoresAddres();
                st_StoresAddress.store_address_id = Guid.CreateVersion7();
                st_StoresAddress.store_id = exising_data.store_id;
                st_StoresAddress.address_type = st_StoresAddres.address_type;
                st_StoresAddress.line1 = st_StoresAddres.line1;
                st_StoresAddress.line2 = st_StoresAddres.line2;
                st_StoresAddress.city = st_StoresAddres.city;
                st_StoresAddress.region = st_StoresAddres.region;
                st_StoresAddress.country = st_StoresAddres.country;
                st_StoresAddress.postal_code = st_StoresAddres.postal_code;
                st_StoresAddress.valid_from = DateTime.Now;
                st_StoresAddress.created_at = DateTime.Now;
                st_StoresAddress.is_current = "T";
                _context.st_StoresAddres.Add(st_StoresAddress);

                st_Stores1.st_StoresAddres.Add(st_StoresAddress);

                exising_data.st_StoresAddres = st_Stores1.st_StoresAddres;

                _context.st_stores.Update(exising_data);
                await _context.SaveChangesAsync();

                return new ServiceResult<st_StoresAddres>
                {
                    Status = 1,
                    Success = true,
                    Message = "Addede",
                    Data = st_StoresAddres
                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating store ");
                return new ServiceResult<st_StoresAddres>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }
            


        }
        public async Task<ServiceResult<st_stores>> Delete_store(Guid store_id)
        {
            if (store_id == null)
            {
                _logger.LogWarning("Store Id was Null");

                return new ServiceResult<st_stores>
                {
                    Status = -1,
                    Message = "not found",

                };
            }
            try
            {
                var existing = await _context.st_stores.FirstOrDefaultAsync(a => a.store_id == store_id);
                existing.status = "F";
                _context.st_stores.Update(existing);
                await _context.SaveChangesAsync();
                return new ServiceResult<st_stores>
                {
                    Status = 1,
                    Success = true,
                    Message = "Success Deleted",
                    Data = existing,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Deleteing store ");
                return new ServiceResult<st_stores>
                {
                    Success = false,
                    Message = "An error occurred while processing your request.",
                    Status = -1,
                };
            }

        }
    }

}
