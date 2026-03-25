namespace Faahi.Dto.sales_dto
{
    public class SalesReportDTO
    {
        public DateOnly? sales_date { get; set; }
        public decimal? discount_total_base { get; set; }
        public decimal? service_charge_base { get; set; }
        public decimal? tax_total_base { get; set; }
        public decimal? grand_total_base { get; set; }

        public string? contact_name { get; set; }
        public Int64? sales_no { get; set; }
        public string? payment_mode { get; set; }
        public string? doc_currency_code { get; set; }

        public decimal? Cashsales { get; set; }
        public decimal? Creditsales { get; set; }
    }
}
