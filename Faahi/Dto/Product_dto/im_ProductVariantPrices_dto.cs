using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Product_dto
{
    public class im_ProductVariantPrices_dto
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public string? variant_price_id { get; set; }

        [ForeignKey("im_product_subvariant")]
        [Display(Name = "sub_variant_id")]
        [Column(TypeName = "varchar(30)")]
        public string? sub_variant_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "varchar(20)")]
        public string? company_id { get; set; }

        [ForeignKey("price_tier_id")]
        [Display(Name = "im_PriceTiers")]
        [Column(TypeName = "varchar(20)")]
        public string? price_tier_id { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? price { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        [DefaultValue("USD")]
        public string? currency { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        public ICollection<im_ProductImages_dto> im_ProductImages_dto { get; set; }
    }
}
