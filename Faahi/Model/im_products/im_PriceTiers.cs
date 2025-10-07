using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_PriceTiers
    {
        [Key]
        [Column(TypeName ="uniqueidentifier")]
        public Guid? price_tier_id { get; set; }

        [ForeignKey("im_ProductVariants")]
        [Display(Name = "variant_id")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [ForeignKey("im_product_subvariant")]
        [Display(Name = "sub_variant_id")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? sub_variant_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? name { get; set; } = null;

        [Column(TypeName ="varchar(max)")]
        public string? description { get; set; }=null;

        public ICollection<im_ProductVariantPrices> im_ProductVariantPrices { get; set; } = null;
    }
}
