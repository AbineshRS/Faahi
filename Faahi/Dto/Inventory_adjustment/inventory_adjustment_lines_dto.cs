namespace Faahi.Dto.Inventory_adjustment
{
    public class inventory_adjustment_lines_dto
    {
        public Guid product_id { get; set; }

        public Guid? variant_id { get; set; }

        public Guid? store_variant_inventory_id { get; set; }

        public string? barcode { get; set; }
        public string? title { get; set; }
        public string? sku { get; set; }

        public string? batch_number { get; set; }

        public DateTime? expiry_date { get; set; }

        public decimal system_qty { get; set; }

        public decimal counted_qty { get; set; }

        public decimal adjusted_qty { get; set; }

        public decimal average_cost { get; set; }

        public decimal total_cost { get; set; }
    }
}
