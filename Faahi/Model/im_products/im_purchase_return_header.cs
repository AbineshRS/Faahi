using Faahi.Model.am_vcos;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.im_products
{
    [Index(nameof(return_id), Name = "return_id", IsUnique = true)]
    [Index(nameof(site_id), Name = "site_id")]
    [Index(nameof(vendor_id), Name = "vendor_id")]
    public class im_purchase_return_header
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid return_id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? return_code { get; set; }

        // 🔗 Link to original purchase
        [Column(TypeName = "uniqueidentifier")]
        public Guid? listing_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? site_id { get; set; }
        [ForeignKey(nameof(site_id))]
        public st_stores stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? vendor_id { get; set; }
        [ForeignKey(nameof(vendor_id))]
        public ap_Vendors ap_Vendors { get; set; }

        [Column(TypeName = "date")]
        public DateOnly? return_date { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? created_user_id { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? return_type { get; set; } // full / partial

        [Column(TypeName = "varchar(50)")]
        public string? supplier_return_ref { get; set; }

        [Column(TypeName = "varchar(400)")]
        public string? reason { get; set; } // 🔥 IMPORTANT

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? sub_total { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? discount_amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? tax_amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? total_amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? orginal_sub_total { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? orginal_discount_amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? orginal_tax_amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? orginal_total_amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? freight_amount { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? other_expenses { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? plastic_bag { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? exchange_rate { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string? status { get; set; } // draft, approved, completed

        [Column(TypeName = "varchar(400)")]
        public string? notes { get; set; }

        public ICollection<im_purchase_return_details_line>? im_purchase_return_details_line { get; set; } = null;
    }
}
