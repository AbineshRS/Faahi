using Faahi.Model.am_vcos;
using Faahi.Model.co_business;
using Faahi.Model.pos_tables;
using Faahi.Model.Shared_tables;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.sales
{
    [Index(nameof(sales_id),IsUnique =true,Name = "IX_sales_id")]
    [Index(nameof(business_id),Name = "IX_business_id")]
    [Index(nameof(store_id),Name = "IX_store_id")]
    [Index(nameof(customer_id),Name = "IX_customer_id")]
    [Index(nameof(sales_no),Name = "IX_sales_no")]
    [Index(nameof(due_date),Name = "IX_due_date")]
    [Index(nameof(sales_date), Name = "IX_sales_date")]
    [Index(nameof(invoice_no),Name = "IX_invoice_no")]
    public class so_SalesHeaders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid? sales_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? business_id { get; set; } 
        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        //[SwaggerIgnore]
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

        [Column(TypeName = "uniqueidentifier")]
        public Guid? membership_id { get; set; } = null;

      
        [Column(TypeName = "bigint")]
        public long? sales_no { get; set; } = null;

        [Column(TypeName ="nvarchar(30)")]
        public string? payment_mode { get; set; }=null;

        [Column(TypeName = "nvarchar(50)")]
        public string? invoice_no { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? quot_no { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? purchase_order_no { get; set; } = null;

        [Column(TypeName = "date")]
        public DateOnly? sales_date { get; set; } = null;

        [Column(TypeName ="varchar(30)")]
        [DefaultValue("SALE")] // -- SALE / RETURN / QUOTE
        public string? doc_type { get;set; }

        [Column(TypeName = "date")]
        public DateOnly? due_date { get; set; } = null;


        [Column(TypeName = "decimal(6,2)")]
        public Decimal? tax_percent { get; set; } = null;

        [Column(TypeName ="decimal(6,2)")]
        public Decimal service_charge_percent { get; set; } = 0;

        [Column(TypeName ="nvarchar(25)")]
        [DefaultValue("GENERAL")]
        public string? sales_mode { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? quick_customer { get; set; } = null;

        [Column(TypeName = "nvarchar(20)")]
        public string? reference_no { get; set; } = null;

        [Column(TypeName ="char(1)")]
        [DefaultValue("F")]
        [StringLength(1)]
        public string? sales_on_hold { get; set; }

        //INSURANCE RELATED

        [Column(TypeName = "nvarchar(20)")]
        public string? id_card_no { get; set; } = null;

        [Column(TypeName = "int")]
        public int? age { get; set; } = null;

        // -- ==== Restaurant

        [Column(TypeName = "nvarchar(10)")]
        public string? table_no { get; set; } = null;

        [Column(TypeName = "int")]
        public int? number_of_pax { get; set; } = null;

        [Column(TypeName = "nvarchar(10)")]
        public string? doc_currency_code { get; set; } = null;

        [Column(TypeName = "nvarchar(10)")]
        public string? base_currency_code { get; set; } = null;

        [Column(TypeName = "decimal(18,6)")]
        public Decimal fx_rate_to_base { get; set; } = 0;

        [Column(TypeName = "date")]
        public DateOnly? fx_rate_date { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string? fx_source { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal sub_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal service_charge { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal grand_total { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_plastic_bag { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_taxable_value { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_zero_value { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_exempted_value { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_charge_customer { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_plastic_bag_tax { get; set; } = 0;


        //-- ========= Totals(base currency) =========

        [Column(TypeName = "decimal(18,4)")]
        public Decimal sub_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal discount_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal tax_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal grand_total_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_taxable_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_zero_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_exempted_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? total_charge_customer_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal service_charge_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_plastic_bag_tax_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal total_charge_bank_marchant  { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal transaction_cost { get; set; } = 0;

        //    -- ========= Payments snapshot =========

        [Column(TypeName = "decimal(18,4)")]
        public Decimal amount_paid_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal change_given_base { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal change_given_doc { get; set; } = 0;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal balance_base { get; set; } = 0;

        [Column(TypeName = "nvarchar(30)")]
        public string? qo_validity { get; set; } = null;

        [Column(TypeName = "nvarchar(30)")]
        public string? qo_delivery { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? qo_attention { get; set; } = null;

        [Column(TypeName = "nvarchar(255)")]
        public string? notes { get; set; } = null;

        [Column(TypeName ="datetime")]
        public DateTime? created_at { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? datetime { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? created_by { get; set; }

        [Column(TypeName = "char(1)")]
        [DefaultValue("F")]
        [StringLength(1)]
        public string is_mutiple_payment { get; set; } = "F";

        [Column(TypeName = "varchar(20)")]
        [DefaultValue("OPEN")]
        public string? status { get; set; }


        public ICollection<so_SalesLines>? so_SalesLines { get; set; } = null;

        [NotMapped]
        public ICollection<pos_SalePayments>? pos_SalePayments { get; set; } = null;

        [NotMapped]
        public string? contact_name { get; set; } = null;

        [NotMapped]
        public Guid? address_id { get; set; } = null;

        [NotMapped]
        public Guid? source_id { get; set; } = null;

        [NotMapped]
        public string? urget_delivery { get; set; } = null;
    }
}
