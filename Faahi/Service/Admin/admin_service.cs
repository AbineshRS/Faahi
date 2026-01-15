using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.Admin;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Faahi.Service.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Faahi.Service.Admin
{
    public class admin_service : Iadmin
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<admin_service> _logger;

        public admin_service(ApplicationDbContext context, IConfiguration configuration, ILogger<admin_service> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<ServiceResult<super_admin>> AddAdminAsync(super_admin admin)
        {
            if (admin == null)
            {
                _logger.LogError("Admin data is null in AddAdminAsync method");
                return new ServiceResult<super_admin>
                {
                    Status = 400,
                    Success = false,
                    Message = "Admin data is null",
                    Data = null
                };
            }
            try
            {
                admin.super_admin_id = Guid.CreateVersion7();
                admin.email = admin.email;
                admin.name = admin.name;
                admin.password = PasswordHelper.HashPassword(admin?.password);
                admin.phone = admin.phone;
                admin.created_at = admin.created_at;
                admin.updated_at = admin.updated_at;
                admin.user_type = admin.user_type;
                admin.status = admin.status;
                await _context.super_admin.AddAsync(admin);
                await _context.SaveChangesAsync();
                return new ServiceResult<super_admin>
                {
                    Status = 201,
                    Success = true,
                    Message = "Admin added successfully",
                    Data = admin
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in AddAdminAsync method");
                return new ServiceResult<super_admin>
                {
                    Status = 500,
                    Success = false,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }

        public async Task<AuthResponse> LoginAsyn(string username, string password)
        {
            if (username == null || password == null)
            {
                return new AuthResponse
                {
                    status = 0,
                    AccessToken = null,
                    RefreshToken = null
                };
            }
            var user = _context.super_admin.FirstOrDefault(u => u.email == username);
            var has_pssword = PasswordHelper.VerifyPassword(password, user.password);
            if (has_pssword == false)
            {
                return new AuthResponse
                {
                    status = -1,
                    AccessToken = null,
                    RefreshToken = null
                };
            }

            var accessToken_site_users = CreatTokensite(user, 15);
            var refreshToken_site_users = CreatTokensite(user, 10080);
            return new AuthResponse
            {
                status = 2,
                AccessToken = accessToken_site_users,
                RefreshToken = refreshToken_site_users
            };
        }
        private string CreatTokensite(super_admin user, int minutes)
        {


            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.super_admin_id.ToString() ?? ""), // important for RefreshToken
                 new Claim(ClaimTypes.Name, user.super_admin_id.ToString() ?? ""),
                 new Claim("userId", user.super_admin_id.ToString() ?? ""),
                 new Claim("company_id", user.super_admin_id.ToString() ?? ""),
                 new Claim("userRole", user.user_type.ToString() ?? ""),
                 new Claim("FullName", user.name.ToString() ?? "")
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokendescription = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims = claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokendescription);
        }

        public async Task<ServiceResult<sa_country_regions>> Addcountry(sa_country_regions regions)
        {
            if (regions == null)
            {
                _logger.LogError("Regions data is null in AddRegionsAsync method");
                return new ServiceResult<sa_country_regions>
                {
                    Status = 400,
                    Success = false,
                    Message = "Regions data is null",
                    Data = null
                };
            }
            try
            {
                var existingCountry = await _context.sa_country_regions.Include(a=>a.sa_regions)
                    .FirstOrDefaultAsync(c => c.avl_countries_id == regions.avl_countries_id);
                if (existingCountry == null)
                {
                    regions.country_region_id = Guid.CreateVersion7();
                    regions.avl_countries_id = regions.avl_countries_id;
                    regions.region_name = regions.region_name;
                    regions.status = regions.status;
                    foreach (var region in regions.sa_regions)
                    {
                        region.region_id = Guid.CreateVersion7();
                        region.country_region_id = regions.country_region_id;
                        region.region_name = region.region_name;
                        region.parent_id = regions.avl_countries_id;
                        _context.sa_regions.Add(region);
                        await _context.sa_country_regions.AddAsync(regions);
                    }
                }
                else
                {
                    foreach (var region in regions.sa_regions)
                    {
                        region.region_id = Guid.CreateVersion7();
                        region.country_region_id = existingCountry.country_region_id;
                        region.region_name = region.region_name;
                        region.parent_id = existingCountry.avl_countries_id;
                        _context.sa_regions.Add(region);
                        existingCountry.sa_regions.Add(region);
                         //_context.sa_country_regions.Update(existingCountry);
                    }
                }

                await _context.SaveChangesAsync();
                return new ServiceResult<sa_country_regions>
                {
                    Status = 201,
                    Success = true,
                    Message = "Regions added successfully",
                    Data = regions
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in AddRegionsAsync method");
                return new ServiceResult<sa_country_regions>
                {
                    Status = 500,
                    Success = false,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }
        public async Task<ServiceResult<sa_regions>> Addregion(sa_regions countries)
        {
            if (countries == null)
            {
                _logger.LogError("Countries data is null in AddCountriesAsync method");
                return new ServiceResult<sa_regions>
                {
                    Status = 400,
                    Success = false,
                    Message = "Countries data is null",
                    Data = null
                };
            }
            try
            {
                var country = await _context.sa_country_regions.Include(a => a.sa_regions).FirstOrDefaultAsync(c => c.country_region_id == countries.country_region_id);
                countries.parent_id = countries.region_id;

                countries.region_id = Guid.CreateVersion7();
                countries.country_region_id = countries.country_region_id;
                countries.city = countries.city;
                await _context.sa_regions.AddAsync(countries);
                country.sa_regions.Add(countries);

                _context.sa_country_regions.Update(country);
                await _context.SaveChangesAsync();
                return new ServiceResult<sa_regions>
                {
                    Status = 201,
                    Success = true,
                    Message = "Countries added successfully",
                    Data = countries
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in AddCountriesAsync method");
                return new ServiceResult<sa_regions>
                {
                    Status = 500,
                    Success = false,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }
        public async Task<ServiceResult<List<sa_country_regions>>> GetRegionsList()
        {
            try
            {
                var regionsList = await _context.sa_country_regions.Include(a => a.sa_regions).ToListAsync();
                return new ServiceResult<List<sa_country_regions>>
                {
                    Status = 200,
                    Success = true,
                    Message = "Regions list retrieved successfully",
                    Data = regionsList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in GetRegionsList method");
                return new ServiceResult<List<sa_country_regions>>
                {
                    Status = 500,
                    Success = false,
                    Message = "Internal server error",
                    Data = null
                };
            }
        }
    }
}
