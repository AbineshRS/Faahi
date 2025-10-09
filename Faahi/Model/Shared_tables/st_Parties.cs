using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Shared_tables
{
    public class st_Parties
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid party_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? vsco_id { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string party_type { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? display_name { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? legal_name { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? payable_name { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? tax_id { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? email { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? phone { get; set; } = null;

        [DefaultValue("'USD'")]
        [Column(TypeName = "varchar(20)")]
        public string? default_currency { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }=null;

        [Column(TypeName ="datetime")]
        public DateTime? updated_at { get;set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName ="char(1)")]
        public string? status { get; set; }=string.Empty;

        public ICollection<st_PartyRoles> st_PartyRoles { get; set; } = null;
    }
}
