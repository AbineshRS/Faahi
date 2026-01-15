using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.Shared_tables
{
    [Index(nameof(address_id), Name = "idx_address_id", IsUnique = true)]
    [Index(nameof(party_id), Name = "idx_party_id")]
    [Index(nameof(customer_id), Name = "idx_customer_id")]
    [Index(nameof(vendor_id), Name = "idx_vendor_id")]

    public class st_PartyAddresses
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? address_id { get; set; }

        [ForeignKey("party_id")]
        [Display(Name = "st_PartyRoles")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? party_id { get; set; } = null;

        [Column(TypeName ="varchar(30)")]
        public string? address_type { get; set; }

        [Column(TypeName ="varchar(200)")]
        public string? line1 { get;set; }

        [Column(TypeName = "varchar(200)")]
        public string? line2 { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? region { get; set; }=null;

        [Column(TypeName = "varchar(30)")]
        public string? postal_code { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? country { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? latitude { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? longitude { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

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

        public ICollection<st_PartyContacts>? PartyContacts { get; set; } = null;

    }
}
