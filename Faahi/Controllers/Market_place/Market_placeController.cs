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
        //[Authorize]
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
    }
}
