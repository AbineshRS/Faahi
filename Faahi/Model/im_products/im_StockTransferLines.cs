using Faahi.Model.st_sellers;
using Faahi.Service.im_products;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    public class im_StockTransferLines
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public Guid transfer_line_id { get; set; }

        [ForeignKey(nameof(transfer_id))]
        [JsonIgnore]
        [ValidateNever]
        public im_StockTransferHeader im_StockTransferHeader { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid transfer_id { get; set; }


        [ForeignKey(nameof(product_id))]
        [JsonIgnore]
        [ValidateNever]
        public im_Products im_product { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid product_id { get; set; }

        [ForeignKey(nameof(variant_id))]
        [JsonIgnore]
        [ValidateNever]
        public im_ProductVariants im_ProductVariants { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid variant_id { get; set; }

        [ForeignKey(nameof(store_variant_inventory_id))]
        [JsonIgnore]
        [ValidateNever]

        public im_StoreVariantInventory im_StoreVariantInventory { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid store_variant_inventory_id { get; set; }

        [ForeignKey(nameof(item_batch_id))]
        [JsonIgnore]
        [ValidateNever]
        public im_ItemBatches im_ItemBatches { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid? item_batch_id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? batch_number { get; set; }=null;

        [Column(TypeName = "date")]
        public DateOnly? expiry_date { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal average_cost { get; set; } = 0m;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal quantity { get; set; } = 0m;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal unit_price { get; set; } = 0m;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal line_total { get; set; } = 0m;

        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }

    }
}
