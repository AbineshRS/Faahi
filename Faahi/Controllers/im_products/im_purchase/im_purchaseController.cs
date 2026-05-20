using Faahi.Dto.Inventory_adjustment;
using Faahi.Dto.Inventory_adjustment.adjustment_rejection;
using Faahi.Dto.Purchase_dto;
using Faahi.Dto.temp;
using Faahi.Model.im_products;
using Faahi.Model.temp_tables;
using Faahi.Service.im_products.im_purchase;
using Faahi.Service.im_products.sales;
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
        [HttpGet]
        [Route("temp_im_purchase_details/{listing_id}")]
        public async Task<IActionResult> temp_im_purchase_details(Guid listing_id)
        {
            var result = await _im_purchase.temp_im_purchase_details(listing_id);
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
        [Route("update_purchase_retuen/{listing_id}")]
        public async Task<ActionResult<im_purchase_listing_dto>> Update_purchase_return(Guid listing_id, im_purchase_listing_dto im_Purchase_)
        {
            if (listing_id == null || im_Purchase_ == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Update_purchase_return(listing_id, im_Purchase_);
            
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
        [Authorize]
        [HttpPost]
        [Route("Add_purchase_listing_excel")]
        public async Task<ActionResult<im_purchase_listing>> Add_purchase_listing_excel(im_purchase_listing im_Purchase_Listing)
        {
            if(im_Purchase_Listing == null)
            {
                return Ok("No data found");

            }
            var result = await _im_purchase.Add_purchase_listing_excel(im_Purchase_Listing);
            return Ok(result);
        }
        [HttpGet]
        [Route("get_product_data/{product_id}")]
        public async Task<IActionResult> Get_product_data(Guid product_id)
        {
            if (product_id == null)
            {
                return Ok("No product_id found");
            }
            var result = await _im_purchase.Get_product_data(product_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_purchase_list/{searchText}")]
        public async Task<IActionResult> Get_purchase_list(string searchText)
        {
            if(searchText == null)
            {
                return Ok("No searchText found");
            }
            var result = await _im_purchase.Get_purchase_list(searchText);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_inventory/{store_id}/{variant_id}")]
        public async Task<IActionResult> Get_inventory(Guid store_id,Guid variant_id)
        {
            if(store_id==null|| variant_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Get_inventory(store_id, variant_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("get_inventory_report/{store_id}/{start_date}/{end_date}/{vendor_id}/{searchText}")]
        public async Task<IActionResult> Get_inventory_report(Guid store_id,DateTime? start_date,DateTime? end_date,string vendor_id,string searchText)
        {
            if (store_id == Guid.Empty)
                return Ok("No store_id found");

            Guid? vid = null;
            if (!string.IsNullOrWhiteSpace(vendor_id) &&
                !vendor_id.Equals("null", StringComparison.OrdinalIgnoreCase) &&
                Guid.TryParse(vendor_id, out var parsed))
            {
                vid = parsed;
            }

            var result = await _im_purchase.Get_inventory_report(store_id, start_date, end_date, vid, searchText);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("Add_transfer")]
        public async Task<IActionResult> Add_transfer(im_StockTransferHeader im_StockTransferHeader)
        {
            if(im_StockTransferHeader == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Add_transfer(im_StockTransferHeader);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("Get_transfer_data/{transfer_id}")]
        public async Task<IActionResult> Get_transfer_data(Guid transfer_id)
        {
            if (transfer_id == null)
            {
                return Ok("no data found");
            }
            var result = await _im_purchase.Get_transfer_data(transfer_id);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        [Route("Get_transfer_list/{store_id}")]
        public async Task<IActionResult> Get_transfer_list(Guid store_id)
        {
            if (store_id == null)
            {
                return Ok("no data found");
            }
            var result = await _im_purchase.Get_transfer_list(store_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("Update_transfer/{transfer_id}")]
        public async Task<IActionResult> Update_transfer(Guid transfer_id, im_StockTransferHeader im_StockTransferHeader)
        {
            if (transfer_id == null)
            {
                return Ok("no data found");
            }
            var result = await _im_purchase.Update_transfer(transfer_id, im_StockTransferHeader);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("Update_inv_hold/{store_variant_inventory_id}")]
        public async Task<IActionResult> Update_inv_hold(Guid store_variant_inventory_id)
        {
            if (store_variant_inventory_id == null)
            {
                return Ok("no Id found");
            }
            var result = await _im_purchase.Update_inv_hold(store_variant_inventory_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("Add_adjustment")]
        public async Task<IActionResult> Add_adjustment(inventory_adjustment_header_dto inventory_Adjustment_Header)
        {
            if(inventory_Adjustment_Header == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Add_adjustment(inventory_Adjustment_Header);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("Get_adjustment_list/{store_id}")]
        public async Task<IActionResult> Get_adjustment_list(Guid store_id)
        {
            if (store_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Get_adjustment_list(store_id);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("Get_adjustment_lines/{adjustment_id}")]
        public async Task<IActionResult> Get_adjustment_lines(Guid adjustment_id)
        {
            if (adjustment_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Get_adjustment_lines(adjustment_id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("Update_adjustment/{adjustment_id}")]
        public async Task<IActionResult> Update_adjustment(Guid adjustment_id, inventory_adjustment_header_update_req_dto inventory_Adjustment_Lines_Response_Dto )
        {
            if (adjustment_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Update_adjustment(adjustment_id, inventory_Adjustment_Lines_Response_Dto);
            return Ok(result);
        }

        //[Authorize]
        [HttpGet]
        [Route("Get_adjustment_list_lines/{store_id}")]
        public async Task<IActionResult> Get_adjustment_list_lines(Guid store_id)
        {
            if (store_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Get_adjustment_list_lines(store_id);
            return Ok(result);
        }
        //[Authorize]
        [HttpPost]
        [Route("Add_rejected_adjustment")]
        public async Task<IActionResult> Add_rejected_adjustment(List<im_random_Stock_reject_dto> im_Random_Stock_Reject_Dtos)
        {
            if (!im_Random_Stock_Reject_Dtos.Any())
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Add_rejected_adjustment(im_Random_Stock_Reject_Dtos);
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("add_adjustment_inv")]
        public async Task<IActionResult> add_adjustment_inv(List<temp_stock_ad_lines_dto> temp_Stock_Ad_Lines)
        {
            if(temp_Stock_Ad_Lines == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.add_adjustment_inv(temp_Stock_Ad_Lines);
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("get_adjusted_inv/{store_id}/{StartDate}/{EndDate}")]
        public async Task<IActionResult> Get_adjusted_inv(Guid store_id,DateOnly StartDate, DateOnly EndDate)
        {
            if (store_id == null)
            {
                return Ok("No data found");
            }
            var result = await _im_purchase.Get_adjusted_inv(store_id,StartDate, EndDate);
            return Ok(result);
        }
        //[Authorize]
        //[HttpDelete]
        //[Route("delete_inv_adjustment_lines/{adjustment_detail_id}")]
        //public async Task<IActionResult> Delete_inv_adjustment_detail_id(Guid adjustment_detail_id)
        //{
        //    if (adjustment_detail_id == null)
        //    {
        //        return Ok("No data found");
        //    }
        //    var result = await _im_purchase.Delete_inv_adjustment_detail_id(adjustment_detail_id);
        //    return Ok(result);
        //}
    }
}
