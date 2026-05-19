namespace Faahi.Dto.Inventory_adjustment
{
    public class inventory_adjustment_header_response_dto
    {
        public string? adjustment_code { get; set; }

        public Guid? adjustment_id { get; set; }

        public string? adjustment_type { get; set; }

        public Guid? created_by { get; set; }

        public string? fullName { get; set; }

        public decimal? total_negative_value { get; set; }

        public int? total_items { get; set; }

        public decimal? total_positive_value { get; set; }

        public string? status { get; set; }

        public DateTime? created_at { get; set; }
    }
}
