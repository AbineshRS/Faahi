using Faahi.Dto;
using Faahi.Dto.sales_dto;
using Faahi.Model.im_products;
using Faahi.Model.sales;
using Microsoft.AspNetCore.Mvc;

namespace Faahi.Service.im_products.sales
{
    public interface Isales
    {
        Task<ServiceResult<so_payment_type>> Create_payment(so_payment_type so_Payment_Type);

        Task<ServiceResult<List<so_payment_type>>> get_payment(Guid company_id);

        Task<ServiceResult<so_payment_type>> Get_payment(Guid payment_type_id);

        Task<ServiceResult<so_payment_type>> Update_payment(Guid payment_type_id, so_payment_type so_Payment_);

        Task<ServiceResult<List<im_ItemBatches>>> Get_item_batches(Guid variant_id);
        Task<ServiceResult<List<im_ItemBatches>>> Get_item_batches_list(Guid variant_id);

        Task<ActionResult<ServiceResult<so_SalesHeaders>>> Add_sales(so_SalesHeaders so_SalesHeaders);

        Task<ServiceResult<List<so_SalesHeaders>>> Get_sales(Guid company_id);

        Task<ServiceResult<so_SalesHeaders>> Get_sales_salesId(Guid salesId);

        Task<ServiceResult<so_SalesHeaders>> Update_sales(Guid salesId, so_SalesHeaders so_SalesHeaders);

        Task<ServiceResult<SalesReportResponseDTO>> Get_sales_report_by_date(Guid store_id, DateOnly? start_date, DateOnly? end_date);
        Task<ServiceResult<Dictionary<string, List<SalesLineDTO>>>> Get_sales_detailed_by_date(Guid store_id, DateOnly? start_date, DateOnly? end_date);
        Task<ServiceResult<List<DailySalesSummaryDto>>> Get_sales_detailed_by_day_report(Guid store_id, DateOnly? start_date, DateOnly? end_date);
        Task<ServiceResult<Dictionary<string, List<CustomerSalesDetailDto>>>> Get_sales_detailed_by_customer(Guid store_id, string? customer, DateOnly? start_date, DateOnly? end_date);

        Task<ServiceResult<List<ProductSalesDetailDto>>> Get_sales_detailed_by_product(Guid store_id,string? ProductSku, DateOnly? start_date, DateOnly? end_date);
        Task<ServiceResult<List<TaxClassReportDTO>>> Get_sales_tax_report(Guid store_id, DateOnly? start_date, DateOnly? end_date);
        Task<ServiceResult<List<OutstandingSalesDTO>>> Get_sales_out_standing(Guid store_id, DateOnly? start_date, DateOnly? end_date);
        Task<ServiceResult<List<HourlySalesReportDTO>>> Get_sales_hourly_base(Guid store_id, DateOnly? start_date, DateOnly? end_date);

    }
}
