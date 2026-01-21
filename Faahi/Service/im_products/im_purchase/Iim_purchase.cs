using Faahi.Dto;
using Faahi.Model.im_products;

namespace Faahi.Service.im_products.im_purchase
{
    public interface Iim_purchase
    {
        Task<ServiceResult<im_purchase_listing>> Create_im_purchase(im_purchase_listing im_Purchase_Listing);

        Task<ServiceResult<List<im_purchase_listing>>> Purchase_list(Guid site_id);

        Task<ServiceResult<im_purchase_listing>> im_purchase_details(Guid listing_id);

        Task<ServiceResult<im_purchase_listing>> Update_purchase(Guid listing_id,im_purchase_listing im_Purchas);
    }
}
