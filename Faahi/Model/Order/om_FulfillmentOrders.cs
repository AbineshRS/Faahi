using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Order
{
    public class om_FulfillmentOrders
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid fulfillment_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }
        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }
        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores? st_Stores { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid customer_order_id { get; set; }
        [ForeignKey(nameof(customer_order_id))]
        [JsonIgnore]
        public om_CustomerOrders? om_CustomerOrders { get; set; }

        [Column(TypeName = "bigint")]
        public int fulfillment_no { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? picked_by { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? packed_by { get; set; } = null;


        [Column(TypeName = "uniqueidentifier")]
        public Guid? updated_by { get; set; }=null;

        [Column(TypeName = "datetime")]
        public DateTime? pick_started_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? pick_completed_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? packed_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? ready_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? cancel_at { get; set; } = null;

        [Column(TypeName = "nvarchar(255)")]
        public string? remarks { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime created_at {  get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? out_for_delivery_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? failed_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? returned_at { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? collected_amount { get; set; }=null;

        [Column(TypeName ="nvarchar(max)")]
        public string? proof_of_delivery_image_url { get; set; }=null;

        [Column(TypeName ="nvarchar(200)")]
        public string? failure_reason { get; set; }=null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_ordered_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_reserved_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_delivered_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_returned_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_rejected_qty { get; set; } = 0m;

        //[Column(TypeName = "decimal(18,4)")]
        //[DefaultValue(0)]
        //public Decimal grand_total { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string fulfillment_status { get; set; }
        //PENDING','PICKING','PICKED','PACKED','READY','CANCELLED

        [Column(TypeName = "nvarchar(50)")]
        public string? created_by { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_user_id { get; set; }

        public ICollection<om_FulfillmentLines>? om_FulfillmentLines { get; set; }= null;
    }
}
