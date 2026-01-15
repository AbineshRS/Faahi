using Faahi.Model.Admin;
using Faahi.Service.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly Iadmin _admin;

        public AdminController(Iadmin admin)
        {
            _admin = admin;
        }
        [HttpPost]
        [Route("add_admin")]
        public async Task<ActionResult<super_admin>> AddAdmin(super_admin admin)
        {
            var data = await _admin.AddAdminAsync(admin);
            return Ok(data);
        }

        [HttpPost]
        [Route("login/{email}/{password}")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var data = await _admin.LoginAsyn(email, password);
            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        [Route("add_countries")]
        public async Task<ActionResult> Addcountry_(sa_country_regions regions)
        {
            var data = await _admin.Addcountry(regions);
            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        [Route("add_regions")]
        public async Task<ActionResult> Addregion(sa_regions countries)
        {
            var data = await _admin.Addregion(countries);
            return Ok(data);
        }
        //[Authorize]
        [HttpGet]
        [Route("regions_list")]
        public async Task<ActionResult> GetRegionsList()
        {
            var data = await _admin.GetRegionsList();
            return Ok(data);
        }
    }
}
