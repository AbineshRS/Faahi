using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.im_products
{
    [Table("im_random_Stock_reject")]
    public class im_random_Stock_reject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid stock_reject_id { get; set; }

        
        [Column(TypeName = "uniqueidentifier")]
        public Guid? adjustment_detail_id { get; set; }=null;

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

        [Column(TypeName = "char(1)")]
        [DefaultValue("F")]
        [StringLength(1)]
        public string? track_expiry { get; set; } = null;

        [Column(TypeName = "int")]
        public int line_no { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal rejected_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal system_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal counted_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal adjusted_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal average_cost { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_cost { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; }

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string is_posted { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_by { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string status { get; set; }
        //PENDING,APPROVED,POSTED,REJECTED

    }
}
