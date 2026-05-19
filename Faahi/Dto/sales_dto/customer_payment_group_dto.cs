namespace Faahi.Dto.sales_dto
{
    public class customer_payment_group_dto
    {
        public Guid? sale_id { get; set; }
        public List<customer_payment_history> payments { get; set; }


    }
}
