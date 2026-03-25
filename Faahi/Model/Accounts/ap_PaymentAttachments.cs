using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    public class ap_PaymentAttachments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid AttachmentId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string FileName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(600)")]
        public string ImageUrl { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(PaymentId))]
        public ap_Payments? Payment { get; set; }
    }
}
