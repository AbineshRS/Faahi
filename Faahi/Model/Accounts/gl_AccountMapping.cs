using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.Accounts
{
    [Index(
        nameof(CompanyId),
        nameof(StoreId),
        nameof(Module),
        nameof(PurposeCode),
        Name = "UQ_gl_AccountMapping_CompanyStoreModulePurpose",
        IsUnique = true)]
    public class gl_AccountMapping
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid DefaultId { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "uniqueidentifier")]
        public Guid CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? CoBusiness { get; set; }

        // Optional store/location
        [Column(TypeName = "uniqueidentifier")]
        public Guid? StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]
        [JsonIgnore]
        public st_stores? Store { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string Module { get; set; } = string.Empty;
        // Example: ECOMMERCE, POS, PURCHASE, INVENTORY, GENERAL

        [Required]
        [StringLength(60)]
        [Column(TypeName = "nvarchar(60)")]
        public string PurposeCode { get; set; } = string.Empty;
        // Example: AR_CONTROL, AP_CONTROL, SALES_REVENUE, etc.

        [Column(TypeName = "uniqueidentifier")]
        public Guid? GlAccountId { get; set; }

        [ForeignKey(nameof(GlAccountId))]
        [JsonIgnore]
        public gl_Accounts? GlAccount { get; set; }

        [Required]
        [StringLength(1)]
        [Column(TypeName = "char(1)")]
        public string IsRequired { get; set; } = "T"; // T/F

        [Required]
        [StringLength(1)]
        [Column(TypeName = "char(1)")]
        public string IsActive { get; set; } = "T"; // T/F

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}