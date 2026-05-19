namespace Faahi.Dto.product_transfer
{
    public class product_transfer_dto
    {
        public Guid? product_id { get; set; }
        public Guid? variant_id { get; set; }
        public Guid? store_variant_inventory_id { get; set; }
        public string? description { get; set; }
        public string? thumbnail_url { get; set; }
        public string? title { get; set; }
        //public string? attribute_name { get; set; }
        //public string? attribute_value { get; set; }
        public string? track_expiry { get; set; }
        public string? sku { get; set; }
        public string? barcode { get; set; }
        public string? uom_name { get; set; }
        public decimal? base_price { get; set; }
        public decimal? average_cost { get; set; }
        public decimal? committed_quantity { get; set; }
        public decimal? on_hand_quantity { get; set; }
        public Guid? item_batch_id { get; set; }
        public int? batch_id { get; set; }
        public string? batch_number { get; set; }
        public DateOnly? expiry_date { get; set; }

    }
}
