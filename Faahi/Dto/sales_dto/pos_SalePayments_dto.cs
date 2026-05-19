using Faahi.Model.sales;
using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.sales_dto
{
    public class pos_SalePayments_dto
    {

        public Guid? sale_payment_id { get; set; }

        public Guid? business_id { get; set; }

        public Guid? store_id { get; set; } = null;

        public Guid? sale_id { get; set; }

        public Guid? payment_method_id { get; set; }

        public Guid? terminal_id { get; set; } = null;

        public Guid? shift_id { get; set; } = null;

        public Guid? drawer_session_id { get; set; } = null;

        public string? receipt_no { get; set; } = null;

        public int? line_no { get; set; } = 1;

        public string? currency_code { get; set; }

        public Decimal? fx_rate { get; set; } = null;

        public Decimal amount { get; set; } = 0m;

        public Decimal? base_amount { get; set; } = null;

        public Decimal? change_given { get; set; } = 0m;

        public string? reference_no { get; set; } = null;

        public string? notes { get; set; } = null;

        public string is_voided { get; set; } = "F";

        public string? voided_by { get; set; } = null;

        public string? created_by { get; set; } = null;

        public DateTime? voided_at { get; set; } = null;

        public DateTime? created_at { get; set; } = DateTime.Now;

        public ICollection<sys_Images_dto>? sys_Images_dto { get; set; } = null;
    }
}
