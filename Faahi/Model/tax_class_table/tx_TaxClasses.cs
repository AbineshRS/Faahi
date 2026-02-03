using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.tax_class_table
{
    [Index(nameof(tax_class_id),Name = "IX_tax_class_id", IsUnique = true)]
    [Index(nameof(tax_class_name),Name = "IX_tax_class_name")]
    public class tx_TaxClasses
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? tax_class_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; } = null;

        [Column(TypeName ="nvarchar(50)")]
        public string? tax_class_name { get; set; }

        [Column(TypeName ="decimal(9,4)")]
        public Decimal? rate_percent { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }


    }
}
