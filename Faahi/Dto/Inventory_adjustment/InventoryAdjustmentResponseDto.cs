namespace Faahi.Dto.Inventory_adjustment
{
    public class InventoryAdjustmentResponseDto
    {
        public List<inventory_adjustment_lines_update_dto> Details { get; set; }

        public List<inventory_adjustment_lines_update_dto> UniqueSkus { get; set; }
    }
}
