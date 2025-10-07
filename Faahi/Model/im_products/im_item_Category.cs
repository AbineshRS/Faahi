using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_item_Category
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? item_class_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName ="varchar(20)")]
        public string? item_class { get; set; }

        [Column(TypeName ="varchar(200)")]
        public string? description { get; set; }= null;

        [Column(TypeName = "varchar(20)")]
        public string? categoryType { get; set; } = null;

        [Column(TypeName = "int")]
        public Int32? item_count { get; set; } = null;

        [Column(TypeName = "int")]
        public Int32? Sales_count { get; set; } = null;

        [Column(TypeName ="varchar(10)")]
        public string? code { get; set; }=null;

        //[Column(TypeName ="varchar(20)")]
        //public string? item_cat_id { get; set; }=null ;

        [Column(TypeName ="varchar(20)")]
        public string? edit_user_id { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; } = null;

        [Column(TypeName ="varchar(max)")]
        public string? categoryImage {  get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? publish { get; set; } = string.Empty;

        public ICollection<im_item_subcategory>? im_item_subcategory { get; set; } = null;
    }
}
