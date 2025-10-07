using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.am_vcos
{
    public class ar_Customers
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? customer_id { get; set; }

        [ForeignKey("im_PriceTiers")]
        [Display(Name = "price_tier_id")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? price_tier_id { get; set; }

        [Column(TypeName ="varchar(30)")]
        public string? customer_code { get; set; }

        [Column(TypeName ="varchar(30)")]
        public string? payment_term_id { get; set; }=null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? credit_limit { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? default_billing_address_id { get; set; } = null;

        [Column(TypeName ="varchar(20)")]
        public string? default_shipping_address_id { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? loyalty_points { get; set; } = null;

        [Column(TypeName = "varchar(40)")]
        public string? loyalty_level { get; set; } = null;

        [Column(TypeName = "varchar(50)")]
        public string? note { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? credit_hold { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? tax_exempt { get; set; } = string.Empty;

    }
}
