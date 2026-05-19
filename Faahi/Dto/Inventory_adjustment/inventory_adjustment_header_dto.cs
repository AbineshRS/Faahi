namespace Faahi.Dto.Inventory_adjustment
{
    public class inventory_adjustment_header_dto
    {
        public string adjustment_code { get; set; }

        public Guid store_id { get; set; }

        public string adjustment_type { get; set; }

        public string created_by { get; set; }

        public decimal total_negative_value { get; set; }

        public decimal total_positive_value { get; set; }

        public decimal total_adjustment_value { get; set; }

        public List<inventory_adjustment_lines_dto> inventory_adjustment_lines { get; set; }
    }
}
