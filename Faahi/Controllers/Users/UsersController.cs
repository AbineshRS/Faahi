using Faahi.Model.am_vcos;
using Faahi.Model.Shared_tables;
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
        public async Task<ActionResult<st_Parties>> Create_vendors(st_Parties vendors)
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
        [HttpPost]
        [Route("update_arcustomer/{customer_id}")]
        public async Task<ActionResult<ar_Customers>> Update_arcustomer(Guid customer_id,ar_Customers ar_Customers)
        {
            if(customer_id==null || ar_Customers == null)
            {
                return Ok("no data found");
            }
            var result = await _iuser.Update_arcustomer(customer_id,ar_Customers);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update_apvendor/{vendor_id}")]
        public async Task<ActionResult<ap_Vendors>> Update_apvendor(Guid vendor_id, ap_Vendors ap_Vendors)
        {
            if(vendor_id == null || ap_Vendors == null)
            {
                return Ok("no data found");
            }
            var result = await _iuser.Update_apvendor(vendor_id, ap_Vendors);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_customer/{customer_id}")]
        public async Task<IActionResult> Get_customer(Guid customer_id)
        {
            if (customer_id == null)
            {
                return Ok("not found");
            }
            var result = await _iuser.Get_customer(customer_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_vendor/{vendor_id}")]
        public async Task<IActionResult> Get_vendor(Guid vendor_id)
        {
            if (vendor_id == null)
            {
                return Ok("not found");
            }
            var result = await _iuser.Get_vendor(vendor_id);
            return Ok(result);
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
