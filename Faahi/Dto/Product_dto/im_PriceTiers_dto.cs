using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Product_dto
{
    public class im_PriceTiers_dto
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public Guid? price_tier_id { get; set; }

        [ForeignKey("im_ProductVariants")]
        [Display(Name = "variant_id")]
        [Column(TypeName = "varchar(30)")]
        public string? variant_id { get; set; }

        [ForeignKey("im_product_subvariant")]
        [Display(Name = "sub_variant_id")]
        [Column(TypeName = "varchar(30)")]
        public string? sub_variant_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? name { get; set; } = null;

        [Column(TypeName = "varchar(max)")]
        public string? description { get; set; } = null;

        public ICollection<im_ProductVariantPrices_dto> im_ProductVariantPrices_dto { get; set; } = null;
    }
}
