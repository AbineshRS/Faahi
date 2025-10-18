using Azure.Core;
using Dekiru.QueryFilter.Extensions;
using Dekiru.QueryFilter.Macros;
using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model;
using Faahi.Model.co_business;
using Faahi.Model.Email_verify;
using Faahi.Model.im_products;
using Faahi.Service.Auth;
using Faahi.Service.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Faahi.Service.CoBusiness
{
    public class CoBusinessService : ICoBusinessservice
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CoBusinessService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        public CoBusinessService(ApplicationDbContext context, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ILogger<CoBusinessService> logger, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResult<co_business>> Create_account(co_business business)
        {
            if (business == null)
            {
                _logger.LogWarning("No data found to insert co_business", business);

                return new ServiceResult<co_business> { Success = false, Message = "NO data found" };
            }
            try
            {
                var email_verify = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == business.email && a.verificationType == "EmailVerification" && a.userType == "co-admin");
                if (email_verify.verified == "F")
                {

                    return new ServiceResult<co_business> { Success = false, Message = "You need to verify your account", Status = -1 };
                }
                var exists = await _context.co_business.FirstOrDefaultAsync(a => a.email == business.email);
                if (exists != null)
                {
                    return new ServiceResult<co_business> { Success = false, Message = "Username already exists.", Status = -2 };
                }



                var namePart = Regex.Replace(business.business_name ?? "", @"\s+", "")
                        .Substring(0, Math.Min(3, (business.business_name ?? "").Length))
                        .ToUpper();
                var companyId = namePart + "-";
                business.company_id = Guid.CreateVersion7();
                var hashedPassword = PasswordHelper.HashPassword(business?.password);
                business.password = hashedPassword;
                business.created_at = DateTime.Now;
                business.edit_date_time = DateTime.Now;
                business.email = business.email;

                foreach (var address in business.co_addresses)
                {

                    address.company_address_id = Guid.CreateVersion7();
                    address.company_id = business.company_id;
                    address.edit_date_time = DateTime.Now;

                }



                await _context.co_business.AddAsync(business);
                string subject = "Congratulations! Your Seller Account on Faahi is Ready";
                string body = @"
                            <!DOCTYPE html>
                            <html lang='en'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                <title>Welcome to Faahi Seller Platform</title>
                                <style>
                                    body {
                                        font-family: 'Arial', sans-serif;
                                        background-color: #f7f7f7;
                                        margin: 0;
                                        padding: 0;
                                        color: #333;
                                    }
                                    .email-container {
                                        width: 100%;
                                        max-width: 600px;
                                        margin: 0 auto;
                                        background-color: #fff;
                                        border-radius: 8px;
                                        overflow: hidden;
                                        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                                    }
                                    .email-header {
                                        background-color: #28a745;
                                        color: #fff;
                                        padding: 20px;
                                        text-align: center;
                                    }
                                    .email-header h1 {
                                        margin: 0;
                                        font-size: 24px;
                                    }
                                    .email-body {
                                        padding: 30px;
                                        text-align: left;
                                        font-size: 16px;
                                        line-height: 1.6;
                                    }
                                    .email-body p {
                                        margin-bottom: 15px;
                                    }
                                    .email-body strong {
                                        color: #28a745;
                                    }
                                    .email-footer {
                                        background-color: #f1f1f1;
                                        padding: 20px;
                                        text-align: center;
                                        font-size: 12px;
                                        color: #777;
                                    }
                                    .email-footer a {
                                        color: #28a745;
                                        text-decoration: none;
                                    }
                                    .button {
                                        background-color: #28a745;
                                        color: #fff;
                                        padding: 12px 24px;
                                        text-align: center;
                                        display: inline-block;
                                        border-radius: 5px;
                                        text-decoration: none;
                                        font-weight: bold;
                                        margin-top: 20px;
                                        width: 100%;
                                        max-width: 220px;
                                        margin-left: auto;
                                        margin-right: auto;
                                    }
                                </style>
                            </head>
                            <body>
                                <div class='email-container'>
                                    <div class='email-header'>
                                        <h1>Welcome to Faahi Seller Platform</h1>
                                    </div>
                                    <div class='email-body'>
                                        <p>Dear " + business.name + @",</p>
                                        <p>Congratulations! You have successfully registered as a seller on <strong>Faahi</strong>.</p>
                                        <p>We are excited to have you join our marketplace. As a seller, you'll now have the ability to manage your products, track your sales, and connect with customers across the globe.</p>
                                        <p>Here are a few next steps to get started:</p>
                                        <ul>
                                            <li><strong>Upload your products</strong> – Start adding your products to our platform.</li>
                                            <li><strong>Set up your store</strong> – Personalize your storefront and manage your brand.</li>
                                            <li><strong>Start selling</strong> – Once your products are live, customers can start buying from you!</li>
                                        </ul>
                                        <p>If you have any questions or need assistance, our support team is here to help.</p>
                                        <a href='mailto:support@faahi.com' class='button'>Contact Support</a>
                                    </div>
                                    <div class='email-footer'>
                                        <p>Best regards,</p>
                                        <p>The Faahi Team</p>
                                        <p><small>If you need assistance, feel free to reach out to us at <a href='mailto:support@faahi.com'>support@faahi.com</a>.</small></p>
                                    </div>
                                </div>
                            </body>
                            </html>
                            ";
                var emailService = new EmailService(_configuration);
                await emailService.SendEmailAsync(business.email, subject, body);

                await _context.SaveChangesAsync();

                return new ServiceResult<co_business>
                {
                    Success = true,
                    Message = "User created successfully",
                    Data = business
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error occurred adding co_business");
                return new ServiceResult<co_business>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }

        }
        public async Task<ActionResult<ServiceResult<string>>> Upload_logo(IFormFile formFile, string company_id)
        {
            if (formFile == null || formFile.Length == 0)
            {
                _logger.LogWarning("No data found in file",formFile);
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No file uploaded.",
                    Data = null
                };
            }
            try
            {

                FileInfo fileInfo = new FileInfo(formFile.FileName);
                var newItemFile = "sub_" + company_id + "_1" + fileInfo.Extension;

                string relativeFolder = Path.Combine("Images", "Company", company_id, "Companylogo");
                string fullFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, relativeFolder);

                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                }

                string fullPath = Path.Combine(fullFolderPath, newItemFile);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                string relativePath = Path.Combine(relativeFolder, newItemFile).Replace("\\", "/");

                var co_buss = await _context.co_business.FindAsync(company_id);
                if (co_buss == null)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Company not found.",
                        Data = null
                    };
                }

                co_buss.logo_fileName = relativePath;
                _context.co_business.Update(co_buss);
                await _context.SaveChangesAsync();

                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "File uploaded",
                    Data = relativePath
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logo");
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            
        }

        public async Task<AuthResponse> LoginAsyn(string username, string password)
        {
            if(username == null || password == null)
            {
                _logger.LogWarning("Username or password is null", username);
                return null;
            }
            try
            {
                _logger.LogWarning("Username or password is null", username);

                var user = _context.co_business.FirstOrDefault(a => a.name == username || a.email == username);
                if (user is null)
                {
                    var site_user = await _context.im_site_users.FirstOrDefaultAsync(a => a.site_user_code == username);
                    if (site_user is null)
                    {
                        return null;
                    }
                    if (!BCrypt.Net.BCrypt.Verify(password, site_user.password))
                    {
                        return null;
                    }
                    var accessToken_site_users = CreatTokensite_user(site_user, 10);   // 10 minutes
                    var refreshToken_site_users = CreatTokensite_user(site_user, 10080); // 7 days (in minutes)
                    return new AuthResponse
                    {
                        AccessToken = accessToken_site_users,
                        RefreshToken = refreshToken_site_users
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.password))
                {
                    return null;
                }
                var accessToken = CreatToken(user, 10);   // 10 minutes
                var refreshToken = CreatToken(user, 10080); // 7 days (in minutes)

                return new AuthResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while login");
                return null;
            }
            
        }
        private string CreatToken(co_business user, int minutes)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.company_id.ToString() ?? ""), // important for RefreshToken
                 new Claim(ClaimTypes.Name, user.company_id.ToString() ?? ""),
                 new Claim("userRole", "co-admin" ?? "")
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
        private string CreatTokensite_user(im_site_users user, int minutes)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.company_id.ToString() ?? ""), // important for RefreshToken
                 new Claim(ClaimTypes.Name, user.company_id.ToString() ?? ""),
                 new Claim("userId", user.userId.ToString() ?? ""),
                 new Claim("site_id", user.site_id.ToString() ?? ""),
                 new Claim("company_id", user.company_id.ToString() ?? ""),
                 new Claim("userRole", user.userRole.ToString() ?? "")
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
        public async Task<ServiceResult<string>> send_reset_password(string email)
        {
            if (email == null)
            {
                _logger.LogWarning("No email found", email);
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No email found",
                    Status = -1
                };
            }
            try
            {
                var existing = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "ResetPassword" && a.userType == "co-admin");



                string token = CreateToken_email(email, 5);

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                DateTime tokenExpiryTime = jwtToken?.ValidTo ?? DateTime.UtcNow;
                string emailPart = email.Substring(0, 3).ToUpper();


                if (existing != null)
                {
                    existing.token = token;
                    existing.tokenExpiryTime = tokenExpiryTime;
                    existing.isExpired = "F";
                    existing.verified = "F";
                    _context.am_emailVerifications.Update(existing);
                }
                else
                {
                    am_emailVerifications am_Email = new am_emailVerifications
                    {

                        Email_id = Guid.CreateVersion7(),
                        email = email,
                        verificationType = "ResetPassword",
                        token = token,
                        tokenExpiryTime = tokenExpiryTime,
                        isExpired = "F",
                        verified = "F",
                        userType = "co-admin"
                    };
                    _context.am_emailVerifications.Add(am_Email);

                }


                //// Load from appsettings.json (optional)
                //var baseUrl = _configuration["AppSettings:BaseUrl"];            // e.g., "http://localhost:5173"
                //var verifyPath = "verify-success"; // Hardcode or config

                //// Your values
                //string token = /* your token */;
                //string email = /* your email */;

                //// URL encode parameters to be safe
                //string encodedToken = Uri.EscapeDataString(token);
                //string encodedEmail = Uri.EscapeDataString(email);

                //// Construct the full URL
                //var verificationLink = $"{baseUrl}/{verifyPath}/token/{encodedToken}/email/{encodedEmail}";

                var baseUrl = _contextAccessor.HttpContext?.Items["BaseUrl"]?.ToString();

                // Fallback if baseUrl is not set (optional)
                if (string.IsNullOrEmpty(baseUrl))
                {
                    baseUrl = _configuration["MailSettings:BaseUrl"];
                }
                var resetUrl = $"{baseUrl}/password-verify-success?token={token}&email={email}";

                string subject = "Reset Your Password";
                string body = $@"
                            <p>Hello,</p>
                            <p>We received a request to reset your password.</p>
                            <p>Please click the link below to reset your password:</p>
                            <p><a href=""{resetUrl}"">Reset Password</a></p>
                            <p>If you did not request a password reset, please ignore this email.</p>
                            <p>Thank you!</p>";

                var emailService = new EmailService(_configuration);
                await emailService.SendEmailAsync(email, subject, body);


                await _context.SaveChangesAsync();
                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Email send successfully",
                    Status = 1,
                    Data = null
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while send reset password");
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }

            

        }
        public async Task<ServiceResult<am_emailVerifications>> verify(string email, string token)
        {
            if (email is null)
            {
                _logger.LogWarning("No email found", email);
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                };
            }
            try
            {
                var am_email = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "EmailVerification" && a.userType == "co-admin");

                if (am_email.token != token)
                {
                    return new ServiceResult<am_emailVerifications>
                    {
                        Success = false,
                        Message = "Invalid token",
                        Status = -1
                    };
                }
                if (am_email.verified == "T")
                {
                    return new ServiceResult<am_emailVerifications>
                    {
                        Success = false,
                        Message = "You have already verifyed",
                        Status = -4
                    };
                }
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken;
                jwtToken = handler.ReadToken(token) as JwtSecurityToken;


                DateTime tokenExpiryFromJwt = jwtToken.ValidTo;
                DateTime? tokenExpiryFromDb = am_email.tokenExpiryTime;
                if (tokenExpiryFromJwt < DateTime.UtcNow || tokenExpiryFromJwt != tokenExpiryFromDb)
                {
                    am_email.isExpired = "T";
                    _context.am_emailVerifications.Update(am_email);
                    await _context.SaveChangesAsync();

                    return new ServiceResult<am_emailVerifications>
                    {
                        Success = false,
                        Message = "Invalid or expired token",
                        Status = -3
                    };
                }
                am_email.verified = "T";
                am_email.isExpired = "F";

                _context.am_emailVerifications.Update(am_email);
                await _context.SaveChangesAsync();

                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "Email verified successfully",
                    Status = 1,
                    Data = am_email
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while email verify");
                return new ServiceResult<am_emailVerifications>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            
        }
        public async Task<ServiceResult<am_emailVerifications>> Password_Verify(string email, string token)
        {
            if (email is null)
            {
                _logger.LogWarning("No email found", email);
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                };
            }
            try
            {
                var am_email = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "ResetPassword" && a.userType == "co-admin");

                if (am_email.token != token)
                {
                    return new ServiceResult<am_emailVerifications>
                    {
                        Success = false,
                        Message = "Invalid token",
                        Status = -1
                    };
                }
                if (am_email.verified == "T")
                {
                    return new ServiceResult<am_emailVerifications>
                    {
                        Success = false,
                        Message = "You have already verifyed",
                        Status = -4
                    };
                }
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken;
                jwtToken = handler.ReadToken(token) as JwtSecurityToken;


                DateTime tokenExpiryFromJwt = jwtToken.ValidTo;
                DateTime? tokenExpiryFromDb = am_email.tokenExpiryTime;
                if (tokenExpiryFromJwt < DateTime.UtcNow || tokenExpiryFromJwt != tokenExpiryFromDb)
                {
                    am_email.isExpired = "T";
                    _context.am_emailVerifications.Update(am_email);
                    await _context.SaveChangesAsync();

                    return new ServiceResult<am_emailVerifications>
                    {
                        Success = false,
                        Message = "Invalid or expired token",
                        Status = -3
                    };
                }
                am_email.verified = "T";
                am_email.isExpired = "F";

                _context.am_emailVerifications.Update(am_email);
                await _context.SaveChangesAsync();

                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "Email verified successfully",
                    Status = 1,
                    Data = am_email
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while password verify");
                return new ServiceResult<am_emailVerifications>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
           
        }
        public async Task<ServiceResult<string>> reset_password(string token, string email, string password)
        {
            if (email == null)
            {
                _logger.LogWarning("No email found", email);
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No email found",
                    Status = -1

                };
            }
            try
            {
                var am_email = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "ResetPassword" && a.userType == "co-admin");

                if (am_email.token != token)
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Invalid token",
                        Status = -2
                    };
                }
                if (am_email.verified == "F")
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Token Not verified",
                        Status = -4
                    };
                }
                if (am_email.isExpired == "T")
                {
                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Token Expired",
                        Status = -3
                    };
                }

                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken;
                jwtToken = handler.ReadToken(token) as JwtSecurityToken;


                DateTime tokenExpiryFromJwt = jwtToken.ValidTo;
                DateTime? tokenExpiryFromDb = am_email.tokenExpiryTime;
                if (tokenExpiryFromJwt < DateTime.UtcNow || tokenExpiryFromJwt != tokenExpiryFromDb)
                {
                    am_email.isExpired = "T";
                    _context.am_emailVerifications.Update(am_email);
                    await _context.SaveChangesAsync();

                    return new ServiceResult<string>
                    {
                        Success = false,
                        Message = "Token Expired ",
                        Status = -4
                    };
                }
                var co_business = await _context.co_business.FirstOrDefaultAsync(a => a.email == email);
                if (co_business != null)
                {
                    co_business.password = PasswordHelper.HashPassword(password);
                    am_email.verified = "T";
                    am_email.isExpired = "F";
                    _context.am_emailVerifications.Update(am_email);
                }
                _context.co_business.Update(co_business);
                await _context.SaveChangesAsync();
                return new ServiceResult<string>
                {
                    Success = true,
                    Message = "Password Updated",
                    Status = 1

                };

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while reset password");
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            
        }
        public string CreateToken_email(string email, int minutes)
        {
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, email ?? ""),
              new Claim(ClaimTypes.Name, email ?? "")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:key")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescription = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
        }

        public async Task<ServiceResult<co_business>> Update_profile(co_business user, string company_id)
        {
            if (user == null)
            {
                _logger.LogWarning("No user data provided for update", user);
                return new ServiceResult<co_business>
                {
                    Success = true,
                    Message = "No user data provided"
                };
            }
            try
            {
                var company_id_guid = Guid.Parse(company_id);
                var existing = await _context.co_business
                       .Include(c => c.co_addresses)
                       .FirstOrDefaultAsync(a => a.company_id == company_id_guid);
                if (existing == null)
                {
                    return new ServiceResult<co_business>
                    {
                        Success = false,
                        Message = "User not found",
                        Data = null
                    };
                }
                existing.edit_date_time = DateTime.Now;
                existing.address = user.address;
                existing.phoneNumber = user.phoneNumber;
                existing.plan_type = user.plan_type;
                existing.sites_allowed = user.sites_allowed;
                existing.createdSites = user.createdSites;
                existing.email = user.email;

                foreach (var co_address in user.co_addresses)
                {
                    var address = await _context.co_address.FirstOrDefaultAsync(a => a.company_address_id == co_address.company_address_id);

                    address.contact_person = co_address.contact_person;
                    address.street_1 = co_address.street_1;
                    address.street_2 = co_address.street_2;
                    address.telephone_1 = co_address.telephone_1;
                    address.telephone_2 = co_address.telephone_2;
                    existing.co_addresses.Add(address);
                }
                _context.co_business.Update(existing);
                await _context.SaveChangesAsync();
                return new ServiceResult<co_business>
                {
                    Success = true,
                    Message = "User profile updated successfully",
                    Data = existing
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating co_business");
                return new ServiceResult<co_business>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            
        }
        public async Task<ServiceResult<co_avl_countries>> CreateAvailableCountry(co_avl_countries co_Avl_Countries)
        {
            if (co_Avl_Countries == null || co_Avl_Countries == null || string.IsNullOrWhiteSpace(co_Avl_Countries.name))
            {
                _logger.LogWarning("Invalid or missing country data", co_Avl_Countries);
                return new ServiceResult<co_avl_countries>
                {
                    Success = false,
                    Message = "Invalid or missing country data",
                    Status = -1
                };
            }
            try
            {
                // Check if country already exists
                var existingData = await _context.co_avl_countries
                    .FirstOrDefaultAsync(a => a.name.ToLower() == co_Avl_Countries.name.ToLower());

                if (existingData != null)
                {
                    return new ServiceResult<co_avl_countries>
                    {
                        Success = false,
                        Message = "already Exisit",
                        Status = -2
                    };
                }

                // Fetch country info
                var countryDetails = await GetCountryInfoByNameAsync(co_Avl_Countries.name);
                if (countryDetails == null)
                {
                    return new ServiceResult<co_avl_countries>
                    {
                        Success = false,
                        Message = "Country information not found",
                        Status = -3
                    };
                }




                // Assign values
                co_Avl_Countries.avl_countries_id = Guid.CreateVersion7();
                co_Avl_Countries.country_code = countryDetails.Code;
                co_Avl_Countries.flag = countryDetails.FlagUrl;
                co_Avl_Countries.dialling_code = countryDetails.DialCode;
                co_Avl_Countries.currency_code = countryDetails.CurrencyCode;
                co_Avl_Countries.currency_name = countryDetails.CurrencyName;
                co_Avl_Countries.serv_available = "T";

                await _context.co_avl_countries.AddAsync(co_Avl_Countries);

                // Increment next_key



                await _context.SaveChangesAsync();

                return new ServiceResult<co_avl_countries>
                {
                    Success = true,
                    Message = "Success",
                    Status = 1,
                    Data = co_Avl_Countries
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating available country");
                return new ServiceResult<co_avl_countries>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }

            
        }
        public async Task<CountryInfo_Dto?> GetCountryInfoByNameAsync(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                return null;

            using var httpClient = new HttpClient();

            // Try both endpoints: first /capital/, then /name/
            string[] endpoints =
            {
        $"https://restcountries.com/v3.1/capital/{Uri.EscapeDataString(countryName)}?fields=name,cca2,flags,idd,currencies",
        $"https://restcountries.com/v3.1/name/{Uri.EscapeDataString(countryName)}?fields=name,cca2,flags,idd,currencies"
    };

            foreach (var url in endpoints)
            {
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    continue;

                var json = await response.Content.ReadAsStringAsync();
                var root = JsonDocument.Parse(json).RootElement;

                if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
                    continue;

                var country = root.EnumerateArray()
                    .FirstOrDefault(c =>
                        string.Equals(
                            c.GetProperty("name").GetProperty("common").GetString(),
                            countryName,
                            StringComparison.OrdinalIgnoreCase));

                // If exact match not found, just use first result
                if (country.ValueKind != JsonValueKind.Object)
                    country = root[0];

                string name = country.GetProperty("name").GetProperty("common").GetString() ?? "";
                string code = country.GetProperty("cca2").GetString() ?? "";
                string flagUrl = country.GetProperty("flags").GetProperty("png").GetString() ?? "";

                string dialCode = country.TryGetProperty("idd", out var idd) &&
                                  idd.TryGetProperty("root", out var rootCode) &&
                                  idd.TryGetProperty("suffixes", out var suffixes) &&
                                  suffixes.GetArrayLength() > 0
                                  ? rootCode.GetString() + suffixes[0].GetString()
                                  : "";

                string currencyCode = "", currencyName = "";
                if (country.TryGetProperty("currencies", out var currencies))
                {
                    var currency = currencies.EnumerateObject().FirstOrDefault();
                    currencyCode = currency.Name;
                    currencyName = currency.Value.GetProperty("name").GetString() ?? "";
                }

                return new CountryInfo_Dto
                {
                    Name = name,
                    Code = code,
                    FlagUrl = flagUrl,
                    DialCode = dialCode,
                    CurrencyCode = currencyCode,
                    CurrencyName = currencyName
                };
            }

            return null;
        }
        public async Task<ServiceResult<List<co_avl_countries>>> CurrencyList()
        {
            if (_context.co_avl_countries == null)
            {
                return new ServiceResult<List<co_avl_countries>>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            var currency_list = await _context.co_avl_countries.ToListAsync();
            return new ServiceResult<List<co_avl_countries>>
            {
                Success = true,
                Status = 1,
                Data = currency_list
            };
        }
        public async Task<ServiceResult<im_site>> Create_im_site(im_site im_site)
        {
            if (im_site == null)
            {
                _logger.LogWarning("No im_site data provided for creation", im_site);
                return new ServiceResult<im_site>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            // Get next key
            //var tableName = "im_site";
            //var amTable = await _context.am_table_next_key.FindAsync(tableName);
            try
            {
                var company_details = await _context.co_address.FirstOrDefaultAsync(a => a.company_id == im_site.company_id);
                var company_user = await _context.co_business.FirstOrDefaultAsync(a => a.company_id == im_site.company_id);
                var currency = await _context.co_avl_countries.FirstOrDefaultAsync(a => a.avl_countries_id == im_site.avl_countries_id);
                if (company_details == null || company_user == null || currency == null)
                {
                    return new ServiceResult<im_site>
                    {
                        Success = false,
                        Status = -2,
                        Message = "Invalid company or currency details"
                    };
                }
                if (company_user != null)
                {
                    if (company_user.createdSites >= company_user.sites_allowed)
                    {
                        return new ServiceResult<im_site>
                        {
                            Success = false,
                            Status = -3,
                            Message = "Site creation limit reached for your plan."
                        };
                    }
                    company_user.createdSites += 1;
                    _context.co_business.Update(company_user);
                }
                // Assign values
                im_site.site_id = Guid.CreateVersion7();
                im_site.company_id = im_site.company_id;
                im_site.company_address_id = company_details.company_address_id;
                im_site.avl_countries_id = currency.avl_countries_id;
                im_site.site_name = im_site.site_name;
                im_site.tin_number = company_user.tin_number;
                im_site.edit_user_id = company_user.name;
                im_site.created_at = DateTime.Now;
                im_site.status = "T";
                foreach (var im_item in im_site.im_item_site)
                {


                    im_item.item_id = Guid.CreateVersion7();
                    im_item.site_id = im_site.site_id;
                    im_item.bin_number = im_item.bin_number;
                    im_item.primary_vendor_id = im_item.primary_vendor_id;
                    im_item.on_hand_quantity = im_item.on_hand_quantity;
                    im_item.committed_quantity = im_item.committed_quantity;
                    im_item.purchase_order_quantity = im_item.purchase_order_quantity;
                    im_item.sales_order_quantity = im_item.sales_order_quantity;
                    im_item.c_price = im_item.c_price;
                    im_item.edit_user_id = im_site.edit_user_id;
                    im_item.created_at = DateTime.Now;
                    im_item.on_hold = im_item.on_hold;
                    im_item.status = im_item.status;


                }

                _context.im_site.Add(im_site);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_site>
                {
                    Success = true,
                    Status = 1,
                    Data = im_site
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "error occurred adding im_site");
                return new ServiceResult<im_site>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            


        }
        public async Task<ServiceResult<List<im_site>>> imsite_list()
        {
            if (_context.im_item_site == null)
            {
                return new ServiceResult<List<im_site>>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            var imsite = await _context.im_site.Include(a => a.im_item_site).ToListAsync();
            if (imsite == null)
            {
                return new ServiceResult<List<im_site>>
                {
                    Success = false,
                    Status = -2,
                    Message = "NOt found"
                };
            }
            return new ServiceResult<List<im_site>>
            {
                Success = true,
                Status = 1,
                Data = imsite
            };
        }
        public async Task<ServiceResult<im_site>> Get_im_site(string site_id)
        {
            if (site_id == null)
            {
                return new ServiceResult<im_site>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            var site_guid = Guid.Parse(site_id);
            var imsite = await _context.im_site.Include(a => a.im_item_site).FirstOrDefaultAsync(a => a.site_id == site_guid);
            if (imsite == null)
            {
                return new ServiceResult<im_site>
                {
                    Success = false,
                    Status = -2,
                    Message = "NOt found"
                };
            }
            return new ServiceResult<im_site>
            {
                Success = true,
                Status = 1,
                Data = imsite
            };
        }
        public async Task<ServiceResult<List<im_site>>> Get_im_site_company(string company_id)
        {
            if (company_id == null)
            {
                return new ServiceResult<List<im_site>>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            var guid_company_id = Guid.Parse(company_id);
            var imsite = await _context.im_site.Include(a => a.im_item_site).Where(a => a.company_id == guid_company_id).ToListAsync();
            if (imsite == null)
            {
                return new ServiceResult<List<im_site>>
                {
                    Success = false,
                    Status = -2,
                    Message = "NOt found"
                };
            }
            return new ServiceResult<List<im_site>>
            {
                Success = true,
                Status = 1,
                Data = imsite
            };
        }
        public async Task<ServiceResult<im_site>> Update_imsite(string site_id, im_site imsite)
        {
            if (site_id == null || imsite == null)
            {
                _logger.LogWarning("No site_id or imsite data provided for update", site_id, imsite);
                return new ServiceResult<im_site>
                {
                    Success = false,
                    Status = -1,
                    Message = "No data found"
                };
            }
            try
            {
                var site_guid = Guid.Parse(site_id);

                var im_site_data = await _context.im_site.Include(a => a.im_item_site).FirstOrDefaultAsync(a => a.site_id == site_guid);
                im_site_data.company_address_id = imsite.company_address_id;
                im_site_data.avl_countries_id = imsite.avl_countries_id;
                im_site_data.site_name = imsite.site_name;
                im_site_data.edit_user_id = imsite.edit_user_id;
                im_site_data.created_at = DateTime.Now;
                foreach (var site_item in imsite.im_item_site)
                {
                    var im_sub_site = im_site_data.im_item_site.FirstOrDefault(a => a.item_id == site_item.item_id);
                    if (im_sub_site == null)
                    {

                        site_item.item_id = Guid.CreateVersion7();
                        site_item.site_id = site_guid;
                        site_item.bin_number = site_item.bin_number;
                        site_item.primary_vendor_id = site_item.primary_vendor_id;
                        site_item.on_hand_quantity = site_item.on_hand_quantity;
                        site_item.committed_quantity = site_item.committed_quantity;
                        site_item.sales_order_quantity = site_item.sales_order_quantity;
                        site_item.c_price = site_item.c_price;
                        site_item.edit_user_id = site_item.edit_user_id;
                        site_item.created_at = DateTime.Now;
                        site_item.on_hold = site_item.on_hold;
                        site_item.status = site_item.status;
                        im_site_data.im_item_site.Add(site_item);



                    }
                    else
                    {
                        im_sub_site.primary_vendor_id = site_item.primary_vendor_id;
                        im_sub_site.on_hand_quantity = site_item.on_hand_quantity;
                        im_sub_site.committed_quantity = site_item.committed_quantity;
                        im_sub_site.purchase_order_quantity = site_item.purchase_order_quantity;
                        im_sub_site.sales_order_quantity = site_item.sales_order_quantity;
                        im_sub_site.c_price = site_item.c_price;
                        im_sub_site.edit_user_id = site_item.edit_user_id;
                        im_sub_site.created_at = DateTime.Now;
                        im_sub_site.on_hold = site_item.on_hold;
                        im_sub_site.status = site_item.status;

                    }


                }
                await _context.SaveChangesAsync();
                return new ServiceResult<im_site>
                {
                    Success = true,
                    Status = 1,
                    Message = "Success",
                    Data = imsite
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating im_site");
                return new ServiceResult<im_site>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            
        }
        public async Task<ServiceResult<im_site_users>> Add_site_users(im_site_users im_Site_Users)
        {
            if (im_Site_Users == null)
            {
                _logger.LogWarning("No im_site_users data provided for creation", im_Site_Users);
                return new ServiceResult<im_site_users>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            try
            {
                var site_users = await _context.im_site_users.FirstOrDefaultAsync(a => a.email == im_Site_Users.email);
                if (site_users != null)
                {
                    return new ServiceResult<im_site_users>
                    {
                        Success = false,
                        Status = -2,
                        Message = "Email already Exisit"
                    };
                }

                im_Site_Users.userId = Guid.CreateVersion7();
                im_Site_Users.site_id = im_Site_Users.site_id;
                im_Site_Users.firstName = im_Site_Users.firstName;
                im_Site_Users.lastName = im_Site_Users.lastName;
                im_Site_Users.fullName = im_Site_Users.firstName + " " + im_Site_Users.lastName;
                var random = new Random();
                string userCode;
                bool exists;

                do
                {
                    userCode = random.Next(100000, 999999).ToString();
                    exists = await _context.im_site_users.AnyAsync(v => v.site_user_code == userCode);
                }
                while (exists);
                var imsite = await _context.im_site.FirstOrDefaultAsync(a => a.site_id == im_Site_Users.site_id);
                var company_user = await _context.co_business.FirstOrDefaultAsync(a => a.company_id == imsite.company_id);
                im_Site_Users.company_id = company_user.company_id;
                string sitePrefix = imsite.site_name.Length >= 2 ? imsite.site_name.Substring(0, 2) : imsite.site_name;
                string firstNamePrefix = im_Site_Users.firstName.Length >= 2 ? im_Site_Users.firstName.Substring(0, 2) : im_Site_Users.firstName;

                im_Site_Users.site_user_code = sitePrefix + firstNamePrefix + "-" + Convert.ToString(userCode).ToUpper();
                string without_hased_password = im_Site_Users.password;
                var hasedpassword = PasswordHelper.HashPassword(im_Site_Users.password);
                im_Site_Users.password = hasedpassword;
                im_Site_Users.email = im_Site_Users.email;
                im_Site_Users.userRole = im_Site_Users.userRole;
                im_Site_Users.created_at = DateTime.Now;
                im_Site_Users.edit_user_id = company_user.name;
                im_Site_Users.phoneNumber = im_Site_Users.phoneNumber;
                im_Site_Users.address = im_Site_Users.address;
                im_Site_Users.status = "T";
                _context.im_site_users.Add(im_Site_Users);
                await _context.SaveChangesAsync();

                var email_send = Send_Emails.EmailBody_site_users(im_Site_Users.fullName, im_Site_Users.site_user_code, without_hased_password);
                string subject = "Welcome to Faahi – Your Site User Account is Ready!";
                var emailService = new EmailService(_configuration);
                await emailService.SendEmailAsync(im_Site_Users.email, subject, email_send);

                return new ServiceResult<im_site_users>
                {
                    Success = true,
                    Status = 1,
                    Message = "Success",
                    Data = im_Site_Users
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding site user");
                return new ServiceResult<im_site_users>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            
        }
        public async Task<ServiceResult<im_site_users>> site_user(Guid user_id)
        {
            if (_context.im_site_users == null)
            {
                return new ServiceResult<im_site_users>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            var site_user = await _context.im_site_users.FirstOrDefaultAsync(a => a.userId == user_id);
            if (site_user == null)
            {
                return new ServiceResult<im_site_users>
                {
                    Success = false,
                    Status = -2,
                    Message = "NOt found"
                };
            }
            return new ServiceResult<im_site_users>
            {
                Success = true,
                Status = 1,
                Data = site_user
            };
        }
        public async Task<ServiceResult<List<im_site_users>>> site_user_list(Guid company_id)
        {
            if (_context.im_site_users == null)
            {
                return new ServiceResult<List<im_site_users>>
                {
                    Success = false,
                    Status = -1,
                    Message = "no data found"
                };
            }
            var site_user = await _context.im_site_users.Where(a => a.company_id == company_id).ToListAsync();
            if (site_user == null)
            {
                return new ServiceResult<List<im_site_users>>
                {
                    Success = false,
                    Status = -2,
                    Message = "NOt found"
                };
            }
            return new ServiceResult<List<im_site_users>>
            {
                Success = true,
                Status = 1,
                Data = site_user
            };
        }
        public async Task<ServiceResult<im_site_users>> Update_site_users(Guid userId, im_site_users im_Site_Users)
        {
            if (userId == null || im_Site_Users == null)
            {
                _logger.LogWarning("No userId or im_site_users data provided for update", userId, im_Site_Users);
                return new ServiceResult<im_site_users>
                {
                    Success = false,
                    Status = -1,
                    Message = "No data found"
                };
            }
            try
            {
                var site_user = await _context.im_site_users.FirstOrDefaultAsync(a => a.userId == userId);

                site_user.site_id = im_Site_Users.site_id;
                var imsite = await _context.im_site.FirstOrDefaultAsync(a => a.site_id == im_Site_Users.site_id);
                var company_user = await _context.co_business.FirstOrDefaultAsync(a => a.company_id == imsite.company_id);
                site_user.company_id = company_user.company_id;

                site_user.firstName = im_Site_Users.firstName;
                site_user.lastName = im_Site_Users.lastName;
                site_user.fullName = im_Site_Users.firstName + " " + im_Site_Users.lastName;
                //site_user.email = im_Site_Users.email;
                site_user.phoneNumber = im_Site_Users.phoneNumber;
                site_user.address = im_Site_Users.address;
                site_user.status = im_Site_Users.status;

                _context.im_site_users.Update(site_user);
                await _context.SaveChangesAsync();
                return new ServiceResult<im_site_users>
                {
                    Success = true,
                    Status = 1,
                    Message = "Success",
                    Data = site_user
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating site user");
                return new ServiceResult<im_site_users>
                {
                    Success = false,
                    Message = "An unexpected error occurred",
                    Status = -500
                };
            }
            
        }
        public async Task<ServiceResult<List<co_business>>> Dekiru(string searchTerm)
        {
            FilterMacros.Add<string, string, bool>(
        "like",
        (source, pattern) => EF.Functions.Like(source, pattern)
    );
            string filter = $"@like(business_name, '%{searchTerm}%')";
            string sort = "company_id desc";

            var query = _context.co_business
                                .AsQueryable()
                                .FilterDynamic(filter)
                                .SortDynamic(sort);

            var resultList = await query.ToListAsync();  // 👈 Fetch list

            return new ServiceResult<List<co_business>>
            {
                Success = true,
                Message = "Filtered businesses fetched successfully",
                Data = resultList
            };
        }

    }
}
