using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_item_subcategory
    {
        [Key]
        [Column(TypeName ="uniqueidentifier")]
        public Guid? item_subclass_id { get; set; }

        [ForeignKey("item_class_id")]
        [Display(Name= "im_item_Category")]
        [Column(TypeName ="uniqueidentifier")]
        public Guid? item_class_id { get;  set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? description { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? edit_user_id { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;
    }
}
