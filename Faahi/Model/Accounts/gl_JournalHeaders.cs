using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    [Index(nameof(BusinessId), nameof(JournalDate), nameof(JournalId), Name = "IX_gl_JournalHeaders_BusinessDate")]
    [Index(nameof(BusinessId), nameof(SourceType), nameof(SourceId), Name = "IX_gl_JournalHeaders_Source")]
    [Index(nameof(BusinessId), nameof(PostingDate), nameof(JournalId), Name = "IX_gl_JournalHeaders_PostingDate")]
    [Index(nameof(BusinessId), nameof(JournalNo), IsUnique = true, Name = "UX_gl_JournalHeaders_Business_JournalNo")]
    public class gl_JournalHeaders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid JournalId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime JournalDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime PostingDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(50)")]
        public string? JournalNo { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ReferenceNo { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? SourceType { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? SourceId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? JournalMemo { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Status { get; set; } = "DRAFT";

        [Column(TypeName = "nvarchar(10)")]
        public string? BaseCurrencyCode { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? TransactionCurrencyCode { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal? ExchangeRate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? TotalDebitFC { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? TotalCreditFC { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? TotalDebitBC { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? TotalCreditBC { get; set; }

        [Column(TypeName = "bit")]
        public bool IsSystemGenerated { get; set; } = false;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? ReversalOfJournalId { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? Remarks { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(50)")]
        public string? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? UpdatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? PostedAt { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PostedBy { get; set; }

        public ICollection<gl_JournalLines> JournalLines { get; set; } = new List<gl_JournalLines>();
        public ICollection<gl_JournalAttachments> Attachments { get; set; } = new List<gl_JournalAttachments>();
    }
}