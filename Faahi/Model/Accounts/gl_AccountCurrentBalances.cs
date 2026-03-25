using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    [Index(nameof(gl_account_current_id),Name = "IX_gl_account_current_id", IsUnique =true)]
    // Optional extra index if you query by AccountId frequently without BusinessId
    public class gl_AccountCurrentBalances
    {
        [Key]
        public Guid? gl_account_current_id { get; set; }

        [Column( TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column( TypeName = "uniqueidentifier")]
        public Guid? AccountId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBalance { get; set; } = 0m;

        // Strongly recommended: currency (especially for future-proofing marketplace)
        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; } = "MVR";  // ISO 4217 code

        // Concurrency token - EF Core will automatically use this for optimistic locking
        // SQL Server will use rowversion / timestamp type
        [Timestamp]                  // This attribute tells EF it's the concurrency token
        [ConcurrencyCheck]           // Optional but explicit
        public byte[] RowVersion { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Optional: who / how updated (helps in support / audit)
        public Guid? LastUpdatedByUserId { get; set; }
    }
}