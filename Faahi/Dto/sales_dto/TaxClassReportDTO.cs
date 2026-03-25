namespace Faahi.Dto.sales_dto
{
    public class TaxClassReportDTO
    {
        public DateOnly? sales_date { get; set; }
        public string? tax_class { get; set; }
        public decimal? taxable_amount_base { get; set; }
        public decimal? tax_amount_base { get; set; }
        public int? invoice_count { get; set; }
    }
}
