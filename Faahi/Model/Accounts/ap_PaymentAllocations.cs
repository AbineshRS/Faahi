using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    public class ap_PaymentAllocations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid AllocationId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BillId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AppliedAmount { get; set; }

        [ForeignKey(nameof(PaymentId))]
        public ap_Payments? Payment { get; set; }
    }
}
