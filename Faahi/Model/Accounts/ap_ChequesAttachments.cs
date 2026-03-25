using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(nameof(ChequeId), nameof(UploadedAt), Name = "IX_ap_ChequesAttachments_Cheque")]
    public class ap_ChequesAttachments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid AttachmentId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid ChequeId { get; set; }

        [ForeignKey(nameof(ChequeId))]
        [JsonIgnore]
        public ap_Cheques? Cheque { get; set; }

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