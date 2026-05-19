using System.ComponentModel.DataAnnotations;

namespace Faahi.Dto.om_Orders
{
    public class update_sales_mode_dto
    {
        [Required] public Guid business_id { get; set; }
        [Required] public Guid store_id { get; set; }
        [Required] public Guid customer_order_id { get; set; }
        public string? sales_mode { get; set; }
        public Guid? updated_by { get; set; }
    }

    public class update_sales_mode_result_dto
    {
        public Guid customer_order_id { get; set; }
        public Guid sales_id { get; set; }
        public string? sales_mode { get; set; }
    }
}
