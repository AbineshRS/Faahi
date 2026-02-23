using Faahi.Model.sales;
using Faahi.Service.im_products.sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.im_products.sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly Isales _isalse;
        public SalesController(Isales isales)
        {
            _isalse = isales;
        }
        //[Authorize]
        [HttpPost]
        [Route("Add_payment")]
        public async Task<ActionResult<so_payment_type>> Create_payment(so_payment_type so_payment_)
        {
            if (so_payment_ == null)
            {
                return Ok("NO data found");

            }
            var result = await _isalse.Create_payment(so_payment_);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_payment/{company_id}")]
        public async Task<IActionResult> get_payment(Guid company_id)
        {
            if (company_id == null)
            {
                return Ok("NO company_id found");
            }
            var result = await _isalse.get_payment(company_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_payemnt/{payTypeCode}")]
        public async Task<IActionResult> Get_payment(string payTypeCode)
        {
            if(payTypeCode == null)
            {
                return Ok("payTypeCode is null");
            }
            var result = await _isalse.Get_payment(payTypeCode);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update_payemnt/{payTypeCode}")]
        public async Task<ActionResult> Update_payemnt(string payTypeCode,so_payment_type so_Payment_Type)
        {
            if(so_Payment_Type == null)
            {
                return Ok("No data found");
            }
            var result = await _isalse.Update_payment(payTypeCode, so_Payment_Type);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_item_batches/{variant_id}")]
        public async Task<IActionResult> Get_item_batches(Guid variant_id)
        {
            if(variant_id == null)
            {
                return Ok("NO data found");
            }
            var result = await _isalse.Get_item_batches(variant_id);
            return Ok(result);
        }
        //[Authorize]
        [HttpGet]
        [Route("get_item_batches_list/{variant_id}")]
        public async Task<IActionResult> Get_item_batches_list(Guid variant_id)
        {
            if(variant_id == null)
            {
                return Ok("NO data found");
            }
            var result = await _isalse.Get_item_batches_list(variant_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("add_sales")]
        public async Task<ActionResult> Add_sales(so_SalesHeaders so_SalesHeaders)
        {
            if (so_SalesHeaders == null)
            {
                return Ok("Not found");
            }
            var result = await _isalse.Add_sales(so_SalesHeaders);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_sales/{company_id}")]
        public async Task<IActionResult> Get_sales(Guid company_id)
        {
            if (company_id == null)
            {
                return Ok("NO Id found");
            }
            var result = await _isalse.Get_sales(company_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_sales_salesId/{salesId}")]
        public async Task<IActionResult> Get_sales_salesId(Guid salesId)
        {
            if (salesId == null)
            {
                return Ok("No salesId found");
            }
            var result = await _isalse.Get_sales_salesId(salesId);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update_sales/{salesId}")]
        public async Task<ActionResult> Update_sales(Guid salesId, so_SalesHeaders so_SalesHeaders)
        {
            if (salesId == null)
            {
                return Ok("No salesId found");
            }
            var result = await _isalse.Update_sales(salesId, so_SalesHeaders);
            return Ok(result);
        }
    }
}
