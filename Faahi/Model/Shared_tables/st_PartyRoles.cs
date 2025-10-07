using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Shared_tables
{
    public class st_PartyRoles
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_role_id { get; set; }

        [ForeignKey("party_id")]
        [Display(Name = "st_PartyRoles")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_id { get; set; }

        [Column(TypeName ="varchar(30)")]
        public string? role { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        public ICollection<st_PartyAddresses> st_PartyAddresses { get; set; } = null;
    }
}
