namespace Faahi.Dto.sales_dto
{
    public class sales_CalculationResult
    {
        public decimal? price_rate { get; set; }

        public decimal? total_price { get; set; }

        public decimal? gst {  get; set; }

        public bool? success { get; set; }
    }
}
