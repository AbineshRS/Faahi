using Faahi.Model.im_products;
using Faahi.Model.st_sellers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.temp_tables
{
    public class temp_stock_ad_lines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid tem_ad_line_id { get; set; }

        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores st_stores { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }

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
        public Guid? variant_id { get; set; } = null;

        [ForeignKey(nameof(store_variant_inventory_id))]
        [JsonIgnore]
        [ValidateNever]
        public im_StoreVariantInventory im_StoreVariantInventory { get; set; } = null;
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_variant_inventory_id { get; set; } = null;


        [Column(TypeName = "nvarchar(100)")]
        public string? barcode { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? sku { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? title { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? batch_number { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? expiry_date { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal counted_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal adjusted_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal average_cost { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_cost { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? adjustment_detail_id { get; set; }


        [Column(TypeName = "uniqueidentifier")]
        public Guid? adjustment_id { get; set; }
    }
}
