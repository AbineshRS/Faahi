using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(detail_id),Name ="detail_id")]
    [Index(nameof(listing_id),Name = "listing_id")]
    [Index(nameof(product_id),Name = "product_id")]
    [Index(nameof(sub_variant_id),Name = "sub_variant_id")]
    public class im_purchase_listing_details
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? detail_id { get; set; }

        [ForeignKey("listing_id")]
        [Display(Name = "im_purchase_listing")]
        public Guid? listing_id { get; set; }

        [ForeignKey("product_id")]
        [Display(Name = "im_Products")]
        public Guid? product_id { get; set; }

        [ForeignKey("sub_variant_id")]
        [Display(Name = "im_product_subvariant")]
        public Guid? sub_variant_id { get; set; }

        [Column(TypeName ="nvarchar(20)")]
        public string? uom_name { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? quantity { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? unit_price { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? discount_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? tax_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? freight_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? other_expenses { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? line_total { get; set; } = null;

        [Column(TypeName = "varchar(400)")]
        public string? notes { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? expiry_date { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? is_varient { get; set; } = null;

        [Column(TypeName ="decimal(18,4)")]
        public Decimal? varient_quantity { get; set; } = null;

        [Column(TypeName ="nvarchar(100)")]
        public string? batch_no { get; set; } = null;

        [Column(TypeName ="nvarchar(50)")]
        public string? bin_no { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? product_description { get; set; } = null;



    }
}
