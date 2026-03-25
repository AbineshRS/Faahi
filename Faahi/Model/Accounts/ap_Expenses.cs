using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    [Index(nameof(BusinessId), Name = "IX_Expenses_Business")]
    [Index(nameof(StoreId), Name = "IX_Expenses_Store")]
    [Index(nameof(BusinessId), nameof(ExpenseNo), IsUnique = true, Name = "UX_ap_Expenses_Business_ExpenseNo")]
    public class ap_Expenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid ExpenseId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string ExpenseNo { get; set; } = string.Empty;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? PayeeId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? PayeeName { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentAccountId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PaymentMethod { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ReferenceNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpenseDate { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(3)")]
        public string CurrencyCode { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,6)")]
        public decimal ExchangeRate { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseTotalAmount { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? Memo { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; } = "DRAFT";

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? UpdatedBy { get; set; }

        public ICollection<ap_ExpenseLines> ExpenseLines { get; set; } = new List<ap_ExpenseLines>();
        public ICollection<ap_ExpensesAttachments> Attachments { get; set; } = new List<ap_ExpensesAttachments>();
    }
}