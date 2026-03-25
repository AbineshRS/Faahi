using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Faahi.Model.im_products
{
    [Index(nameof(goods_receipt_line_id),IsUnique =true,Name = "IX_im_GoodsReceiptLineBatches_line")]
    [Index(nameof(business_id),nameof(store_id),nameof(variant_id),nameof(batch_no),nameof(expiry_date),Name = "IX_im_GoodsReceiptLineBatches_variant_batch")]

    public class im_GoodsReceiptLineBatches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid goods_receipt_line_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(business_id))]
        public Faahi.Model.co_business.co_business co_business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }

        [ForeignKey(nameof(store_id))]
        public st_stores st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid variant_id { get; set; }

        [ForeignKey(nameof(variant_id))]
        public im_ProductVariants variants { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string batch_no { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? expiry_date { get; set; } = null;

        [Column(TypeName = "date")]
        public DateOnly? manufacture_date { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal received_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal free_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal rejected_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal accepted_qty { get; set; } = 0;

        [Column(TypeName = "decimal(18,6)")]
        public Decimal unit_cost { get; set; } = 0;

        [Column(TypeName = "decimal(18,6)")]
        public Decimal net_unit_cost { get; set; } = 0;

        [Column(TypeName = "nvarchar(500)")]
        public string? remarks { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime")]
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}
