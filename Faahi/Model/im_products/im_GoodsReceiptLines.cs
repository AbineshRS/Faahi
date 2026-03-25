using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(goods_receipt_id),IsUnique =true,Name = "IX_goods_receipt_id")]
    [Index(nameof(line_no),Name = "IX_im_GoodsReceiptLines_receipt")]
    [Index(nameof(business_id),nameof(store_id),nameof(variant_id),Name = "IX_im_GoodsReceiptLines_variant")]
    public class im_GoodsReceiptLines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid goods_receipt_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(business_id))]
        public Faahi.Model.co_business.co_business co_business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }

        [ForeignKey(nameof(store_id))]
        public st_stores st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? supplier_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid variant_id { get; set; }

        [ForeignKey(nameof(variant_id))]
        public im_ProductVariants variants { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid  product_id { get; set;}

        [ForeignKey(nameof(product_id))]
        public im_Products products { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? uom_id { get; set; }

        [ForeignKey(nameof(uom_id))]
        public im_UnitsOfMeasure im_UnitsOfMeasure { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? uom_code { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        public string? trans_type { get; set; } = null;

        [Column(TypeName ="int")]
        public int line_no { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal ordered_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal received_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal free_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal rejected_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal accepted_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal unit_cost { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_percent { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_amount { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_amount { get; set; } = 0;

        [Column(TypeName = "decimal(9,4)")]
        public Decimal tax_percent { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal line_amount { get; set; } = 0;

        [Column(TypeName = "decimal(18,6)")]
        public Decimal net_unit_cost { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal net_amount { get; set; } = 0;

        [Column(TypeName = "nvarchar(50)")]
        public string? batch_no { get; set; } = null;

        [Column(TypeName = "date")]
        public DateOnly? expiry_date { get; set; } = null;

        [Column(TypeName = "date")]
        public DateOnly? manufacture_date { get; set; } = null;

        [Column(TypeName = "nvarchar(500)")]
        public string? remarks { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime created_at { get; set; }= DateTime.Now;

        [Column(TypeName ="datetime")]
        public DateTime updated_at { get; set; }= DateTime.Now;

    }
}
