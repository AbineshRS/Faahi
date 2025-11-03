using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(category_name),IsUnique =false)]
    [Index(nameof(parent_id))]
    [Index(nameof(is_active))]
    [Index(nameof(Level))]
    public class im_ProductCategories
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? category_id { get; set; }

        //[ForeignKey("company_id")]
        //[Column(TypeName = "uniqueidentifier")]
        //public Guid? company_id { get; set; }

        [Column(TypeName ="nvarchar(30)")]
        public string? category_name { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? parent_id { get; set; } = null;

        [Column(TypeName ="varchar(200)")]
        public string? image_url { get; set; }=null ;

        [Column(TypeName ="varchar(30)")]
        public string? edit_user_id { get; set; } =null ;

        [Column(TypeName = "datetime")]
        public DateTime? edit_date_time { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? is_active { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? Level { get; set; } = null;


    }
}
