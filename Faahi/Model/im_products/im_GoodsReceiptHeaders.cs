using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(goods_receipt_id),IsUnique =true,AllDescending =true,Name = "IX_goods_receipt_id")]
    [Index(nameof(supplier_id),nameof(receipt_date),AllDescending =true,Name = "IX_im_GoodsReceiptHeaders_supplier")]
    [Index(nameof(business_id),nameof(store_id),nameof(status),nameof(receipt_date), AllDescending = true, Name = "IX_im_GoodsReceiptHeaders_status")]
    public class   im_GoodsReceiptHeaders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid goods_receipt_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(business_id))]
        public Faahi.Model.co_business.co_business co_business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }

        [ForeignKey(nameof (store_id))]
        public st_stores st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? supplier_id { get; set; }

        [ForeignKey(nameof(supplier_id))]
        public ap_Vendors ap_Vendors { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? purchase_order_id { get; set; }

        [ForeignKey(nameof(purchase_order_id))]
        public im_purchase_listing im_purchase_listing { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Goods_recipt_code { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? purchase_entry_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? warehouse_id { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? received_by { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_by { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? posted_by { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? cancelled_by { get; set; }=null;

        [Column(TypeName = "nvarchar(30)")]
        public string receipt_no { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime receipt_date { get; set; }= DateTime.Now;

        [Column(TypeName = "nvarchar(50)")]
        public string? supplier_invoice_no { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? supplier_do_no { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? supplier_ref_no { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        public Decimal subtotal { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_amount { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_amount { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_amount { get; set; } = 0;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }=DateTime.Now;

        [Column(TypeName ="datetime")]
        public DateTime? updated_at { get; set; }=DateTime.Now;

        [Column(TypeName ="datetime")]
        public DateTime? posted_at { get; set; }=DateTime.Now;


        [Column(TypeName = "nvarchar(500)")]
        public string? remarks { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? status { get; set; }

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string is_posted { get; set; }

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string is_cancelled { get; set; }

        public ICollection<im_GoodsReceiptLines>? im_GoodsReceiptLines { get; set; } = null;

    }
}
