using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.site_settings
{
    public class mk_blacklisted_numbers
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid blacklist_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }
        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_business { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string phone_number { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? reason { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; }

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string is_active { get; set; }
    }
}
