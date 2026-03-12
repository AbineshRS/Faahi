using Faahi.Model.am_vcos;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.sales
{
    [Index(nameof(sales_return_id), IsUnique = true, Name = "IX_sales_return_id")]
    [Index(nameof(sales_id), Name = "IX_sales_id")]
    [Index(nameof(business_id), Name = "IX_business_id")]
    [Index(nameof(store_id), Name = "IX_store_id")]
    [Index(nameof(return_no),Name = "IX_return_no")]
    public class so_SalesReturnHeaders
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid sales_return_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid sales_id { get; set; }

        [ForeignKey(nameof(sales_id))]
        [JsonIgnore]
        public so_SalesHeaders? so_SalesHeaders { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; }
        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid store_id { get; set; }
        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores? st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? customer_id { get; set; }
        [ForeignKey(nameof(customer_id))]
        [JsonIgnore]
        public ar_Customers? ar_Customers { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? payment_term_id { get; set; } = null;
        [ForeignKey(nameof(payment_term_id))]
        [JsonIgnore]
        public so_payment_type? So_Payment_Type { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? return_no { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? return_date { get; set; } = null;

        [Column(TypeName = "varchar(10)")]
        [DefaultValue("SALE")] // -- SALE / RETURN / QUOTE
        public string? doc_type { get; set; }

        [Column(TypeName = "varchar(10)")]
        [DefaultValue("RETURN")] // -- SALE / RETURN / QUOTE
        public string? return_type { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? return_reason { get; set; } = null;

        [Column(TypeName = "nvarchar(15)")]
        public string? doc_currency_code { get; set; } = null;

        [Column(TypeName = "nvarchar(15)")]
        public string? base_currency_code { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? fx_rate_to_base { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal sub_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal grand_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal sub_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal grand_total_base { get; set; } = 0;

        [Column(TypeName = "nvarchar(255)")]
        public string? notes { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; } = null;

        [Column(TypeName = "nvarchar(130)")]
        public string? created_by { get; set; } = null;

        public ICollection<so_SalesReturnLines>? so_SalesReturnLines { get; set; } = null;
    }
}
    