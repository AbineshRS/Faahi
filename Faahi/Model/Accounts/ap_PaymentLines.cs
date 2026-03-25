using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    public class ap_PaymentLines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentLineId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentId { get; set; }

        [Column(TypeName = "int")]
        public int LineNo { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid ExpenseAccountId { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [ForeignKey(nameof(PaymentId))]
        public ap_Payments? Payment { get; set; }

        [ForeignKey(nameof(ExpenseAccountId))]
        public gl_Accounts? ExpenseAccount { get; set; }
    }
}
