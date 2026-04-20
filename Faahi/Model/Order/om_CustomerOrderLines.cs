using Faahi.Model.im_products;
using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Order
{
    public class om_CustomerOrderLines
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid customer_order_line_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid customer_order_id { get; set; }
        [ForeignKey(nameof(customer_order_id))]
        [JsonIgnore]
        public om_CustomerOrders? om_CustomerOrders { get; set; }

        [Column(TypeName = "int")]
        public int? line_no { get; set; }

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

        //[Column(TypeName = "uniqueidentifier")]
        //public Guid? uom_id { get; set; } = null;
        //[ForeignKey(nameof(uom_id))]
        //[JsonIgnore]
        //public im_UnitsOfMeasure? im_UnitsOfMeasure { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal ordered_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0)]
        public Decimal reserved_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? picked_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal packed_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal dispatched_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal delivered_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal returned_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal cancelled_qty { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal unit_price { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_amount { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_amount { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal line_total { get; set; } = 0m;

        [Column(TypeName = "nvarchar(255)")]
        [DefaultValue(0)]
        public string? remarks { get; set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName ="uniqueidentifier")]
        public Guid? created_by { get; set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? updated_at { get; set; }

        [Column(TypeName ="uniqueidentifier")]
        public Guid? updated_by { get; set; }=null;

        [Column(TypeName = "nvarchar(30)")]
        [DefaultValue("OPEN")]
        public string? line_status { get; set; }

    }
}
