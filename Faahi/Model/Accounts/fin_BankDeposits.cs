using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Finance
{
    [Index(nameof(BusinessId), nameof(DepositNumber), IsUnique = true)]
    [Index(nameof(BusinessId), nameof(DepositDate), nameof(DepositId), Name = "IX_fin_BankDeposits_BusinessDate")]
    public class fin_BankDeposits
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid DepositId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BankAccountId { get; set; } // gl_Accounts

        [Column(TypeName = "nvarchar(100)")]
        public string DepositNumber { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime DepositDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0;

        [Column(TypeName = "nvarchar(max)")]
        public string? Memo { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string Status { get; set; } = "POSTED";

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<fin_BankDepositLines>? Lines { get; set; }
        public ICollection<fin_BankDepositAttachments>? Attachments { get; set; }
    }
}
