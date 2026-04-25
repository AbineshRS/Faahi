namespace Faahi.Dto.sales_dto
{
    public class so_sales_header_customer
    {
        public Guid? sales_id { get; set; }

        public long? sales_no { get; set; } = null;

        public string? payment_mode { get; set; } = null;

        public string? invoice_no { get; set; } = null;

        public string? doc_type { get; set; }

        public string? reference_no { get; set; }

        public string? doc_currency_code { get; set; } = null;

        public DateTime? created_at { get; set; } = null;

        public DateTime? sales_date { get; set; } = null;

        public decimal? balance_base { get; set; } = null;

        public decimal? grand_total { get; set; } = null;

        public string? status { get; set; } = null;
    
    }
}
