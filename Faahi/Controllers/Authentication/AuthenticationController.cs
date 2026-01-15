using Faahi.Controllers.Application;
using Faahi.Dto;
using Faahi.Model;
using Faahi.Model.Email_verify;
using Faahi.Service.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Faahi.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        public AuthenticationController(ApplicationDbContext context,IAuthService authService)
        {
            _context = context;
            _authService = authService; 
        }

        [HttpPost]
        [Route("login/{username}/{password}")]
        public async Task<ActionResult<string>> Login(string username,string password)
        {
            var token = await _authService.LoginAsyn(username,password);
            if (token == null)
            {

                return Ok(new { status = -1, message = "Username / Password invalid" });
            }

            return Ok(new { status = 1, token });

        }
        [HttpPost("refresh-token/{refreshToken}")]
        public ActionResult<AuthResponse> RefreshToken( string refreshToken)
        {
            var response = _authService.RefreshToken(refreshToken);
            if (response == null)
                return Unauthorized("Invalid refresh token");

            return Ok(response);
        }
        [HttpPost]
        [Route("create_account")]
        public async Task<ActionResult> Create_account(am_users am_Users)
        {
            if (am_Users == null)
            {
                return Ok("no data found");
            }
            var register = await _authService.Create_account(am_Users);
            if (register == null)
            {
                return Ok("no data inserted");
            }
            return Ok(new { message = register.Message, user = register.Data });

        }
        [HttpPost]
        [Route("email_verify/{email}/{userType}")]
        public async Task<ActionResult<am_emailVerifications>> Email_verify(string email,string userType)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var verified_email = await _authService.email_verification(email, userType);

            return Ok(verified_email);
        }
        [HttpPost]
        [Route("user_email_verify/{email}")]
        public async Task<ActionResult<am_emailVerifications>> User_Email_verify(string email)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var verified_email = await _authService.User_Email_verify(email);

            return Ok(verified_email);
        }
        [HttpPost]
        [Route("resend_verification/{email}/{userType}")]
        public async Task<ActionResult<am_emailVerifications>> Resend_verification(string email,string userType)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var verified_email = await _authService.Resend_verification(email, userType);

            return Ok(verified_email);
        }
        [HttpPost]
        [Route("User_resend_verification/{email}")]
        public async Task<ActionResult<am_emailVerifications>> _User_Resend_verification(string email)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var verified_email = await _authService._User_Resend_verification(email);

            return Ok(verified_email);
        }
        [HttpPost]
        [Route("verify/{email}/{token}")]
        public async Task<ActionResult<am_emailVerifications>> Verify(string email,string token)
        {
            if(email == null)
            {
                return Ok("No email found");
            }
            var verify_satus = await _authService.verify(email,token);
            return Ok(verify_satus);
        }
        [Authorize]
        [HttpGet("am_users")]
        public async Task<IActionResult> Users_list()
        {
            var user = await _authService.Users_list();
            return Ok(user);
        }


        [Authorize]
        [HttpPost]
        [Route("update_profile/{userId}")]
        public async Task<ActionResult> Update_profile(am_users am_Users,string userId)
        {
            if(am_Users == null)
            {
                return Ok("NO data found");
            }
            var update = await _authService.Update_profile(am_Users,userId);
            return Ok(update);
        }
        [HttpPost]
        [Route("send_reset_password/{email}")]
        public async Task<ActionResult<string>> send_reset_password(string email)
        {
            if (email == null)
            {
                return Ok("no Email Found");
            }
            var reset = await _authService.send_reset_password(email);
            return Ok(reset);
        }
        [HttpPost]
        [Route("User_verify/{email}/{token}")]
        public async Task<ActionResult<am_emailVerifications>> User_Verify(string email, string token)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var verify_satus = await _authService.User_verify(email, token);
            return Ok(verify_satus);
        }
        [HttpPost]
        [Route("reset_password/{token}/{email}/{password}")]
        public async Task<ActionResult<string>> reset_password(string token, string email, string password)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var reset = await _authService.reset_password(token, email, password);
            return Ok(reset);
        }




    }
}
