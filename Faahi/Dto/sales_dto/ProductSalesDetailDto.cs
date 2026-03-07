namespace Faahi.Dto.sales_dto
{
    public class ProductSalesDetailDto
    {
        public string? product_sku { get; set; }
        public DateTime? sales_date { get; set; }
        public string? payment_method { get; set; }
        public string? item_description { get; set; }
        public decimal? quantity { get; set; }
        public string? doc_currency_code { get; set; }
        public decimal? unit_price_base { get; set; }
        public decimal? line_total_base { get; set; }
        public string? CustomerName { get; set; }
        //public int? sales_no { get; set; }
    }
}
