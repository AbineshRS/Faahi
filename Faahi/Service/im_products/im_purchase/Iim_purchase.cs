using Faahi.Dto;
using Faahi.Model.im_products;

namespace Faahi.Service.im_products.im_purchase
{
    public interface Iim_purchase
    {
        Task<ServiceResult<im_purchase_listing>> Create_im_purchase(im_purchase_listing im_Purchase_Listing);
    }
}
