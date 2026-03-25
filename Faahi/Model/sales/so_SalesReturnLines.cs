using Faahi.Model.im_products;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.sales
{
    [Index(nameof(sales_return_line_id), Name = "IX_sales_return_line_id",IsUnique =true)]
    [Index(nameof(sales_return_id), Name = "IX_sales_return_id")]
    [Index(nameof(business_id), Name = "IX_business_id")]
    public class so_SalesReturnLines
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid sales_return_line_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid sales_return_id { get; set; }
        [ForeignKey(nameof(sales_return_id))]
        public so_SalesReturnHeaders? so_SalesReturnHeaders { get; set; }

        public Guid? business_id { get; set; } = null;
        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_business { get; set; } = null;


        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }

        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores? st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? product_id { get; set; }

        [ForeignKey(nameof(product_id))]
        [JsonIgnore]
        public im_Products? im_Products { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

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
        public im_ItemBatches? im_ItemBatches { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? barcode { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? product_sku { get; set; } = null;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string? track_expiry { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(255)")]
        public string? item_description { get; set; } = null;

        [Column(TypeName = "decimal(16,2)")]
        public Decimal return_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal unit_price { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_amount { get; set; } = 0;

        [Column(TypeName = "decimal(6,2)")]
        public Decimal discount_percent { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_amount { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal line_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal unit_price_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_amount_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_amount_base { get; set; } = 0;


        [Column(TypeName = "decimal(18,4)")]
        public Decimal line_total_base { get; set; } = 0;

        [Column(TypeName = "nvarchar(50)")]
        public string? tax_class { get; set; } = null;

        [Column(TypeName = "nvarchar(255)")]
        public string? return_reason { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? line_status { get; set; } = null;

    }
}
