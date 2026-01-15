using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Admin
{
    public class sa_regions
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? region_id { get; set; }

        [ForeignKey("country_region_id")]
        [Display(Name = "sa_country_regions")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? country_region_id { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? parent_id { get; set; } = null;


        [Column(TypeName = "nvarchar(250)")]
        public string? region_name { get; set; } = null;


        [Column(TypeName = "nvarchar(250)")]
        public string? city { get; set; } = null;
    }
}
