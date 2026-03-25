using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(nameof(JournalId), nameof(LineNo), Name = "IX_gl_JournalLines_Journal")]
    [Index(nameof(BusinessId), nameof(GlAccountId), Name = "IX_gl_JournalLines_Account")]
    [Index(nameof(BusinessId), nameof(StoreId), nameof(GlAccountId), Name = "IX_gl_JournalLines_StoreAccount")]
    public class gl_JournalLines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid JournalLineId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid JournalId { get; set; }

        [ForeignKey(nameof(JournalId))]
        [JsonIgnore]
        public gl_JournalHeaders? Journal { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid GlAccountId { get; set; }

        [ForeignKey(nameof(GlAccountId))]
        [JsonIgnore]
        public gl_Accounts? Account { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? CurrencyCode { get; set; }

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

        [Column(TypeName = "int")]
        public int LineNo { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Description { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string? SourceType { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? SourceLineId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? CustomerId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? SupplierId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? CostCenterId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? DepartmentId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(50)")]
        public string? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? UpdatedBy { get; set; }
    }
}