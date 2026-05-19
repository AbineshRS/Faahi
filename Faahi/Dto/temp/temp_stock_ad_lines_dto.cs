namespace Faahi.Dto.temp
{
    public class temp_stock_ad_lines_dto
    {


        public Guid? tem_ad_line_id { get; set; }

        public Guid? store_id { get; set; }


        public Guid? product_id { get; set; }


        public Guid? variant_id { get; set; } = null;


        public Guid? store_variant_inventory_id { get; set; } = null;


        public string? barcode { get; set; } = null;

        public string? sku { get; set; } = null;

        public string? title { get; set; } = null;

        public string? batch_number { get; set; } = null;

        public DateTime? expiry_date { get; set; } = null;

        public Decimal? counted_qty { get; set; }

        public Decimal? adjusted_qty { get; set; }

        public Decimal? average_cost { get; set; }

        public Decimal? total_cost { get; set; }

        public Guid? adjustment_detail_id { get; set; }


        public Guid? adjustment_id { get; set; }
    }
}
