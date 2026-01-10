using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model.st_sellers;
using Faahi.Model.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Faahi.Service.Admin
{
    public class admin_service: Iadmin
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public admin_service(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsyn(string username, string password)
        {
            if(username == "Admin" || password == "Admin@123")
            {
                return new AuthResponse
                {
                    status = 0,
                    AccessToken = null,
                    RefreshToken = null
                };
            }
            var accessToken_site_users = CreatTokensite_user(username, 15);   
            var refreshToken_site_users = CreatTokensite_user(username, 10080); 
            return new AuthResponse
            {
                status = 2,
                AccessToken = accessToken_site_users,
                RefreshToken = refreshToken_site_users
            };
        }
        private string CreatTokensite_user(string username, int minutes)
        {
            

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, "Admin" ?? ""), // important for RefreshToken
                 new Claim(ClaimTypes.Name, username ?? ""),
                 new Claim("userId", "" ?? ""),
                 new Claim("company_id","1" ?? ""),
                 new Claim("userRole", "Admin" ?? ""),
                 new Claim("FullName", "Admin" ?? "")
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
    }
}
