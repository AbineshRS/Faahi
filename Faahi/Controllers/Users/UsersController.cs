using Faahi.Model.am_vcos;
using Faahi.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _iuser;

        public UsersController(IUser iuser)
        {
            _iuser = iuser;
        }
        [Authorize]
        [HttpPost]
        [Route("create_vendore")]
        public async Task<ActionResult<ap_Vendors>> Create_vendors(ap_Vendors vendors)
        {
            if(vendors == null)
            {
                return Ok("NO data found");
            }
            var created_vendor=await _iuser.Create_vendors(vendors);
            return Ok(created_vendor);
        }
        [Authorize]
        [HttpPost]
        [Route("create_customer")]
        public async Task<ActionResult<ar_Customers>> Create_customer(ar_Customers ar_Customers)
        {
            if(ar_Customers == null)
            {
                return Ok("NO data found");
            }
            var customer= await _iuser.Create_customer(ar_Customers);
            return Ok(customer);
        }
    }
}
