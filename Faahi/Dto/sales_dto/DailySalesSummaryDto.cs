namespace Faahi.Dto.sales_dto
{
    public class DailySalesSummaryDto
    {
        public string PaymentType { get; set; }          
        public DateTime SalesDate { get; set; }            
        public int SalesCount { get; set; }                  
        public decimal TotalSales { get; set; }              
        public decimal TotalDiscount { get; set; }           
        public decimal TotalTax { get; set; }               
        public decimal BankCharge { get; set; }              
        public decimal AverageSales { get; set; }            
        public decimal NetSales { get; set; }
    }
}
