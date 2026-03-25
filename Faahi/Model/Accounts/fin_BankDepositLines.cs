using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Finance
{
    public class fin_BankDepositLines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid DepositLineId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid DepositId { get; set; }

        [Column(TypeName = "int")]
        public int LineNo { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid SourceAccountId { get; set; } // gl_Accounts

        [Column(TypeName = "nvarchar(100)")]
        public string? ReferenceNo { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        // Navigation
        [ForeignKey(nameof(DepositId))]
        public fin_BankDeposits? Deposit { get; set; }

        [ForeignKey(nameof(SourceAccountId))]
        public Faahi.Model.Accounts.gl_Accounts? SourceAccount { get; set; }
    }
}
