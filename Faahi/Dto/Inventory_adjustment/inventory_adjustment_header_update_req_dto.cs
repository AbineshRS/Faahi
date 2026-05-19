namespace Faahi.Dto.Inventory_adjustment
{
    public class inventory_adjustment_header_update_req_dto
    {
        public Guid? adjustment_id { get; set; }
        public string? adjustment_type { get; set; }


        public ICollection<inventory_adjustment_lines_update_dto>? inventory_adjustment_lines { get; set; } = null;
    }
}
