using Faahi.Dto;
using Faahi.Dto.Inventory_adjustment;
using Faahi.Dto.Inventory_adjustment.adjustment_rejection;
using Faahi.Dto.Purchase_dto;
using Faahi.Dto.temp;
using Faahi.Model.Accounts;
using Faahi.Model.im_products;
using Faahi.Model.temp_tables;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Service.im_products.im_purchase
{
    public interface Iim_purchase
    {
        Task<ServiceResult<im_purchase_listing>> Create_im_purchase(im_purchase_listing im_Purchase_Listing);

        Task<ServiceResult<List<PurchaseListDto>>> Purchase_list(Guid site_id);

        Task<ServiceResult<im_purchase_listing>> Calculate_discount_avg(Guid listing_id);

        Task<ServiceResult<im_purchase_listing>> im_purchase_details(Guid listing_id);

        Task<ServiceResult<List<temp_im_purchase_listing_details>>> temp_im_purchase_details(Guid listing_id);

        Task<ServiceResult<im_purchase_listing>> Update_purchase(Guid listing_id,im_purchase_listing im_Purchas);

        Task<ServiceResult<im_purchase_listing_dto>> Update_purchase_return(Guid listing_id, im_purchase_listing_dto im_Purchas);

        Task<ServiceResult<im_GoodsReceiptHeaders>> Add_GoodsHeader(Guid listing_id);

        Task<ServiceResult<im_purchase_listing>> Update_purchase_calculation(Guid listing_id, im_purchase_listing im_Purchas);

        Task<ServiceResult<im_bin_location>>  Add_bin_No(im_bin_location im_Bin_Location);

        Task<ServiceResult<List<im_bin_location>>> Get_bin_Locations(Guid store_id);

        Task<ServiceResult<im_purchase_listing_details>> Delete_im_purchase_listing(Guid detail_id);

        Task<ServiceResult<im_purchase_listing>> Delete_purchase(Guid listing_id);

        Task<ServiceResult<List<im_ItemBatches>>> get_batches(Guid store_id);

        Task<ServiceResult<List<im_ItemBatches>>> get_batches_search(Guid store_id,string searchText);

        Task<ServiceResult<im_ItemBatches>> Get_item_batch(Guid item_batch_id);

        Task<ServiceResult<im_ItemBatches>> update_item_batch(Guid item_batch_id,im_ItemBatches item_batch);

        Task<ServiceResult<im_purchase_listing>> Add_purchase_listing_excel(im_purchase_listing im_Purchase_Listing );

        Task<ServiceResult<im_Products>> Get_product_data(Guid product_id);

        Task<ServiceResult<List<im_purchase_listing>>> Get_purchase_list(string searchText);

        Task<ServiceResult<Dictionary<string, List<im_GoodsReceiptLines>>>> Get_inventory(Guid store_id, Guid variant_id);

        Task<ServiceResult<List<im_purchase_listing>>> Get_inventory_report(Guid store_id, DateTime? start_date, DateTime? end_date,Guid? vendor_id,string searchText);

        Task<ServiceResult<im_purchase_listing_dto>> Add_retuen_purchase(Guid listing_id, im_purchase_listing_dto _Purchase_Listing);

        Task<ServiceResult<gl_JournalHeaders>> Add_Journal_header(Guid listing_id);

        Task<ServiceResult<im_StockTransferHeader>> Add_transfer(im_StockTransferHeader _StockTransferHeader);

        Task<ServiceResult<List<StockTransferFlatDto>>> Get_transfer_data(Guid transfer_id);

        Task<ServiceResult<List<im_StockTransferHeader_data_dto>>> Get_transfer_list(Guid store_id);

        Task<ServiceResult<im_StockTransferHeader>> Update_transfer(Guid transfer_id, im_StockTransferHeader im_StockTransferHeader);

        Task<ServiceResult<store_inv_hold_dto>> Update_inv_hold(Guid store_variant_inventory_id);
        
        Task<ServiceResult<inventory_adjustment_header_dto>> Add_adjustment(inventory_adjustment_header_dto inventory_Adjustment_Header);

        Task<ServiceResult<List<inventory_adjustment_header_response_dto>>> Get_adjustment_list(Guid store_id);
        
        Task<ServiceResult<List<inventory_adjustment_lines_response_dto>>> Get_adjustment_lines(Guid adjustment_id);

        Task<ServiceResult<inventory_adjustment_header_update_req_dto>> Update_adjustment(Guid adjustment_id, inventory_adjustment_header_update_req_dto inventory_Adjustment);

        Task<ServiceResult<InventoryAdjustmentResponseDto>> Get_adjustment_list_lines(Guid store_id);

        Task<ServiceResult<List<im_random_Stock_reject_dto>>> Add_rejected_adjustment(List<im_random_Stock_reject_dto> im_Random_Stock_Reject_Dtos);

        Task<ServiceResult<List<temp_stock_ad_lines_dto>>> add_adjustment_inv(List<temp_stock_ad_lines_dto> temp_Stock_Ad_Lines);

    }
}
