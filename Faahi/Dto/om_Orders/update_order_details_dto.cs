using System.ComponentModel.DataAnnotations;

namespace Faahi.Dto.om_Orders
{
    public class update_order_details_dto
    {
        [Required] public Guid business_id { get; set; }
        [Required] public Guid store_id { get; set; }
        [Required] public Guid customer_order_id { get; set; }
        public Guid? fulfillment_id { get; set; }

        public string? expected_payment_method { get; set; }
        public string? payment_status { get; set; }
        public string? delivery_status { get; set; }
        public string? sales_mode { get; set; }
        // ORDER / FULFILLMENT / DELIVERY / PAYMENT
        public string? status_type { get; set; }
        public string? notes { get; set; }
        public string? internal_notes { get; set; }

        public DateTime? out_for_delivery_at { get; set; }
        public DateTime? delivered_at { get; set; }
        public decimal? collected_amount { get; set; }
        public decimal? amount { get; set; }
        public Guid? payment_method_id { get; set; }
        public string? reference_no { get; set; }
        public string? payment_note { get; set; }

        public Guid? updated_by { get; set; }

        public List<IFormFile>? receipt_files { get; set; }
        public List<IFormFile>? other_files { get; set; }
    }

    public class update_order_details_result_dto
    {
        public Guid customer_order_id { get; set; }
        public Guid? fulfillment_id { get; set; }
        public int receipt_file_count { get; set; }
        public int other_file_count { get; set; }
        public string? proof_of_delivery_image_url { get; set; }
        public Guid? sale_payment_id { get; set; }
        public string? payment_status { get; set; }
        public string? sales_mode { get; set; } 
        public string? delivery_status { get; set; }
        public string? expected_payment_method { get; set; }
    }
}
