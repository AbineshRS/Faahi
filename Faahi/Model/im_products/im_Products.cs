using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Faahi.Model.im_products
{
    public class im_Products
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? product_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [ForeignKey("category_id")]
        [Display(Name = "im_ProductCategories")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? category_id { get; set; }

        [ForeignKey("parent_id")]
        [Display(Name = "im_ProductCategories")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? parent_id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? title { get; set; } = null;

        [Column(TypeName = "varchar(max)")]
        public string? description { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? brand { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? tax_class { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? thumbnail_url { get; set; } = null;

        [Column(TypeName = "varchar(10)")]
        public string? HS_CODE { get; set; } = null;

        [Column(TypeName = "varchar(30)")]
        public string? vendor_Code { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? dutyP { get; set; } = null;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? katta { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? featured_item { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? ignore_direct { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? consign_item { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? free_item { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? iqnore_decimal_qty { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? restrict_HS { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? stock { get; set; }  = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

        public ICollection<im_ProductVariants>? im_ProductVariants { get; set; } = null;

    }
}
