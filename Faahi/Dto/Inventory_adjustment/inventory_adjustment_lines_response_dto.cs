using Faahi.Model.im_products;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Inventory_adjustment
{
    public class inventory_adjustment_lines_response_dto
    {

        public Guid adjustment_detail_id { get; set; }


        public Guid adjustment_id { get; set; }


        public Guid product_id { get; set; }

        public Guid? variant_id { get; set; } = null;


        public Guid? store_variant_inventory_id { get; set; } = null;

        public string? barcode { get; set; } = null;
        public string? title { get; set; } = null;
        public string? sku { get; set; } = null;

        public string? batch_number { get; set; } = null;

        public DateTime? expiry_date { get; set; } = null;

        [DefaultValue("F")]
        public string? track_expiry { get; set; } = null;

        public Decimal system_qty { get; set; }

        public Decimal counted_qty { get; set; }

        public Decimal adjusted_qty { get; set; }

        public Decimal average_cost { get; set; }

        public Decimal total_cost { get; set; }

        public DateTime created_at { get; set; }


        [DefaultValue("F")]
        public string is_posted { get; set; }


        public string adjustment_type { get; set; }
        public string status { get; set; }
        //PENDING,APPROVED,POSTED,REJECTED
    }
}
