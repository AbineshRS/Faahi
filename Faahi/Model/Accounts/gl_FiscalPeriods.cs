using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Accounts
{
    [Index(nameof(BusinessId), nameof(IsClosed), nameof(FiscalYear), nameof(FiscalMonth),
    Name = "IX_gl_FiscalPeriods_BusinessClosed")]
    public class gl_FiscalPeriods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid PeriodId { get; set; } // NEWSEQUENTIALID in SQL

        [Column(TypeName = "uniqueidentifier")]
        public Guid BusinessId { get; set; }

        [ForeignKey(nameof(BusinessId))]
        public Faahi.Model.co_business.co_business? CoBusiness { get; set; }

        [Column(TypeName = "int")]
        public int FiscalYear { get; set; }

        [Column(TypeName = "tinyint")]
        public byte FiscalMonth { get; set; } // 1..12

        [Column(TypeName = "datetime")]
        public DateTime PeriodStart { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime PeriodEnd { get; set; }

        [Column(TypeName = "Char(1)")]
        public string IsClosed { get; set; } 

        [Column(TypeName = "datetime")]
        public DateTime? ClosedAt { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? ClosedBy { get; set; } // optional FK to users

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
