using Faahi.Model.sales;
using Faahi.Model.st_sellers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Faahi.Model.pos_tables
{
    [Index(nameof(sale_payment_id),Name = "IX_sale_payment_id", IsUnique =true)]
    [Index(nameof (sale_id),Name = "IX_sale_id")]
    [Index(nameof (line_no),Name = "IX_line_no")]
    [Index(nameof (business_id),Name = "IX_business_id")]
    [Index(nameof (store_id),Name = "IX_store_id")]
    [Index(nameof (terminal_id),Name = "IX_terminal_id")]
    [Index(nameof (created_at),Name = "IX_created_at")]
    [Index(nameof (payment_method_id),Name = "IX_payment_method_id")]
    public class pos_SalePayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid sale_payment_id { get; set; }

        [ForeignKey(nameof(business_id))]
        [JsonIgnore]
        public Faahi.Model.co_business.co_business? co_Business { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid business_id { get; set; }

        [ForeignKey(nameof(store_id))]
        [JsonIgnore]
        public st_stores? st_Stores { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? store_id { get; set; } = null;

        [ForeignKey(nameof(sale_id))]
        [JsonIgnore]
        public so_SalesHeaders? sales_headers { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid sale_id { get;set; }

        //[ForeignKey(nameof(payment_method_id))]
        //public so_payment_type? payment_type { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid payment_method_id { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid? terminal_id { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? shift_id { get; set; }=null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? drawer_session_id { get; set; }=null;

        [Column(TypeName = "nvarchar(50)")]
        public string? receipt_no { get; set; } = null;

        [Column(TypeName = "int")]
        public int? line_no { get; set; } = 1;

        [Column(TypeName = "nvarchar(10)")]
        [DefaultValue("MVR")]
        public string? currency_code { get; set; } = "MVR";

        [Column(TypeName = "decimal(18,6)")]
        public Decimal? fx_rate { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0.0)]
        public Decimal amount { get; set; } = 0m;

        [Column(TypeName = "decimal(18,4)")]
        public Decimal? base_amount { get; set; } = null;

        [Column(TypeName = "decimal(18,4)")]
        [DefaultValue(0.0)]
        public Decimal change_given { get; set; } = 0m;

        [Column(TypeName = "nvarchar(100)")]
        public string? reference_no { get; set; } = null;

        [Column(TypeName = "nvarchar(250)")]
        public string? notes { get; set; } = null;

        [Column(TypeName = "char(1)")]
        [DefaultValue("F")]
        [StringLength(1)]
        public string is_voided { get; set; } = "F";

        [Column(TypeName = "nvarchar(50)")]
        public string? voided_by { get; set; } = null;

        [Column(TypeName = "nvarchar(50)")]
        public string? created_by { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? voided_at { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime created_at { get; set; }=DateTime.Now;

       
    }
}
