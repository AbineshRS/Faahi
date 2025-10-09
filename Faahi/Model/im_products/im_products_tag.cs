using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_products_tag
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? tag_id { get; set; }

        [ForeignKey("item_class_id")]
        [Display(Name = "im_item_Category")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? item_class_id { get; set; }

        [ForeignKey("item_subclass_id")]
        [Display(Name = "im_item_subcategory")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? item_subclass_id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? description { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; } = null;
    }
}
