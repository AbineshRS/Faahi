using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    [Index(nameof(store_id))]
    [Index(nameof(category_id))]
    [Index(nameof(is_selected))]
    public class st_StoreCategories
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_category_id { get; set; }

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }


        [ForeignKey("category_id")]
        [Display(Name = "im_ProductCategories")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? category_id { get; set; }

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? is_selected { get; set; } = null;
    }
}
