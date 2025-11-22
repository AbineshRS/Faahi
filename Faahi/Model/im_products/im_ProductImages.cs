using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_ProductImages
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? image_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? product_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? image_url { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? is_primary { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public Int32? display_order { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? uploaded_at { get; set; } = null;
    }
}
