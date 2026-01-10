using Faahi.Service.Admin;
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
        [Route("login/{username}/{password}")]
        public async Task<ActionResult> Login(string username, string password)
        {
            var data = await _admin.LoginAsyn(username, password);
            return Ok(data);
        }
    }
}
