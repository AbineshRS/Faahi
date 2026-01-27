using Faahi.Model.Stores;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.st_sellers
{
    [Index(nameof(store_name))]
    [Index(nameof(company_id))]
    [Index(nameof(store_type))]
    [Index(nameof(created_at))]
    [Index(nameof(status))]

    public class st_stores
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; }

        [ForeignKey("company_id")]
        [Display(Name = "co_business")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; }

        [ForeignKey("timezone_id")]
        [Display(Name = "fx_timezones")]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? timezone_id { get; set; }


        [Column(TypeName = "varchar(255)")]
        public string? store_name { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? store_location { get; set; } = null;

        [DefaultValue("online")]
        [Column(TypeName = "varchar(100)")]
        public string? store_type { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; } = null;

       
        [StringLength(1)]
        [DefaultValue("T")]
        [Column(TypeName = "char(1)")]
        public string? status { get; set; } = string.Empty;

        [Column(TypeName ="time")]
        public TimeOnly? default_close_time { get;set; }

        [Column(TypeName = "nvarchar(25)")]
        public string? phone1 { get; set; } = null;

        [Column(TypeName = "nvarchar(25)")]
        public string? phone2 { get; set; } = null;

        [Column(TypeName = "nvarchar(200)")]
        public string? email { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? tax_identification_number { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? default_invoice_init { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? default_quote_init { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? default_invoice_template { get; set; } = null;

        [Column(TypeName ="nvarchar(50)")]
        public string? default_receipt_template { get; set; } = null;

        [Column(TypeName = "date")]
        public DateOnly? last_transaction_date { get; set; } = null ;

        [Column(TypeName ="char(3)")]
        [StringLength(3)]
        public string? default_currency { get; set; } = null;


        [Column(TypeName = "decimal(18,2)")]
        public Decimal? service_charge { get; set; } = null;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        public string? tax_inclusive_price { get; set; } = null;

        [Column(TypeName ="nvarchar(10)")]
        public string? tax_activity_no { get; set; } = null;

        [Column(TypeName ="nvarchar(200)")]
        public string? tax_payer_name { get; set; } = null;

        [Column(TypeName ="nvarchar(200)")]
        public string? low_stock_alert_email { get; set; } = null;

        [Column(TypeName = "decimal(18,2)")]
        public Decimal? plastic_bag_tax_amount { get; set; } = null;

        [Column(TypeName ="nvarchar(200)")]
        public string? message_on_receipt { get; set; } = null;

        [Column(TypeName = "nvarchar(200)")]
        public string? message_on_invoice { get; set; } = null;

        [Column(TypeName = "nvarchar(100)")]
        public string? store_code { get; set; } = null;


        public ICollection<st_StoresAddres>? st_StoresAddres { get; set; } = null;

        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Ignore for POST
        //public virtual ICollection<st_StoreCategories>? st_StoreCategories { get; set; }

    }
}
