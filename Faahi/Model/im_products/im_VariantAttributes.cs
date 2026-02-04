using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(varient_attribute_id),Name = "varient_attribute_id")]
    [Index(nameof(value_id),Name = "value_id")]
    [Index(nameof(attribute_id),Name = "attribute_id")]
    [Index(nameof(variant_id),Name = "variant_id")]
    public class im_VariantAttributes
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? varient_attribute_id {  get; set; }

        [ForeignKey("value_id")]
        [Display(Name = "im_AttributeValues")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? value_id { get; set; } = null;

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; } = null;

        [ForeignKey("attribute_id")]
        [Display(Name = "im_ProductAttributes")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? attribute_id { get; set; } = null;


    }
}
