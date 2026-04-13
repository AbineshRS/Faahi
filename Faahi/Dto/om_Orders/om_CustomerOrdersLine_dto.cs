namespace Faahi.Dto.om_Orders
{
    public class om_CustomerOrdersLine_dto
    {
        public Guid? customer_order_line_id { get; set; }
        public Guid? customer_order_id { get; set; }
        public Guid? product_id { get; set; }
        public Guid? variant_id { get; set; }
        public Guid? batch_id { get; set; }
        public Decimal? ordered_qty { get; set; }
        public Decimal? unit_price { get; set; }
        public Decimal? line_total { get; set; }

    }
}
