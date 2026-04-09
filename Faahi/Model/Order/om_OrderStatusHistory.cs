using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Order
{
    public class om_OrderStatusHistory
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid order_status_history_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? customer_order_id { get; set; } = null;
        [ForeignKey(nameof(customer_order_id))]
        [JsonIgnore]
        public om_CustomerOrders? om_CustomerOrders { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? old_status { get; set; }=null;

        [Column(TypeName = "nvarchar(50)")]
        public string? new_status { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? status_type { get; set; }
        //ORDER / FULFILLMENT / DELIVERY / PAYMENT

        [Column(TypeName = "uniqueidentifier")]
        public Guid? changed_by { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? changed_at { get; set; }
    }
}
