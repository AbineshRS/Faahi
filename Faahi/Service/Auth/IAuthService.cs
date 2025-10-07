using Faahi.Dto;
using Faahi.Model;
using Faahi.Model.Email_verify;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Service.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsyn(string username,string password);

        Task<ServiceResult<am_users>> Create_account(am_users user);

        AuthResponse RefreshToken(string request);

        Task<ServiceResult<am_users>> Update_profile(am_users user,string userId);

        Task<ServiceResult<List<am_users>>> Users_list();

        Task<ServiceResult<am_emailVerifications>> email_verification(string email);

        Task<ServiceResult<am_emailVerifications>> User_Email_verify(string email);

        Task<ServiceResult<am_emailVerifications>> Resend_verification(string email);

        Task<ServiceResult<am_emailVerifications>> _User_Resend_verification(string email);

        Task<ServiceResult<am_emailVerifications>> verify(string email,string token);

        Task<ServiceResult<string>> send_reset_password(string email);

        Task<ServiceResult<am_emailVerifications>> User_verify(string email, string token);

        Task<ServiceResult<string>> reset_password(string token, string email, string password);
    }
}
