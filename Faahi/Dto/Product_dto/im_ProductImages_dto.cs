using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Product_dto
{
    public class im_ProductImages_dto
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public Guid? image_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        [Column(TypeName = "varchar(20)")]
        public Guid? product_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "varchar(20)")]
        public Guid? variant_id { get; set; }

        [Column(TypeName = "varchar(200)")]
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
