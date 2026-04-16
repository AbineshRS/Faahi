using AutoMapper.Configuration.Annotations;
using Faahi.Model.am_users;
using Faahi.Model.Order;
using Faahi.Service.market_place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.Market_place
{
    [Route("api/[controller]")]
    [ApiController]
    public class Market_placeController : ControllerBase
    {
        private readonly IMarket_place_service _market_Place_Service;

        public Market_placeController(IMarket_place_service market_Place_Service)
        {
            _market_Place_Service = market_Place_Service;
        }

        [Authorize]
        [HttpPost]
        [Route("create_market_place_users")]
        public async Task<ActionResult<am_users>> Add_users(am_users users)
        {
            if (users == null)
            {
                return Ok("No data found");
            }
            var result = await _market_Place_Service.Add_users(users);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("add_source")]
        public async Task<ActionResult<om_OrderSources>> Add_source(om_OrderSources om_OrderSources)
        {
            if (om_OrderSources == null)
            {
                return Ok("No data found");
            }
            var result = await _market_Place_Service.Add_source(om_OrderSources);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_market_place_users/{search_text}")]
        public async Task<IActionResult> Get_market_place_users(string search_text)
        {
            var result = await _market_Place_Service.Get_market_place_users(search_text);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("get_sources/{business_id}")]
        public async Task<IActionResult> Get_sources(Guid business_id)
        {
            var result = await _market_Place_Service.Get_sources(business_id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("Add_shipping_address")]
        public async Task<ActionResult<mk_customer_addresses>> Add_shipping_address(mk_customer_addresses mk_customer_)
        {
            if (mk_customer_ == null)
            {
                return Ok("No data found");
            }
            var result = await _market_Place_Service.Add_shipping_address(mk_customer_);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_shipping/{search_text}")]
        public async Task<IActionResult> Get_shipping(string search_text)
        {
            if ( search_text==null)
            {
                return Ok("No data found");
            }
            var result = await _market_Place_Service.Get_shipping(search_text);  
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("Add_zones")]
        public async Task<ActionResult<mk_business_zones>> Add_Zones(mk_business_zones mk_Business_Zones)
        {
            if(mk_Business_Zones == null)
            {
                return Ok("No data found");
            }
            var result = await _market_Place_Service.Add_Zones(mk_Business_Zones);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_zones/{company_id}")]
        public async Task<IActionResult> get_zones(Guid company_id)
        {
            if (company_id == null)
            {
                return Ok("No data found");
            }
            var result = await _market_Place_Service.get_zones(company_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_order_list/{company_id}")]
        public async Task<IActionResult> Get_order_list(Guid company_id)
        {
            var result = await _market_Place_Service.Get_order_list(company_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("check_black_list/{company_id}/{phone_number}")]
        public async Task<IActionResult> check_black_list(Guid company_id,string phone_number)
        {
            if (phone_number==null)
            {
                return Ok("no data found");
            }
            var result = await _market_Place_Service.check_black_list(company_id, phone_number);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_order_list_customer_order_id/{customer_order_id}")]
        public async Task<IActionResult> Get_order_list_customer_order_id(Guid customer_order_id)
        {
            var result = await _market_Place_Service.Get_order_list_customer_order_id(customer_order_id);
            return Ok(result);
        }
    }
}
