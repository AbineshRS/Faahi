using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.countries
{
    public class avl_countries
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? avl_countries_id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? name { get; set; } = string.Empty;

        [Column(TypeName = "varchar(16)")]
        public string? country_code { get; set; } = null;

        [Column(TypeName = "varchar(150)")]
        public string? flag { get; set; } = null;

        [Column(TypeName = "varchar(16)")]
        public string? dialling_code { get; set; } = null;

        [Column(TypeName = "varchar(16)")]
        public string? currency_code { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? currency_name { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? serv_available { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string status { get; set; } = string.Empty;
    }
}
