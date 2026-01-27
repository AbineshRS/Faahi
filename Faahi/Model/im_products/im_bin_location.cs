using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_bin_location
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? bin_location_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; } = null;

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        public string? bin_code { get; set; } = null;
    }
}
