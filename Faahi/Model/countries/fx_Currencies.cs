using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.countries
{
    [Index(nameof(currency_id))]
    [Index(nameof(currency_name))]
    [Index(nameof(currency_name))]
    public class fx_Currencies
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid currency_id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string country_name { get; set; }

        [Column(TypeName = "char(3)")]
        public string country_code { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string currency_name { get; set; }

        [Column(TypeName ="nvarchar(8)")]
        public string currency_symbol { get; set; }

        public ICollection<fx_timezones>? fx_timezones { get; set; } = null;
    }
}
