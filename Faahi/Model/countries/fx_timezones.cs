using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.countries
{
    public class fx_timezones
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid timezone_id { get; set; }


        [ForeignKey("currency_id")]
        [Display(Name = "fx_Currencies")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? currency_id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string timezone { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string timezone_name { get; set; }
    }
}
