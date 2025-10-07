using Faahi.Dto;
using Faahi.Model.co_business;
using Faahi.Model.Email_verify;
using Faahi.Model.im_products;
using Faahi.Service.CoBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Faahi.Controllers.Cobusiness
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobusinessController : ControllerBase
    {
        private readonly ICoBusinessservice _co_businessService;
        public CobusinessController(ICoBusinessservice co_businessService)
        {
            _co_businessService = co_businessService;
        }

        [HttpPost]
        [Route("create_account")]
        public async Task<ActionResult> Create_account(co_business co_business)
        {
            if (co_business == null)
            {
                return Ok("no data found");
            }
            var data = await _co_businessService.Create_account(co_business);
            return Ok(data);
        }
        [HttpPost]
        [Route("upload_logo/{company_id}")]
        public async Task<ActionResult<string>> Upload(IFormFile formFil, string company_id)
        {
            if (formFil is null)
            {
                return Ok("Not found");
            }
            var upload = await _co_businessService.Upload_logo(formFil, company_id);
            return Ok(upload);
        }
        [HttpPost]
        [Route("login/{username}/{password}")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            var token = await _co_businessService.LoginAsyn(username, password);
            if (token == null)
            {

                return Ok(new { status = -1, message = "Username / Password invalid" });
            }

            return Ok(new { status = 1, token });

        }
        [HttpPost]
        [Route("send_reset_password/{email}")]
        public async Task<ActionResult<string>> send_reset_password(string email)
        {
            if (email == null)
            {
                return Ok("no Email Found");
            }
            var reset = await _co_businessService.send_reset_password(email);
            return Ok(reset);
        }
        [HttpPost]
        [Route("verify/{email}/{token}")]
        public async Task<ActionResult<am_emailVerifications>> Verify(string email, string token)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var verify_satus = await _co_businessService.verify(email, token);
            return Ok(verify_satus);
        }
        [HttpPost]
        [Route("password_verify/{email}/{token}")]
        public async Task<ActionResult<am_emailVerifications>> Password_Verify(string email, string token)
        {
            if (email == null)
            {
                return Ok("No email found");
            }
            var verify_satus = await _co_businessService.Password_Verify(email, token);
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
            var reset = await _co_businessService.reset_password(token, email, password);
            return Ok(reset);
        }
        [Authorize]
        [HttpPost]
        [Route("update-profile/{company_id}")]
        public async Task<ActionResult> Update_profile(co_business co_Address, string company_id)
        {

            if (company_id == null)
            {
                return Ok("Not found Company_id");
            }
            var update = await _co_businessService.Update_profile(co_Address, company_id);
            return Ok(update);
        }
        [Authorize]
        [HttpPost]
        [Route("create_sh_avilable_country")]
        public async Task<ActionResult<co_avl_countries>> CreateAvailableCountry(co_avl_countries co_Avl_Countries)
        {
            if (co_Avl_Countries == null)
            {
                return Ok("no data found");
            }
            var currency = await _co_businessService.CreateAvailableCountry(co_Avl_Countries);
            return Ok(currency);
        }
        [Authorize]
        [HttpGet]
        [Route("currency_list")]
        public async Task<IActionResult> CurrencyList()
        {
            var currency_list = await _co_businessService.CurrencyList();
            return Ok(currency_list);
        }
        [Authorize]
        [HttpPost]
        [Route("im_site")]
        public async Task<ActionResult<im_site>> Create_im_site(im_site imsite)
        {
            if (imsite == null)
            {
                return Ok("no data found");
            }
            var created = await _co_businessService.Create_im_site(imsite);
            return Ok(created);
        }
        [Authorize]
        [HttpGet]
        [Route("imsite_list")]
        public async Task<IActionResult> imsite_list()
        {

            var imsite = await _co_businessService.imsite_list();
            return Ok(imsite);
        }
        [Authorize]
        [HttpGet]
        [Route("get_imsite/{site_id}")]
        public async Task<IActionResult> Get_im_site(string site_id)
        {
            if (site_id == null)
            {
                return Ok("No data found");
            }
            var imsite = await _co_businessService.Get_im_site(site_id);
            return Ok(imsite);
        }
        [Authorize]
        [HttpGet]
        [Route("get_imsite_company/{company_id}")]
        public async Task<IActionResult> Get_im_site_company(string company_id)
        {
            if (company_id == null)
            {
                return Ok("No data found");
            }
            var imsite = await _co_businessService.Get_im_site_company(company_id);
            return Ok(imsite);
        }
        [Authorize]
        [HttpPost]
        [Route("update_imsite/{site_id}")]
        public async Task<ActionResult<im_site>> Update_imsite(string site_id, im_site imsite)
        {
            if (site_id == null || imsite == null)
            {
                return Ok("not found");
                
            }
            var updated = await _co_businessService.Update_imsite(site_id, imsite);
            return Ok(updated);
        }
        [HttpGet]
        [Route("Dekiru/{searchTerm}")]
        public async Task<ActionResult> Dekiru(string searchTerm)
        {
            var data = await _co_businessService.Dekiru(searchTerm);
            return Ok(data);
        }

    }
}
