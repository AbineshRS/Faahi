using Faahi.Model.im_products;
using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Order
{
    public class om_FulfillmentLines
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid fulfillment_line_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid fulfillment_id { get; set; }
        [ForeignKey(nameof(fulfillment_id))]
        [JsonIgnore]
        public om_FulfillmentOrders? om_FulfillmentOrders { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? customer_order_line_id { get; set; }=null;


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
        public Guid? store_variant_inventory_id { get; set; }

        [ForeignKey(nameof(store_variant_inventory_id))]
        [JsonIgnore]
        public im_StoreVariantInventory? im_StoreVariantInventory { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? batch_id { get; set; } = null;
        [ForeignKey(nameof(batch_id))]
        [JsonIgnore]
        public im_ItemBatches? im_ItemBatches { get; set; } = null;

        [Column(TypeName ="int")]
        public int? line_no { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal ordered_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal reserved_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal picked_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal packed_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal delivered_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal returned_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal rejected_qty { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? remarks { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        public string line_status { get; set; }
        //PENDING','PICKING','PICKED','PACKED','SHORT','CANCELLED
    }
}
