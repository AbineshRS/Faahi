using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_ProductVariantPrices
    {
        [Key]
        [Column(TypeName ="uniqueidentifier")]
        public Guid? variant_price_id {  get; set; }

        [ForeignKey("im_product_subvariant")]
        [Display(Name = "sub_variant_id")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? sub_variant_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [ForeignKey("price_tier_id")]
        [Display(Name = "im_PriceTiers")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? price_tier_id { get; set; }

        [Column(TypeName ="decimal(18,4)")]
        public Decimal? price {  get; set; }=null;

        [Column(TypeName ="varchar(20)")]
        [DefaultValue("USD")]
        public string? currency {  get; set; }=string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

    }
}
