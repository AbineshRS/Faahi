using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Admin
{
    public class sa_country_regions
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? country_region_id { get; set; }

        [ForeignKey("avl_countries_id")]
        [Display(Name = "avl_countries")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? avl_countries_id { get; set; }=null;

        [Column(TypeName = "nvarchar(250)")]
        public string? region_name { get; set; }=null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; }=null;

        public ICollection<sa_regions>? sa_regions { get; set; } = null;

    }
}
