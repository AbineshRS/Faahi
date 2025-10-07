using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_product_subvariant
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? sub_variant_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? product_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? variantType { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? variantValue { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? list_price { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? standard_cost { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? last_cost { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? avg_cost { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? ws_price { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? profit_p { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? minimum_selling { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? item_barcode { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? modal_number { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? shipping_weight { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? shipping_length { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? shipping_width { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? shipping_height { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? total_volume { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? total_weight { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? deduct_qnty { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? quantity { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? edit_user_id { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? unit_breakdown { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? fixed_price { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? generateBarcode { get; set; } = string.Empty;

        public ICollection<im_PriceTiers>? im_PriceTiers { get; set; } = null;

    }
}
