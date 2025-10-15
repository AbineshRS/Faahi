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
        [Authorize]
        [HttpGet]
        [Route("get_all_customer/{company_id}")]
        public async Task<ActionResult<ar_Customers>> Get_all_customer(Guid company_id)
        {
            if(company_id == null)
            {
                return Ok("NO data found");
            }
            var customer= await _iuser.Get_all_customer(company_id);
            return Ok(customer);
        }
        [Authorize]
        [HttpGet]
        [Route("get_all_vendors/{company_id}")]
        public async Task<ActionResult<ap_Vendors>> Get_all_vendors(Guid company_id)
        {
            if (company_id == null)
            {
                return Ok("NO data found");
            }
            var vendors = await _iuser.Get_all_vendors(company_id);
            return Ok(vendors);
        }
    }
}
