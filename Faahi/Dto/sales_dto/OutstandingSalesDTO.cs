namespace Faahi.Dto.sales_dto
{
    public class OutstandingSalesDTO
    {
        public Guid? sales_id { get; set; }
        public long? sales_no { get; set; }
        public string? invoice_no { get; set; }
        public DateOnly? sales_date { get; set; }
        public DateOnly? due_date { get; set; }
        public string? customer_name { get; set; }
        public string? payment_method { get; set; }
        public decimal? grand_total_base { get; set; }
        public decimal? amount_paid_base { get; set; }
        public decimal? balance_base { get; set; }
        public int? overdue_days { get; set; }
        public string? aging_bucket { get; set; }
    }
}
