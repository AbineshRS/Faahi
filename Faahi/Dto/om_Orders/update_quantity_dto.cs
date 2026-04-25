using System.ComponentModel.DataAnnotations;

namespace Faahi.Dto.om_Orders
{
    public class update_quantity_dto
    {
        [Required] public Guid business_id { get; set; }
        [Required] public Guid store_id { get; set; }
        [Required] public Guid customer_order_id { get; set; }
        [Required] public Guid customer_order_line_id { get; set; }
        // Optional if you want to target one fulfillment only
        public Guid? fulfillment_id { get; set; }
        [Range(0, double.MaxValue)]
        public decimal new_ordered_qty { get; set; }
        public Guid? updated_by { get; set; }
        public string? remarks { get; set; }
        public bool confirm_delete { get; set; } = false;
    }

    public class update_quantity_result_dto
    {
        public Guid customer_order_line_id { get; set; }
        public decimal old_qty { get; set; }
        public decimal new_qty { get; set; }
        public decimal released_qty { get; set; }
    }
}
