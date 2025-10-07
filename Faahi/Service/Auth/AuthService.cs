using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model;
using Faahi.Model.Email_verify;
using Faahi.Service.Email;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Faahi.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<AuthResponse> LoginAsyn(string username,string password)
        {
            var user = _context.am_users.FirstOrDefault(a => a.userName == username || a.email==username);
            if (user is null)
            {
                return null;
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
        private string CreatToken(am_users user, int minutes)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.userId.ToString() ?? ""), // important for RefreshToken
                 new Claim(ClaimTypes.Name, user.userId.ToString() ?? "")
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
        public AuthResponse RefreshToken(string request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request))
            {
                // Refresh token was not provided
                throw new ArgumentException("Refresh token is required", nameof(request));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["AppSettings:Key"]);

            try
            {
                // Validate refresh token
                var principal = tokenHandler.ValidateToken(request,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _configuration["AppSettings:Issuer"],
                        ValidAudience = _configuration["AppSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true, // check expiry
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                // Extract user claims
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    // Invalid token claims
                    return null;
                }
                var parsedId = Guid.Parse(userId);
                // Issue new tokens
                var newAccessToken = CreatToken(new am_users
                {
                    userId = parsedId,
                }, 10); // 15 minutes

                var newRefreshToken = CreatToken(new am_users
                {
                    userId = parsedId

                }, 10080); // 7 days

                return new AuthResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (SecurityTokenExpiredException)
            {
                // Token expired
                return null;
            }
            catch (Exception ex)
            {
                // Invalid token
                return null;
            }
        }

        public async Task<ServiceResult<am_users>> Create_account(am_users user)
        {
            if (user == null)
            {
                return null;
            }
            var existing = await _context.am_users.FirstOrDefaultAsync(a => a.userName == user.userName);
            if (existing != null)
            {
                return new ServiceResult<am_users> { Success = false, Message = "Username already exists." };
            }
            var email_verify = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == user.email  && a.verificationType== "EmailVerification" && a.userType=="User");
            if (email_verify.verified == "F")
            {
                return new ServiceResult<am_users> { Success = false, Message = "Email is Not verified" };

            }


            //var table = "am_users";
            //var am_table = await _context.am_table_next_key.FindAsync(table);
            //var key = Convert.ToInt16(am_table.next_key);

            am_users am_Users = new am_users();
            am_Users.userId = Guid.CreateVersion7();
            am_Users.userName = user.email;
            var hashedPassword = PasswordHelper.HashPassword(user?.password);
            am_Users.password = hashedPassword;
            am_Users.firstName = user.firstName;
            am_Users.lastName = user.lastName;
            am_Users.fullName = user.firstName + user.lastName;

            am_Users.email = user.email;
            string email = user.email;
            if (email_verify != null)
            {
                am_Users.emailVerified = "T";
            }

            //am_Users.siteId = user.siteId;
            //am_Users.company_id = user.company_id;
            //am_Users.userRole = user.userRole;
           
            am_Users.created_at = DateTime.Now;
            am_Users.edit_date_time = DateTime.Now;
            am_Users.edit_user_id = user.edit_user_id;
            am_Users.phoneNumber = user.phoneNumber;
            am_Users.address1 = user.address1;
            am_Users.address2 = user.address2;
            am_Users.status = user.status;
            _context.am_users.Add(am_Users);
            string subject = "Welcome to Faahi - Account Created Successfully!";
            string body = @"
                        <!DOCTYPE html>
                        <html lang='en'>
                        <head>
                            <meta charset='UTF-8'>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <title>Welcome to Faahi</title>
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
                                    background-color: #007bff;
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
                                }
                                .email-body p {
                                    line-height: 1.6;
                                }
                                .email-body strong {
                                    color: #007bff;
                                }
                                .email-footer {
                                    background-color: #f1f1f1;
                                    padding: 20px;
                                    text-align: center;
                                    font-size: 12px;
                                    color: #777;
                                }
                                .email-footer a {
                                    color: #007bff;
                                    text-decoration: none;
                                }
                                .button {
                                    background-color: #007bff;
                                    color: #fff;
                                    padding: 12px 24px;
                                    text-align: center;
                                    display: inline-block;
                                    border-radius: 5px;
                                    text-decoration: none;
                                    font-weight: bold;
                                    margin-top: 20px;
                                }
                            </style>
                        </head>
                        <body>
                            <div class='email-container'>
                                <div class='email-header'>
                                    <h1>Welcome to Faahi</h1>
                                </div>
                                <div class='email-body'>
                                    <p>Dear " + am_Users.fullName + @",</p>
                                    <p>Congratulations! Your account has been successfully created on <strong>Faahi</strong>.</p>
                                    <p>Thank you for signing up with us. We are excited to have you on board!</p>
                                    <p>If you have any questions, feel free to reach out to our support team.</p>
                                    <a href='mailto:support@faahi.com' class='button'>Contact Support</a>
                                </div>
                                <div class='email-footer'>
                                    <p>Best regards,</p>
                                    <p>The Faahi Team</p>
                                    <p><small>If you need assistance, you can always contact us at <a href='mailto:support@faahi.com'>support@faahi.com</a>.</small></p>
                                </div>
                            </div>
                        </body>
                        </html>
                        ";


            try
            {
                //am_table.next_key = key + 1;
                //_context.am_table_next_key.Update(am_table);
                await _context.SaveChangesAsync();
                var emailService = new EmailService(_configuration);
                await emailService.SendEmailAsync(email, subject, body);
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return new ServiceResult<am_users>
            {
                Success = true,
                Message = "User created successfully",
                Data = am_Users
            };

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
        /// <summary>
        /// co_business Email verification
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ServiceResult<am_emailVerifications>> email_verification(string email)
        {
            if (email is null)
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                };
            }
            var existing = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType== "EmailVerification" && a.userType== "co-admin");
            var co_existing = await _context.co_business.FirstOrDefaultAsync(a => a.email == email);
            if (existing != null || co_existing!=null)
            {
                return new ServiceResult<am_emailVerifications> { Success = false, Message = "Email already exists.",Status= -1 };
            }


            string token = CreateToken_email(email, 5);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            DateTime tokenExpiryTime = jwtToken?.ValidTo ?? DateTime.UtcNow;
            string emailPart = email.Substring(0, 3).ToUpper();
            
            am_emailVerifications am_Email = new am_emailVerifications
            {

                Email_id = Guid.CreateVersion7(),
                email = email,
                verificationType = "EmailVerification",
                token = token,
                tokenExpiryTime = tokenExpiryTime,
                isExpired = "F",
                verified = "F",
                userType="co-admin"
            };

            _context.am_emailVerifications.Add(am_Email);
           

            var verifyUrl = $"{_configuration["MailSettings:BaseUrl"]}/{_configuration["MailSettings:VerifyEmailPath"]}?token={token}&email={email}";


            string subject = "Verify Your Email Address";
            string body = $"<p>Hello,</p><p>Please click the link below to verify your email address:</p><p><a href=\"{verifyUrl}\">Verify Your Email</a></p><p>Thank you!</p>";

            var emailService = new EmailService(_configuration);
            await emailService.SendEmailAsync(email, subject, body);

            
            await _context.SaveChangesAsync();
            return new ServiceResult<am_emailVerifications>
            {
                Success = true,
                Message = "Email send successfully",
                Status=1,
                Data = am_Email
                
            };
        }
        /// <summary>
        /// User Email verification
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ServiceResult<am_emailVerifications>> User_Email_verify(string email)
        {
            if (email is null)
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                };
            }
            var existing = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "EmailVerification" && a.userType == "User");
            var existing_user = await _context.am_users.FirstOrDefaultAsync(a => a.email == email);
            if (existing != null && existing_user != null)
            {
                return new ServiceResult<am_emailVerifications> { Success = false, Message = "Email already exists.", Status = -1 };
            }


            string token = CreateToken_email(email, 5);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            DateTime tokenExpiryTime = jwtToken?.ValidTo ?? DateTime.UtcNow;
            string emailPart = email.Substring(0, 3).ToUpper();
          
            am_emailVerifications am_Email = new am_emailVerifications
            {

                Email_id = Guid.CreateVersion7(),
                email = email,
                verificationType = "EmailVerification",
                token = token,
                tokenExpiryTime = tokenExpiryTime,
                isExpired = "F",
                verified = "F",
                userType="User"
            };

            _context.am_emailVerifications.Add(am_Email);


            var verifyUrl = $"{_configuration["MailSettings:BaseUrl"]}/{_configuration["MailSettings:VerifyEmailPath"]}?token={token}&email={email}";


            string subject = "Verify Your Email Address";
            string body = $"<p>Hello,</p><p>Please click the link below to verify your email address:</p><p><a href=\"{verifyUrl}\">Verify Your Email</a></p><p>Thank you!</p>";

            var emailService = new EmailService(_configuration);
            await emailService.SendEmailAsync(email, subject, body);

           
            await _context.SaveChangesAsync();
            return new ServiceResult<am_emailVerifications>
            {
                Success = true,
                Message = "Email send successfully",
                Status = 1,
                Data = am_Email

            };
        }
        public async Task<ServiceResult<am_emailVerifications>> Resend_verification(string email)
        {
            if (email is null)
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                    Status = -2
                };
            }
            if (email == null)
            {
                return new ServiceResult<am_emailVerifications> { Success = false, Message = "Not found", Status = -2 };
            }
            var existing = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType== "EmailVerification" && a.userType== "co-admin");

            if (existing.verified == "T")
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = false,
                    Message = "You have already verifyed",
                    Status = -4
                };
            }

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

            var verifyUrl = $"{_configuration["MailSettings:BaseUrl"]}/{_configuration["MailSettings:VerifyEmailPath"]}?token={token}&email={email}";


            string subject = "Verify Your Email Address";
            string body = $"<p>Hello,</p><p>Please click the link below to verify your email address:</p><p><a href=\"{verifyUrl}\">Verify Your Email</a></p><p>Thank you!</p>";

            var emailService = new EmailService(_configuration);
            await emailService.SendEmailAsync(email, subject, body);


            await _context.SaveChangesAsync();
            return new ServiceResult<am_emailVerifications>
            {
                Success = true,
                Message = "Email send successfully",
                Status = 1
            };
        }
        /// <summary>
        /// User resend verification
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ServiceResult<am_emailVerifications>> _User_Resend_verification(string email)
        {
            if (email is null)
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                    Status = -2
                };
            }
            if (email == null)
            {
                return new ServiceResult<am_emailVerifications> { Success = false, Message = "Not found", Status = -2 };
            }
            var existing = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "EmailVerification" && a.userType=="User");

            if (existing.verified == "T")
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = false,
                    Message = "You have already verifyed",
                    Status = -4
                };
            }

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

            var verifyUrl = $"{_configuration["MailSettings:BaseUrl"]}/{_configuration["MailSettings:VerifyEmailPath"]}?token={token}&email={email}";


            string subject = "Verify Your Email Address";
            string body = $"<p>Hello,</p><p>Please click the link below to verify your email address:</p><p><a href=\"{verifyUrl}\">Verify Your Email</a></p><p>Thank you!</p>";

            var emailService = new EmailService(_configuration);
            await emailService.SendEmailAsync(email, subject, body);


            await _context.SaveChangesAsync();
            return new ServiceResult<am_emailVerifications>
            {
                Success = true,
                Message = "Email send successfully",
                Status = 1
            };
        }
        /// <summary>
        /// Verify user using email token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ServiceResult<am_emailVerifications>> verify(string email, string token)
        {
            if (email is null)
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                };
            }
            var am_email = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "EmailVerification" && a.userType=="User");

            if (am_email.token != token)
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = false,
                    Message = "Invalid token",
                    Status=-1
                };
            }
            if (am_email.verified =="T")
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
                    Status=-3
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
                Status=1,
                Data = am_email
            };
        }
        public async Task<ServiceResult<List<am_users>>> Users_list()
        {
            var user = await _context.am_users.ToListAsync();
            return new ServiceResult<List<am_users>>
            {
                Success = true,
                Message = "Success",
                Status = 1,
                Data = user
            };
        }
        public async Task<ServiceResult<am_users>> Update_profile(am_users am_users, string userId)
        {

            if (am_users == null)
            {
                return new ServiceResult<am_users> { Success = false, Message = "NO data found to insert" };
            }
            Guid guidUserId = Guid.Parse(userId); // use Guid.TryParse if it's not guaranteed to be valid

            var user = await _context.am_users.FirstOrDefaultAsync(a => a.userId == guidUserId);
            user.firstName = am_users.firstName;
            user.lastName = am_users.lastName;
            user.fullName = am_users.firstName + am_users.lastName;
            user.email = am_users.email;
            user.phoneNumber = am_users.phoneNumber;
            _context.am_users.Update(user);
            await _context.SaveChangesAsync();
            return new ServiceResult<am_users>
            {
                Success = true,
                Message = "User created successfully",
                Data = user
            };
        }
        /// <summary>
        /// user reset_password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> send_reset_password(string email)
        {
            if (email == null)
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No email found",
                    Status = -1
                };
            }

            var existing = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "ResetPassword" && a.userType=="User");



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
                    userType="User"
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


            var resetUrl = $"{_configuration["MailSettings:BaseUrl"]}/reset-password?token={token}&email={email}";

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
        /// <summary>
        /// User need to verify email,token for reset_password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ServiceResult<am_emailVerifications>> User_verify(string email, string token)
        {
            if (email is null)
            {
                return new ServiceResult<am_emailVerifications>
                {
                    Success = true,
                    Message = "NO data found",
                };
            }
            var am_email = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "ResetPassword" && a.userType == "User");

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
        public async Task<ServiceResult<string>> reset_password(string token, string email, string password)
        {
            if (email == null)
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "No email found",
                    Status = -1

                };
            }
            var am_email = await _context.am_emailVerifications.FirstOrDefaultAsync(a => a.email == email && a.verificationType == "ResetPassword" && a.userType == "User");
            if (am_email.verified == "F")
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "Not verified ",
                    Status = -2
                };
            }
            if (am_email.token != token)
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "Invalid token",
                    Status = -3
                };
            }
            if (am_email.isExpired == "T")
            {
                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "Token Expired",
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

                return new ServiceResult<string>
                {
                    Success = false,
                    Message = "Token Expired ",
                    Status = -5
                };
            }
            var am_User = await _context.am_users.FirstOrDefaultAsync(a => a.email == email);
            if (am_User != null)
            {
                am_User.password = PasswordHelper.HashPassword(password);
                am_email.verified = "T";
                am_email.isExpired = "F";
                _context.am_emailVerifications.Update(am_email);
            }
            _context.am_users.Update(am_User);
            await _context.SaveChangesAsync();
            return new ServiceResult<string>
            {
                Success = true,
                Message = "Password Updated",
                Status = 1

            };
        }
    }
}
