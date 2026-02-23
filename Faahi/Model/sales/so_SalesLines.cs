using Faahi.Model.im_products;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.sales
{
    [Index(nameof(sales_line_id),IsUnique =true,Name = "IX_sales_line_id")]
    [Index(nameof (sales_id),Name = "IX_sales_id")]
    [Index(nameof (business_id),Name = "IX_business_id")]
    [Index(nameof (store_id),Name = "IX_store_id")]
    [Index(nameof (product_id),Name = "IX_product_id")]
    [Index(nameof (variant_id),Name = "IX_variant_id")]
    [Index(nameof (barcode),Name = "IX_barcode")]
    [Index(nameof (product_sku),Name = "IX_product_sku")]
    public class so_SalesLines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid sales_line_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? sales_id { get; set; }

        [ForeignKey(nameof(sales_id))]
        [JsonIgnore]
        public so_SalesHeaders? so_SalesHeaders { get; set; }

        [Column(TypeName = "uniqueidentifier")]
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

        [Column(TypeName = "nvarchar(100)")]
        public string? barcode { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? product_sku { get; set; } = null;

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string? track_expiry { get;  set; } = string.Empty;

        [Column(TypeName = "nvarchar(255)")]
        public string? item_description { get; set; } = null;

        [Column(TypeName = "decimal(16,2)")]
        public Decimal line_discount_amount { get; set; } = 0;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string? stock_item { get; set; } = string.Empty;

        //-- Consignment (optional)
        [Column(TypeName = "uniqueidentifier")]
        public Guid? consignment_id { get; set; } = null;

        [Column(TypeName ="int")]
        public int? consignment_det_id { get; set; }=null;

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        public string? consignment_billed { get; set; } = null;

        //-- Batch snapshot (optional; you can keep only batch_id if preferred)


        [Column(TypeName = "uniqueidentifier")]
        public Guid? batch_id { get; set; } = null;

        [ForeignKey(nameof(batch_id))]
        [JsonIgnore]
        public im_ItemBatches? im_ItemBatches { get; set; }

        [Column(TypeName ="int")]
        public int? batch_id_int { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? batch_name { get; set; } = null;

        [Column(TypeName = "date")]
        public DateOnly? expiry_date { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? insurance_code { get; set; } = null;

        [Column(TypeName = "char(1)")]
        public string? doctor_consent { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal detected_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal quantity { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal unit_price { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_amount { get; set; } = 0;

        [Column(TypeName = "decimal(6,2)")]
        public Decimal discount_percent { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_amount { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal original_price_base { get; set; } = 0;

        [Column(TypeName = "nvarchar(50)")]
        public string? tax_class {  get; set; }=null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal returned_quantity { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal original_quantity { get; set; } = 0;

        [Column(TypeName = "nvarchar(10)")]
        public string? doc_currency_code { get; set; } = null;

        [Column(TypeName = "nvarchar(10)")]
        public string? base_currency_code { get; set; } = null;

        [Column(TypeName = "decimal(18,6)")]
        public Decimal fx_rate_to_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal unit_price_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_amount_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_amount_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal unit_discount_amount_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal line_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal line_total { get; set; } = 0;

        [Column(TypeName = "nvarchar(255)")]
        public string? remarks { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }



    }
}
