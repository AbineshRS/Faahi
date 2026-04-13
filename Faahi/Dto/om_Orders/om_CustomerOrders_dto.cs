using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Faahi.Dto.om_Orders
{
    public class om_CustomerOrders_dto
    {
        public Guid? customer_order_id { get; set; }

        public Guid? business_id { get; set; }

        public DateTime? order_date { get; set; }

        public Guid? source_id { get; set; }

        public string payment_status { get; set; }

        public Decimal? sub_total { get; set; }

        public long? order_no { get; set; }

        public string? delivery_contact_name { get; set; }

        public string? delivery_status { get; set; }

        public string? currency_code { get; set; }

        public string? delivery_contact_no { get; set; }

        public string? delivery_address1 { get; set; }

        public string? delivery_city { get; set; }

        public string? delivery_postal_code { get; set; }

        public string? platform_name { get; set; }

        public string? source_name { get; set; }

        public string? zone_name { get; set; }

        public Decimal? delivery_latitude { get; set; }

        public Decimal? delivery_longitude { get; set; }

        public string? image_url { get; set; }

        public Guid? customer_order_line_id { get; set; }
        public Guid? product_id { get; set; }
        public Guid? variant_id { get; set; }
        public Guid? batch_id { get; set; }
        public Decimal? ordered_qty { get; set; }
        public Decimal? unit_price { get; set; }
        public Decimal? line_total { get; set; }

        public ICollection<om_CustomerOrdersLine_dto> om_CustomerOrdersLine_Dtos { get; set; }
    }
}
