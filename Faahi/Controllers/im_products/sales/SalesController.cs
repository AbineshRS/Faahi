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
        [Route("get_payemnt/{payment_type_id}")]
        public async Task<IActionResult> Get_payment(Guid payment_type_id)
        {
            if (payment_type_id == null)
            {
                return Ok("payTypeCode is null");
            }
            var result = await _isalse.Get_payment(payment_type_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update_payemnt/{payment_type_id}")]
        public async Task<ActionResult> Update_payemnt(Guid payment_type_id, so_payment_type so_Payment_Type)
        {
            if (so_Payment_Type == null)
            {
                return Ok("No data found");
            }
            var result = await _isalse.Update_payment(payment_type_id, so_Payment_Type);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_item_batches/{variant_id}")]
        public async Task<IActionResult> Get_item_batches(Guid variant_id)
        {
            if (variant_id == null)
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
            if (variant_id == null)
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
        [Authorize]
        [HttpGet]
        [Route("get_sales_report_by_date/{store_id}/{start_date}/{end_date}")]
        public async Task<IActionResult> Get_sales_report_by_date(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_report_by_date(store_id, start_date, end_date);
            return Ok(result);
        }
        //[Authorize]
        [HttpGet]
        [Route("get_sales_deatiled_by_date/{store_id}/{start_date}/{end_date}")]
        public async Task<IActionResult> Get_sales_detailed_by_date(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_detailed_by_date(store_id, start_date, end_date);
            return Ok(result);
        }
        //[Authorize]
        [HttpGet]
        [Route("get_sales_deatiled_by_dayreport/{store_id}/{start_date}/{end_date}")]
        public async Task<IActionResult> Get_sales_detailed_by_day_report(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_detailed_by_day_report(store_id, start_date, end_date);
            return Ok(result);
        }
        //[Authorize]
        [HttpGet]
        [Route("get_sales_deatiled_by_customer/{store_id}")]
        public async Task<IActionResult> Get_sales_detailed_by_customer(Guid store_id, string? customer, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_detailed_by_customer(store_id,customer ,start_date, end_date);
            return Ok(result);
        }
        [HttpGet]
        [Route("get_sales_deatiled_by_product/{store_id}")]
        public async Task<IActionResult> Get_sales_detailed_by_product(Guid store_id, string? ProductSku, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_detailed_by_product(store_id, ProductSku, start_date, end_date);
            return Ok(result);
        }
        [HttpGet]
        [Route("Get_sales_tax_report/{store_id}/{start_date}/{end_date}")]
        public async Task<IActionResult> Get_sales_tax_report(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_tax_report(store_id, start_date, end_date);
            return Ok(result);
        }
        [HttpGet]
        [Route("Get_sales_out_standing/{store_id}/{start_date}/{end_date}")]
        public async Task<IActionResult> Get_sales_out_standing(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_out_standing(store_id, start_date, end_date);
            return Ok(result);
        }
        [HttpGet]
        [Route("Get_sales_hourly_base/{store_id}/{start_date}/{end_date}")]
        public async Task<IActionResult> Get_sales_hourly_base(Guid store_id, DateOnly? start_date, DateOnly? end_date)
        {
            if (store_id == null)
            {
                return Ok("No store_id found");
            }
            var result = await _isalse.Get_sales_hourly_base(store_id, start_date, end_date);
            return Ok(result);
        }
    }
}
