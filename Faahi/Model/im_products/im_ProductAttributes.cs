using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(company_id))]
    [Index(nameof(name))]
    public class im_ProductAttributes
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid attribute_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? name { get; set; }

        [Column(TypeName ="int")]
        public Int16? display_order { get; set; }

        public ICollection<im_AttributeValues>? im_AttributeValues { get; set; } = null;
    }
}
