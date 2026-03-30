using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Finance
{
    public class fin_BankDepositAttachments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid AttachmentId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid DepositId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string FileName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(600)")]
        public string image_url { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        [ForeignKey(nameof(DepositId))]
        public fin_BankDeposits? Deposit { get; set; }
    }
}
