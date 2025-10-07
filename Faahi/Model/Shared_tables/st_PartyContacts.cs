using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Shared_tables
{
    public class st_PartyContacts
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? contact_id { get; set; }

        [ForeignKey("party_id")]
        [Display(Name = "st_PartyRoles")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string first_name { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? last_name { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? email { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? phone { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? title { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? is_primary { get; set; } = string.Empty;

    }
}
