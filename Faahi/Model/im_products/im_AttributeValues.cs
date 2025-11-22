using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_AttributeValues
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid value_id { get; set; }

        [ForeignKey("attribute_id")]
        [Display(Name = "im_ProductAttributes")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? attribute_id { get; set; }

        [Column(TypeName ="nvarchar(255)")]
        public string? value { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? color_name { get; set; } = null;

        [Column(TypeName = "int")]
        public Int16? display_order { get; set; }

    }
}
