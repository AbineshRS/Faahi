using Faahi.Model.Shared_tables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.am_vcos
{
    [Index(nameof(vendor_id),Name ="idx_vendor_id", IsUnique = true)]
    [Index(nameof(party_id), Name = "idx_party_id")]
    [Index(nameof(company_id), Name = "idx_company_id")]
    [Index(nameof(vendor_code), Name = "idx_vendor_code")]
    [Index(nameof(status), Name = "idx_status")]
    public class ap_Vendors
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? vendor_id { get; set; } = Guid.CreateVersion7();

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [ForeignKey("party_id")]
        [Display(Name = "st_Parties")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_id { get; set; }

        [Column(TypeName ="varchar(30)")]
        public string? vendor_code { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? payment_term_id { get; set; }

        [DefaultValue("check")]
        [Column(TypeName = "varchar(20)")]
        public string? preferred_payment_method { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? withholding_tax_rate { get; set; } = null;

        [Column(TypeName ="varchar(40)")]
        public string? ap_control_account { get; set; }=null;

        [Column(TypeName ="varchar(40)")]
        public string? note {  get; set; } =null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

        //Updated at 14/11/2025

        [Column(TypeName = "nvarchar(100)")]
        public string? contact_name { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? contact_phone1 { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? contact_phone2 { get; set; } = null;

        [Column(TypeName = "nvarchar(255)")]
        public string? contact_website { get; set; } = null;

        [Column(TypeName = "nvarchar(150)")]
        public string? contact_email { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? tex_identification_number { get; set; } = null;

        public ICollection<fin_PartyBankAccounts>? fin_PartyBankAccounts { get; set; } = null;

        public ICollection<st_PartyAddresses>? st_PartyAddresses { get; set; } = null;

    }
}
