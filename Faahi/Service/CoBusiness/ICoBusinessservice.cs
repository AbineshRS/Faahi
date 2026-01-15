using Faahi.Dto;
using Faahi.Model.co_business;
using Faahi.Model.Email_verify;
using Faahi.Model.im_products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Faahi.Service.CoBusiness
{
    public interface ICoBusinessservice
    {

        Task<ServiceResult<co_business>> Create_account(co_business co_business);

        Task<ServiceResult<List<co_business>>> Company_list();

        Task<AuthResponse> LoginAsyn(string username,string password);

        Task<ServiceResult<string>> send_reset_password(string email);

        Task<ServiceResult<am_emailVerifications>> verify(string email, string token,string userType);

        Task<ServiceResult<am_emailVerifications>> Password_Verify(string email, string token);

        Task<ServiceResult<string>> reset_password(string token, string email,string password);

        Task<ActionResult<ServiceResult<string>>> Upload_logo(IFormFile formFile, Guid company_id);

        Task<ServiceResult<co_business>> Get_company(Guid company_id);

        Task<ServiceResult<co_business>> Update_profile(co_business co_business,string company_id);

        Task<ServiceResult<ActionResult>> Inactive_company(Guid company_id);

        Task<ServiceResult<co_avl_countries>> CreateAvailableCountry(co_avl_countries co_Avl_Countries);

        Task<ServiceResult<List<co_avl_countries>>> CurrencyList();

        Task<ServiceResult<im_site>> Create_im_site(im_site im_Site);

        Task<ServiceResult<im_site>> Get_im_site(string site_id);

        Task<ServiceResult<List<im_site>>> Get_im_site_company(string company_id);

        Task<ServiceResult<List<im_site>>> imsite_list();

        Task<ServiceResult<im_site>> Update_imsite(string site_id, im_site imsite);

        Task<ServiceResult<im_site_users>> Add_site_users(im_site_users im_Site_Users);

        Task<ServiceResult<im_site_users>> site_user(Guid user_id);

        Task<ServiceResult<List<im_site_users>>> site_user_list(Guid company_id);

        Task<ServiceResult<im_site_users>> Update_site_users(Guid userId, im_site_users im_Site_Users);

        Task<ServiceResult<List<co_business>>> Dekiru(string searchTerm);

        
    }
}
