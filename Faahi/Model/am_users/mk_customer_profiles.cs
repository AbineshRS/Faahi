using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.am_users
{
    [Index(nameof(customer_profile_id),IsUnique =true,Name = "IX_customer_profile_id")]
    public class mk_customer_profiles
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid customer_profile_id { get; set; }

        [ForeignKey(nameof(user_id))]
        public am_users am_Users { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid user_id { get; set; }

        [Column(TypeName = "varchar(32)")]
        public string? customer_code { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? gender { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? date_of_birth { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? preferred_language { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string? notes { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string status { get; set; } = string.Empty;

        public ICollection<mk_customer_addresses>? mk_customer_addresses { get; set; } = null;
    }
}
