using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Stores
{
    [Index(nameof(store_currency_id))]
    [Index(nameof(store_id))]
    [Index(nameof(currency_code))]
    public class st_store_currencies
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_currency_id { get; set; }

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [Column(TypeName = "char(3)")]
        [StringLength(3)]
        public string? currency_code { get; set; } = string.Empty;

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string? is_default { get; set; } = null;
    }
}
