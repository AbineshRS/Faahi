using Faahi.Model.im_products;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Product_dto
{
    public class im_ProductVariants_dto
    {
        [Key]
        [Column(TypeName = "varchar(30)")]
        public string? variant_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        [Column(TypeName = "varchar(20)")]
        public string? product_id { get; set; }

        [ForeignKey("uom_id")]
        [Column(TypeName = "varchar(20)")]
        public string? uom_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? sku { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? barcode { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? color { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? size { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? price { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? stock_quantity { get; set; } = null;

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

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? low_stock_alert { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? allow_below_Zero { get; set; } = string.Empty;

        public ICollection<im_Product_Subvariants_dto>? im_Product_Subvariants_dto { get; set; } = null;

    }
}
