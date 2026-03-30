using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(nameof(JournalId), nameof(UploadedAt),
    Name = "IX_gl_JournalAttachments_Journal")]
    public class gl_JournalAttachments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid AttachmentId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid JournalId { get; set; }

        [ForeignKey(nameof(JournalId))]
        [JsonIgnore]
        public gl_JournalHeaders? Journal { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string FileName { get; set; }

        [Column(TypeName = "nvarchar(600)")]
        public string image_url { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
