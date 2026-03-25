namespace Faahi.Dto.sales_dto
{
    public class CustomerSalesDetailDto
    {
        public string? contact_name { get; set; }        // Customer name
        public DateOnly? sales_date { get; set; }
        public string? payment_method { get; set; }

        public string? product_sku { get; set; }
        public string? item_description { get; set; }
        public string? doc_currency_code { get; set; }
        public decimal? quantity { get; set; }
        public decimal? unit_price_base { get; set; }
        public decimal? line_total_base { get; set; }
    }
}
