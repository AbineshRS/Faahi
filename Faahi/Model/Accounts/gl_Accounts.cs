using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(nameof(AccountName),
    Name = "UQ_gl_Accounts_BusinessCode")]

    [Index(nameof(CompanyId), nameof(AccountType), nameof(IsActive),
    Name = "IX_gl_Accounts_BusinessType")]
    public class gl_Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column( TypeName = "uniqueidentifier")]
        public Guid GlAccountId { get; set; } // NEWSEQUENTIALID in SQL

        [Column( TypeName = "uniqueidentifier")]
        public Guid CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? CoBusiness { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string AccountNumber { get; set; } = string.Empty; //

        [Column( TypeName = "nvarchar(150)")]
        public string AccountName { get; set; } = string.Empty;   //

        [Column( TypeName = "nvarchar(255)")]
        public string AccountType { get; set; } = string.Empty;   //

        [Column(TypeName = "nvarchar(10)")]
        public string? BalanceType{ get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? NormalBalance { get; set; }

        [Column( TypeName = "nvarchar(50)")]
        public string? DetailType { get; set; }                   //

        [Column( TypeName = "uniqueidentifier")]
        public Guid? ParentAccountId { get; set; }

        [ForeignKey(nameof(ParentAccountId))]
        [JsonIgnore]
        public gl_Accounts? Parent { get; set; }

        [Column( TypeName ="Char(1)")]
        public string IsPostable { get; set; } 

        [Column(TypeName = "Char(1)")]
        public string IsActive { get; set; }

        [Column( TypeName = "nvarchar(10)")]
        public string? CurrencyCode { get; set; }                //

        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningBalance { get; set; } = 0;         //

        [Column( TypeName = "datetime")]
        public DateTime? AsOfDate { get; set; }                  //

        [Column(TypeName = "nvarchar(255)")]
        public string? Description { get; set; }                 //

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        [NotMapped]
        public Guid? store_id { get; set; }

        [NotMapped]
        public decimal? CurrentBalance { get; set; }
    }
}
