using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_VariantAttributes
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid varient_attribute_id {  get; set; }

        [ForeignKey("value_id")]
        [Display(Name = "im_AttributeValues")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? value_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }


    }
}
