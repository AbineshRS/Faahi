using Faahi.Model.im_products;
using Faahi.Service.im_products.im_purchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Controllers.im_products.im_purchase
{
    [Route("api/[controller]")]
    [ApiController]
    public class im_purchaseController : ControllerBase
    {
        private readonly Iim_purchase _im_purchase;

        public im_purchaseController(Iim_purchase im_purchase)
        {
            _im_purchase = im_purchase;
        }

        [Authorize]
        [HttpPost]
        [Route("create_im_purchase")]
        public async Task<ActionResult<im_purchase_listing>> Create_im_purchase(im_purchase_listing im_Purchase_Listing)
        {
            if (im_Purchase_Listing == null)
            {
                return Ok("no data found");
            }
            var created = await _im_purchase.Create_im_purchase(im_Purchase_Listing);
            return Ok(created);
        }
        [Authorize]
        [HttpGet]
        [Route("im_purchase_list/{site_id}")]
        public async Task<IActionResult> Purchase_list(Guid site_id)
        {
            var result = await _im_purchase.Purchase_list(site_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("im_purchase_details/{listing_id}")]
        public async Task<IActionResult> im_purchase_details(Guid listing_id)
        {
            var result = await _im_purchase.im_purchase_details(listing_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update_purchase/{listing_id}")]
        public async Task<ActionResult<im_purchase_listing>> Update_purchase(Guid listing_id, im_purchase_listing im_Purchase_)
        {
            if(listing_id==null || im_Purchase_ == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Update_purchase(listing_id, im_Purchase_);
            return Ok(result);
        }
    }
}
