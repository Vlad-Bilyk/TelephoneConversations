namespace TelephoneConversations.Core.Models.DTOs
{
    public class CityReportDTO
    {
        public string CityName { get; set; }
        public int TotalCalls { get; set; }
        public int TotalMinutes { get; set; }
        public double AverageCallDuration { get; set; }
        public decimal BaseCostSum { get; set; }
        public decimal TotalDiscount {  get; set; }
        public decimal FinalCostSum { get; set; }
    }
}
