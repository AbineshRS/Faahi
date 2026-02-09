using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(listing_id),Name = "listing_id",IsUnique =true)]
    [Index(nameof(site_id),Name ="site_id")]
    [Index(nameof(vendor_id),Name ="vendor_id")]
    public class im_purchase_listing
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? listing_id { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string? listing_code { get; set; } = null;

        [ForeignKey("site_id")]
        [Display(Name = "im_site")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? site_id { get; set; }

        [ForeignKey("vendor_id")]
        [Display(Name = "ap_Vendors")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? vendor_id { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? created_at { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? edited_date_time { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? edit_user_id { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? payment_mode { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? purchase_type { get; set; } = null; //(purchase_type IN ('local','international','consignment')),

        [Column(TypeName = "varchar(50)")]
        public string? supplier_invoice_no { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? supplier_invoice_date { get; set; } = null;

        [Column(TypeName = "varchar(10)")]
        public string? currency_code { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? exchange_rate { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? sub_total { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? discount_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? freight_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? tax_amount { get; set; } = null;

        [Column(TypeName = "decimal(18, 4)")]
        public Decimal? other_expenses { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? received_at { get; set; } = null;

        [Column(TypeName = "varchar(400)")]
        public string? notes { get; set; } = null;

        [Column(TypeName = "varchar(20)")]
        public string? status { get; set; } = null; //('draft','posted','received','void')),

        [Column(TypeName = "decimal(19, 4)")]
        public Decimal? doc_total { get; set; } = null;

        [Column(TypeName ="nvarchar(100)")]
        public string? local_referance { get; set; } = null;

        [Column(TypeName ="decimal(6,2)")]
        public Decimal? plastic_bag { get; set; } = null;

        [Column(TypeName ="decimal(6,2)")]
        public Decimal? gst { get; set; } = null;

        //[Column(TypeName = "varchar(100)")]
        //public string? brand { get; set; } = null;

        //[Column(TypeName = "varchar(200)")]
        //public string? title { get; set; } = null;

        public ICollection<im_purchase_listing_details>? im_purchase_listing_details { get; set; }=null;


    }
}
