using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_UnitsOfMeasure
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? uom_id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? name { get; set; } = null;

        [Column(TypeName = "varchar(10)")]
        public string? abbreviation { get; set; } = null;
    }
}
