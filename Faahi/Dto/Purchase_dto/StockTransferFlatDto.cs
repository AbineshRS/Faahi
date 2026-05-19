namespace Faahi.Dto.Purchase_dto
{
    public class StockTransferFlatDto
    {
        public Guid transfer_id { get; set; }
        public string? transfer_code { get; set; }
        public Guid? business_id { get; set; }
        public Guid? from_store_id { get; set; }
        public Guid? to_store_id { get; set; }
        public decimal? total_quantity { get; set; }
        public decimal? total_amount { get; set; }
        public DateTime? created_at { get; set; }
        public Guid? created_by_user_id { get; set; }
        public string? created_by { get; set; }
        public DateTime? approved_at { get; set; }
        public Guid? approved_by { get; set; }
        public string? status { get; set; }

        public string? from_store_name { get; set; }
        public string? to_store_name { get; set; }

        // LINE
        public Guid? transfer_line_id { get; set; }
        public Guid? line_transfer_id { get; set; }
        public Guid? product_id { get; set; }
        public Guid? variant_id { get; set; }
        public Guid? store_variant_inventory_id { get; set; }
        public Guid? item_batch_id { get; set; }
        public string? batch_number { get; set; }
        public DateOnly? expiry_date { get; set; }
        public decimal? average_cost { get; set; }
        public decimal? quantity { get; set; }
        public decimal? unit_price { get; set; }
        public decimal? line_total { get; set; }
        public DateTime? line_created_at { get; set; }

        // PRODUCT
        public string? title { get; set; }
    }
}
