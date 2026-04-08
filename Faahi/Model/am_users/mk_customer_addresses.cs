using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.am_users
{
    [Index(nameof(address_id),IsUnique =true,Name = "IX_address_id")]
    public class mk_customer_addresses
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid address_id { get; set; }

        [ForeignKey(nameof(user_id))]
        [JsonIgnore]
        [ValidateNever]

        public am_users am_Users { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid user_id { get; set; }

        [ForeignKey(nameof(customer_profile_id))]
        [JsonIgnore]
        [ValidateNever]
        public mk_customer_profiles mk_customer_profiles { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid customer_profile_id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? address_type { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? contact_name { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string? contact_phone { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? address_line1 { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? address_line2 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? city { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? state_region { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        public string? postal_code { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        public string? country_code { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; }

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string is_default { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string status { get; set; } = string.Empty;
    }
}
