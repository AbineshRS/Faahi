using Faahi.Model.im_products;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Product_dto
{
    public class im_products_dto
    {

        [Key]
        [Column(TypeName = "varchar(20)")]
        public Guid? product_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "varchar(20)")]
        public string? company_id { get; set; }

        [ForeignKey("item_class_id")]
        [Display(Name = "im_item_Category")]
        [Column(TypeName = "varchar(20)")]
        public string? item_class_id { get; set; }

        [ForeignKey("item_subclass_id")]
        [Display(Name = "im_item_subcategory")]
        [Column(TypeName = "varchar(20)")]
        public string? item_subclass_id { get; set; }

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
        public string? stock { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

        public ICollection<im_ProductVariants_dto>? im_ProductVariants_dto { get; set; } = null;

    }
}
