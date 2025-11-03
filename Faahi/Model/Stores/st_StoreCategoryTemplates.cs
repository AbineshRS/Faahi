using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    [Index(nameof(store_type))]
    [Index(nameof(category_id))]
    public class st_StoreCategoryTemplates
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_category_template_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? store_type { get; set; }

        [ForeignKey("category_id")]
        [Display(Name = "im_ProductCategories")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? category_id { get; set; }
    }
}
