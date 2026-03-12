using Faahi.Model.am_vcos;
using Faahi.Model.pos_tables;
using Faahi.Model.sales;
using Faahi.Model.st_sellers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.sales_dto
{
    public class so_SalesHeaders_dto
    {

        public Guid? sales_id { get; set; }

        public long? sales_no { get; set; } = null;

        public string? payment_mode { get; set; } = null;

        public string? invoice_no { get; set; } = null;

        public string? doc_type { get; set; }

        public string? doc_currency_code { get; set; } = null;

        public DateTime? datetime { get; set; } = null;

        public decimal? grand_total { get; set; } = null;

        public string? status { get; set; }

        public string? contact_name { get; set; } = null;
    }
}
