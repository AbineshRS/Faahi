using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_purchase_listing_details
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? detail_id { get; set; }

        [ForeignKey("listing_id")]
        [Display(Name = "im_purchase_listing")]
        public Guid? listing_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        public Guid? product_id { get; set; }

        [ForeignKey("sub_variant_id")]
        [Display(Name = "im_product_subvariant")]
        public Guid? sub_variant_id { get; set; }

        [ForeignKey("uom_id")]
        [Display(Name = "im_UnitsOfMeasure")]
        public Guid? uom_id { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? quantity { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? unit_price { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? discount_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? tax_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? freight_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? other_expenses { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? line_total { get; set; } = null;

        [Column(TypeName = "varchar(400)")]
        public string? notes { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? expiry_date { get; set; } = null;

    }
}
