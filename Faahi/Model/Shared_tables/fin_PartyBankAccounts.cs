using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Shared_tables
{
    [Index(nameof(party_account_id),Name = "party_account_id", IsUnique =true)]
    [Index(nameof(party_id),Name = "party_id")]
    public class fin_PartyBankAccounts
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_account_id { get; set; }

        [ForeignKey("party_id")]
        [Display(Name = "st_Parties")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? bank_name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? account_holder_name { get; set; }

        [Column(TypeName = "nvarchar(80)")]
        public string? account_number { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? routing_number { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? swift_code { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? iban { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? currency { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? is_default { get; set; } = string.Empty;

        [ForeignKey("customer_id")]
        [Display(Name = "ar_Customers")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? customer_id { get; set; } = null;

        [ForeignKey("vendor_id")]
        [Display(Name = "ap_Vendors")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? vendor_id { get; set; } = null;
    }
}
