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
            if (listing_id == null || im_Purchase_ == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Update_purchase(listing_id, im_Purchase_);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("Update_purchase_calculation/{listing_id}")]
        public async Task<IActionResult> Update_purchase_calculation(Guid listing_id, im_purchase_listing im_Purchase_Listing)
        {
            var result = await _im_purchase.Update_purchase_calculation(listing_id, im_Purchase_Listing);
            return Ok(result);
        }
        //[Authorize]
        [HttpPost]
        [Route("add_bin_No")]
        public async Task<ActionResult<im_bin_location>> Add_bin_No(im_bin_location im_Bin_Location)
        {
            if (im_Bin_Location == null)
            {
                return Ok("no data found");
            }
            var result = await _im_purchase.Add_bin_No(im_Bin_Location);
            // Logic to add bin location would go here
            return Ok(result);
        }

        //[Authorize]
        [HttpGet]
        [Route("get_bin_No/{store_id}")]
        public async Task<IActionResult> Get_bin_No(Guid store_id)
        {
            if (store_id == null)
            {
                return Ok("no data found");
            }
            var result = await _im_purchase.Get_bin_Locations(store_id);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete_im_purchase_listing/{detail_id}")]
        public async Task<IActionResult> Delete_im_purchase_listing(Guid detail_id)
        {
            var result = await _im_purchase.Delete_im_purchase_listing(detail_id);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete_purchase/{listing_id}")]
        public async Task<IActionResult> Delete_purchase(Guid listing_id)
        {
            var result = await _im_purchase.Delete_purchase(listing_id);
            return Ok(result);

        }
        [Authorize]
        [HttpGet]
        [Route("get_batches/{store_id}")]
        public async Task<IActionResult> get_batches(Guid store_id)
        {
            if (store_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.get_batches(store_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_batches_search/{store_id}/{searchText}")]
        public async Task<IActionResult> get_batches_search(Guid store_id,string searchText)
        {
            if (store_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.get_batches_search(store_id, searchText);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_item_batch/{item_batch_id}")]
        public async Task<IActionResult> Get_item_batch(Guid item_batch_id)
        {
            if (item_batch_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Get_item_batch(item_batch_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("update_item_batch/{item_batch_id}")]
        public async Task<ActionResult<im_ItemBatches>> update_item_batch(Guid item_batch_id,im_ItemBatches item_batch)
        {
            if (item_batch_id == null)
            {
                return Ok("No Id found");
            }
            var result = await _im_purchase.update_item_batch(item_batch_id, item_batch);
            return Ok(result);
        }
    }
}
