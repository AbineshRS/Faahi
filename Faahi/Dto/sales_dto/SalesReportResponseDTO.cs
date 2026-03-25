namespace Faahi.Dto.sales_dto
{
    public class SalesReportResponseDTO
    {
        public SalesTotalDTO Totals { get; set; }
        public ICollection<SalesReportDTO> Sales { get; set; }

    }
}
