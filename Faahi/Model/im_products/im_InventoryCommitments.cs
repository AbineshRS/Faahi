using Faahi.Model.st_sellers;
using Faahi.Service.im_products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    public class im_InventoryCommitments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid commitment_id { get; set; }

        [ForeignKey(nameof(business_id))]
        public co_business.co_business co_business { get; set; }
        [JsonIgnore]
        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(store_id))]
        public st_stores st_stores { get; set; }
        [JsonIgnore]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; } = null;

        [ForeignKey(nameof(product_id))]
        public im_Products im_Product { get; set; }
        [JsonIgnore]
        [Column(TypeName = "uniqueidentifier")]
        public Guid product_id { get; set; }

        [ForeignKey(nameof(variant_id))]
        public im_ProductVariants im_ProductVariants { get; set; }
        [JsonIgnore]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [ForeignKey(nameof(store_variant_inventory_id))]
        public im_StoreVariantInventory im_StoreVariantInventory { get; set; }
        [JsonIgnore]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_variant_inventory_id { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? source_id { get; set; }=null;

        [Column(TypeName ="nvarchar(100)")]
        public string source_type { get; set; }
        //PURCHASE ,INV_TRANSFER, SALES

        [Column(TypeName ="nvarchar(100)")]
        public string source_no { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal committed_quantity { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string action_type { get; set; }
        //INCREASE ,DECREASE,SALES,SALES_RETURN

        [Column(TypeName = "nvarchar(500)")]
        public string remarks { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_by { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }
    }
}
