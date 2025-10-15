using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.am_vcos
{
    public class ap_Vendors
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? vendor_id { get; set; } = Guid.CreateVersion7();

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

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

    }
}
