using Faahi.Dto;
using Faahi.Model.im_products;
using Faahi.Model.sales;

namespace Faahi.Service.im_products.sales
{
    public interface Isales
    {
        Task<ServiceResult<so_payment_type>> Create_payment(so_payment_type so_Payment_Type);

        Task<ServiceResult<List<so_payment_type>>> get_payment(Guid company_id);

        Task<ServiceResult<so_payment_type>> Get_payment(string payTypeCode);

        Task<ServiceResult<so_payment_type>> Update_payment(string payTypeCode, so_payment_type so_Payment_);

        Task<ServiceResult<List<im_ItemBatches>>> Get_item_batches(Guid variant_id);
        Task<ServiceResult<List<im_ItemBatches>>> Get_item_batches_list(Guid variant_id, decimal requiredQuantity);
    }
}
