using Faahi.Model.co_business;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Model.sales
{
    public class so_payment_type
    {
        [Column(TypeName ="nvarchar(20)")]
        public string? PayTypeCode { get;set;}

        [ForeignKey(nameof(company_id))]
        public Faahi.Model.co_business.co_business? co_business { get; set; } = null;

        [Column(TypeName = "uniqueidentifier")]
        public Guid? company_id { get; set; } = null;

        [Column(TypeName = "nvarchar(35)")]
        public string? Description { get; set; } = null;

        [Column(TypeName = "decimal(14,2)")]
        public Decimal? Bank_pcnt { get; set; } = null;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        [DefaultValue("T")]
        public string? is_avilable { get; set; } = null;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        public string? cash_types { get; set; } = null;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        public string? card_type { get; set; } = null;

        [Column(TypeName = "char(1)")]
        [StringLength(1)]
        public string? req_det { get; set; } = null;

        [Column(TypeName = "int")]
        public int? Order { get; set; } = null;


    }
}
