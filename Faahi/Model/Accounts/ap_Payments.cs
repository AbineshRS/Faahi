using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    [Index(nameof(BusinessId), nameof(PaymentDate), nameof(PaymentId), Name = "IX_ap_Payments_BusinessDate")]
    [Index(nameof(BusinessId), nameof(PaymentMethod), nameof(ChequeNumber), Name = "IX_ap_Payments_MethodCheque")]
    public class ap_Payments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid VendorId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime PaymentDate { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string PaymentMethod { get; set; } = string.Empty;

        [Column(TypeName = "uniqueidentifier")]
        public Guid PaymentAccountId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ReferenceNo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ChequeNumber { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ChequeDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ClearedDate { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? CurrencyCode { get; set; }

        [Column(TypeName = "decimal(28,12)")]
        public decimal? ExchangeRate { get; set; }

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
    }
}
