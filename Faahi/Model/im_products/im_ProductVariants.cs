using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(variant_id),Name = "variant_id")]
    [Index(nameof(product_id),Name = "product_id")]
    [Index(nameof(description_2),Name = "description_2")]
    public class im_ProductVariants
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? product_id { get; set; }

        
        [Column(TypeName = "varchar(20)")]
        public string? uom_name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? sku { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? barcode { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? barcovendor_part_number { get; set; } = null;

        //[Column(TypeName = "varchar(50)")]
        //public string? color { get; set; } = null;

        //[Column(TypeName = "varchar(50)")]
        //public string? size { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? base_price { get; set; } = null;

        //[Column(TypeName = "decimal(18, 4)")]
        //public Decimal? stock_quantity { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? weight_kg { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? length_cm { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? width_cm { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? height_cm { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? chargeable_weight_kg { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? average_cost { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? last_price { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        //[StringLength(1)]
        //[DefaultValue("F")]
        //[Column(TypeName = "char(1)")]
        //public string? low_stock_alert { get; set; } = string.Empty;

        //[StringLength(1)]
        //[DefaultValue("F")]
        //[Column(TypeName = "char(1)")]
        //public string? allow_below_Zero { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? is_default { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(255)")]
        public string? description_2 { get; set; } = null;

        public ICollection<im_VariantAttributes>? im_VariantAttributes { get; set; } = null;
        public ICollection<im_StoreVariantInventory>? im_StoreVariantInventory { get; set; } = null;
        public ICollection<im_ProductImages>? im_ProductImages { get; set; } = null;

    }
}
