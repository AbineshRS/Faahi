using Faahi.Dto.sales_dto;
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
        public async Task<ActionResult<st_Parties>> Create_customer(st_Parties ar_Customers)
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
        [Route("get_all_customer_search/{company_id}/{search_text}")]
        public async Task<IActionResult> get_all_customer_search(Guid company_id,string search_text)
        {
            if (string.IsNullOrEmpty(search_text))
            {
                return Ok("No search_text");
            }
            var result = await _iuser.get_all_customer_search(company_id,search_text);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_all_vendors_search/{company_id}/{search_text}")]
        public async Task<IActionResult> get_all_vendors_search(Guid company_id,string search_text)
        {
            if (string.IsNullOrEmpty(search_text))
            {
                return Ok("No search_text");
            }
            var result = await _iuser.get_all_vendors_search(company_id,search_text);
            return Ok(result);
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

        [Authorize]
        [HttpPost]
        [Route("create_party")]
        public async Task<ActionResult<st_Parties>> Create_other_party(st_Parties st_Parties)
        {
            if (st_Parties == null)
            {
                return Ok("No data found");
            }
            var result = await _iuser.Create_other_party(st_Parties);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("get_all_parties/{company_id}")]
        public async Task<ActionResult<List<st_Parties>>> Get_all_parties(Guid company_id)
        {
            if (company_id == Guid.Empty)
            {
                return Ok("No data found");
            }
            var result = await _iuser.Get_all_parties(company_id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("order_list_customer/{customer_id}")]
        public async Task<IActionResult> Order_list_customer(Guid customer_id)
        {
            if (customer_id == null)
            {
                return Ok("no data found");
            }
            var result = await _iuser.Order_list_customer(customer_id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("Update_sales_payment/{salesId}")]
        public async Task<IActionResult> Update_sales_payment([FromForm] sales_customer_update_payment_dto sales_Customer)
        {
            if (sales_Customer == null)
            {
                return Ok("no data found");
            }
            var result = await _iuser.Update_sales_payment(sales_Customer);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("customer_payment_history/{salesId}")]
        public async Task<IActionResult> Customer_payment_history(Guid salesId)
        {
            var result = await _iuser.Customer_payment_history(salesId);
            return Ok(result);
        }
    }
}
