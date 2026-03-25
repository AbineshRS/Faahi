namespace Faahi.Dto.sales_dto
{
    public class HourlySalesReportDTO
    {
        public DateOnly? sales_date { get; set; }
        public int? sales_hour { get; set; }
        public string? payment_type { get; set; }
        public int? sales_count { get; set; }
        public decimal? total_sales_base { get; set; }
        public decimal? total_discount_base { get; set; }
        public decimal? total_tax_base { get; set; }
        public decimal? average_bill_base { get; set; }
    }
}
