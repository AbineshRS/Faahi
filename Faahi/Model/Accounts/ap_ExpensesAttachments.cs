using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(nameof(ExpenseId), nameof(UploadedAt), Name = "IX_ap_ExpensesAttachments_Expense")]
    public class ap_ExpensesAttachments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid AttachmentId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid ExpenseId { get; set; }

        [ForeignKey(nameof(ExpenseId))]
        [JsonIgnore]
        public ap_Expenses? Expense { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(600)")]
        public string image_url { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}