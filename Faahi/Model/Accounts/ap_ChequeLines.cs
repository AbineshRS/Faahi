using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(nameof(ChequeId), Name = "IX_ChequeLines_Cheque")]
    public class ap_ChequeLines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid ChequeLineId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid ChequeId { get; set; }

        [ForeignKey(nameof(ChequeId))]
        [JsonIgnore]
        public ap_Cheques? Cheque { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid AccountId { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseAmount { get; set; }

        [Column(TypeName = "int")]
        public int? SortOrder { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}