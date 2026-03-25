using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(nameof(BusinessId), nameof(PostingDate), nameof(GlAccountId), Name = "IX_gl_Ledger_BusinessDateAccount")]
    [Index(nameof(JournalId), nameof(JournalLineId), Name = "IX_gl_Ledger_JournalRefs")]
    [Index(nameof(SourceType), nameof(SourceId), Name = "IX_gl_Ledger_Source")]
    [Index(nameof(BusinessId), nameof(TransactionDate), Name = "IX_gl_Ledger_BusinessTransactionDate")]
    public class gl_Ledger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid LedgerId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid JournalId { get; set; }

        [ForeignKey(nameof(JournalId))]
        [JsonIgnore]
        public gl_JournalHeaders? JournalHeader { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid JournalLineId { get; set; }

        [ForeignKey(nameof(JournalLineId))]
        [JsonIgnore]
        public gl_JournalLines? JournalLine { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid GlAccountId { get; set; }

        [ForeignKey(nameof(GlAccountId))]
        [JsonIgnore]
        public gl_Accounts? GlAccount { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime PostingDate { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? BaseCurrencyCode { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? TransactionCurrencyCode { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal? ExchangeRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal DebitAmountFC { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal CreditAmountFC { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal DebitAmountBC { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal CreditAmountBC { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ReferenceNo { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? SourceType { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? SourceId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? SourceLineId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Description { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(50)")]
        public string? CreatedBy { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? CurrencyCode { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string? Module { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}