namespace Faahi.Dto.sales_dto
{
    public class customer_payment_history
    {
        public Guid? sale_id {  get; set; }

        public Guid? sale_payment_id { get; set; }

        public Guid? source_id { get; set; }

        public Guid? image_id { get; set; }


        public DateTime? created_at { get; set; }

        public Decimal? amount { get; set; }

        public string? image_url { get; set; }

        public string? PayTypeCode { get; set; }

        public string? Description { get; set; }

        public ICollection<sys_Images_dto>? sys_Images { get; set; } = null;
    }
}
