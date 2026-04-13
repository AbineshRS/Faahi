using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.am_users
{
    public class mk_business_zones
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid zone_id { get; set; }

        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        [ValidateNever]
        public Faahi.Model.co_business.co_business c_business { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; } = null;

        [Column(TypeName ="nvarchar(100)")]
        public string? zone_name { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string? description { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? updated_at { get; set; }

        [Column(TypeName ="char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string is_active { get; set; }
    }
}
