using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    public class im_item_site
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? item_id { get; set; }

        [ForeignKey("site_id")]
        [Display(Name = "im_site")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? site_id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? bin_number { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? primary_vendor_id { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? on_hand_quantity { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? committed_quantity { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? purchase_order_quantity { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? sales_order_quantity { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? c_price { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? edit_user_id { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? on_hold { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;
    }
}
