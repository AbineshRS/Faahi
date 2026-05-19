using Faahi.Model.co_business;
using Faahi.Model.st_sellers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faahi.Dto.Purchase_dto
{
    public class im_StockTransferHeader_data_dto
    {

        public string? transfer_code { get; set; }

        public Guid? from_store_id { get; set; }
        public Guid? transfer_id { get; set; }

        public Guid? to_store_id { get; set; }

        public Decimal? total_quantity { get; set; } = 0m;

        public Decimal? total_amount { get; set; } = 0m;

        public DateTime? created_at { get; set; }

        public string? from_store_name { get; set; }

        public string? to_store_name { get; set; }

        public string? status { get; set; }

    }
}
