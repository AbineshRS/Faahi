using Faahi.Model.im_products;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Purchase_dto
{
    public class im_StockTransferLines_dto
    {


        public Guid? transfer_line_id { get; set; }


        public Guid? transfer_id { get; set; }


        public Guid? product_id { get; set; }


        public Guid? variant_id { get; set; }

        public Guid? store_variant_inventory_id { get; set; }


        public Guid? item_batch_id { get; set; }

        public string? batch_number { get; set; } = null;

        public DateOnly? expiry_date { get; set; } = null;

        public Decimal? average_cost { get; set; } = 0m;

        public Decimal? quantity { get; set; } = 0m;

        public Decimal? unit_price { get; set; } = 0m;

        public Decimal? line_total { get; set; } = 0m;

        public DateTime? created_at { get; set; }

        public string? product_title { get; set; }
    }
}
