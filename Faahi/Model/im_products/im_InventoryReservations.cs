using Faahi.Model.Order;
using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    public class im_InventoryReservations
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid reservation_id { get; set; }

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

        [Column(TypeName = "uniqueidentifier")]
        public Guid customer_order_line_id { get; set; } 
        [ForeignKey(nameof(customer_order_line_id))]
        [JsonIgnore]
        public om_CustomerOrderLines? om_CustomerOrderLines { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid product_id { get; set; }

        [ForeignKey(nameof(product_id))]
        [JsonIgnore]
        public im_Products? im_Products { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid variant_id { get; set; }

        [ForeignKey(nameof(variant_id))]
        [JsonIgnore]
        public im_ProductVariants? im_ProductVariants { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid? batch_id { get; set; } = null;
        [ForeignKey(nameof(batch_id))]
        [JsonIgnore]
        public im_ItemBatches? im_ItemBatches { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal reserved_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal released_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal consumed_qty { get; set; }

       

        [Column(TypeName ="datetime")]
        public DateTime? reserved_at { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? released_at { get; set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? consumed_at { get; set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? expires_at { get; set; }=null;

        [Column(TypeName = "nvarchar(255)")]
        public string? remarks { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? created_by { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_user_id { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get;set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? updated_at { get;set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? updated_by { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        [DefaultValue("ACTIVE")]
        public string? reservation_status { get; set; }
        //ACTIVE','PARTIAL','CONSUMED','RELEASED','CANCELLED

    }
}
