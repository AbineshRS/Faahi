using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(variant_id))]
    [Index(nameof(expiry_date))]
    [Index(nameof(is_on_hold))]
    [Index(nameof(lot_code))]
    public class im_Lots
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid lot_id { get; set; }

        [ForeignKey("variant_id")]
        [Display(Name = "im_ProductVariants")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? variant_id { get; set; }

        [Column(TypeName ="varchar(80)")]
        public string? lot_code { get; set; }

        [Column(TypeName ="date")]
        public DateOnly? mfg_date { get; set; }=null;

        [Column(TypeName ="date")]
        public DateOnly? expiry_date { get; set; }=null;

        [Column(TypeName = "varchar(200)")]
        public string? note { get; set; } = null;


        [Column(TypeName = "decimal(18,4)")]
        public Decimal? quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? committed_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? consign_qty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? promo_price { get; set; }

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("F")]
        public string? is_on_hold { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; }

    }
}
