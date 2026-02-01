using Faahi.Dto;
using Faahi.Model.im_products;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Service.im_products.im_purchase
{
    public interface Iim_purchase
    {
        Task<ServiceResult<im_purchase_listing>> Create_im_purchase(im_purchase_listing im_Purchase_Listing);

        Task<ServiceResult<List<im_purchase_listing>>> Purchase_list(Guid site_id);

        Task<ServiceResult<im_purchase_listing>> im_purchase_details(Guid listing_id);

        Task<ServiceResult<im_purchase_listing>> Update_purchase(Guid listing_id,im_purchase_listing im_Purchas);

        Task<ServiceResult<im_purchase_listing>> Update_purchase_calculation(Guid listing_id, im_purchase_listing im_Purchas);

        Task<ServiceResult<im_bin_location>>  Add_bin_No(im_bin_location im_Bin_Location);

        Task<ServiceResult<List<im_bin_location>>> Get_bin_Locations(Guid store_id);

        Task<ServiceResult<im_purchase_listing_details>> Delete_im_purchase_listing(Guid detail_id);

        Task<ServiceResult<im_purchase_listing>> Delete_purchase(Guid listing_id);

        Task<ServiceResult<List<im_ItemBatches>>> get_batches(Guid store_id);

        Task<ServiceResult<List<im_ItemBatches>>> get_batches_search(Guid store_id,string searchText);

        Task<ServiceResult<im_ItemBatches>> Get_item_batch(Guid item_batch_id);

        Task<ServiceResult<im_ItemBatches>> update_item_batch(Guid item_batch_id,im_ItemBatches item_batch);
    }
}
