using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    [Index(nameof(BusinessId), Name = "IX_Cheques_Business")]
    [Index(nameof(StoreId), Name = "IX_Cheques_Store")]
    public class ap_Cheques
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid ChequeId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string ChequeNo { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(100)")]
        public string? CheckNumber { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? PayeeId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? PayeeName { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentAccountId { get; set; }

        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? Memo { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Status { get; set; } = "PENDING";

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? UpdatedBy { get; set; }

        public ICollection<ap_ChequeLines> ChequeLines { get; set; } = new List<ap_ChequeLines>();
        public ICollection<ap_ChequesAttachments> Attachments { get; set; } = new List<ap_ChequesAttachments>();
    }
}