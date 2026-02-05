using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace Faahi.Model.im_products
{
    [Index(nameof(product_id),Name = "product_id")]
    [Index(nameof(company_id),Name = "company_id")]
    [Index(nameof(category_id),Name = "category_id")]
    [Index(nameof(sub_category_id),Name = "sub_category_id")]
    [Index(nameof(sub_sub_category_id),Name = "sub_sub_category_id")]
    [Index(nameof(title))]
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

        [ForeignKey("category_id")]
        [Display(Name = "im_ProductCategories")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? sub_category_id { get; set; }

        [ForeignKey("parent_id")]
        [Display(Name = "im_ProductCategories")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? sub_sub_category_id { get; set; } = null;

        [ForeignKey("store_id")]
        [Display(Name = "st_stores")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string? title { get; set; } = null;

        [Column(TypeName = "nvarchar(max)")]
        public string? description { get; set; } = null;

        [Column(TypeName = "varchar(100)")]
        public string? brand { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? tax_class_id { get; set; } = null;

        [Column(TypeName = "varchar(200)")]
        public string? thumbnail_url { get; set; } = null;

        //[Column(TypeName = "varchar(10)")]
        //public string? HS_CODE { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? vendor_Code { get; set; } = null;

        [Column(TypeName = "varchar(32)")]
        public string? kitchen_type { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? updated_at { get; set; } = null;

        [Column(TypeName = "decimal(16, 4)")]
        public Decimal? dutyP { get; set; } = null;

        //[StringLength(1)]
        //[DefaultValue("F")]
        //[Column(TypeName = "char(1)")]
        //public string? katta { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? fixed_price { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? track_expiry { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? allow_below_zero { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? is_multi_unit { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? low_stock_alert { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? published { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? featured_item { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? ignore_direct { get; set; } = string.Empty;

        //[StringLength(1)]
        //[DefaultValue("F")]
        //[Column(TypeName = "char(1)")]
        //public string? consign_item { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? has_free_item { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? restrict_deciaml_qty { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("F")]
        [Column(TypeName = "char(1)")]
        public string? restrict_HS { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("Y")]
        [Column(TypeName = "char(1)")]
        public string? stock_flag { get; set; }  = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? is_varient { get; set; } = string.Empty;

        //Excel 
        //[NotMapped]
        //public string? Category { get; set; } = null;

        //[NotMapped]
        //public string? Sub_Category { get; set; } = null;

        //[NotMapped]
        //public string? Sub_sub_Category { get; set; } = null;

        //[NotMapped]
        //public string? tax_class_name { get; set; } = null;

        //[NotMapped]
        //public Guid? listing_id { get; set; } = null;

        //[NotMapped]
        //public Decimal? cost { get; set; }

        //[NotMapped]
        //public Decimal? Qty { get; set; }

        //[NotMapped]
        //public string? Unit_of_Measure { get; set; }

        //[NotMapped]
        //public string? Other_charge { get; set; }

        //[NotMapped]
        //public DateOnly? expiry_date { get; set; }

        //[NotMapped]
        //public string? Batch_no { get; set; }

        //[NotMapped]
        //public string? Bin_no { get; set; }

        //[NotMapped]
        //public Guid? vendor_id { get; set; }

        //[NotMapped]
        //public string? listing_code { get; set; }
      

        public ICollection<im_ProductVariants>? im_ProductVariants { get; set; } = null;

    }
}
