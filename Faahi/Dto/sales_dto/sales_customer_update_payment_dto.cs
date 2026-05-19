namespace Faahi.Dto.sales_dto
{
    public class sales_customer_update_payment_dto
    {
        public Guid? sales_id { get; set; }

        public ICollection<pos_SalePayments_dto>? pos_SalePayments_dto { get; set; } = null;
    }
}
