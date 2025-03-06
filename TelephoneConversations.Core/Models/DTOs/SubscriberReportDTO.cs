namespace TelephoneConversations.Core.Models.DTOs
{
    public class SubscriberReportDTO
    {
        public string SubscriberName { get; set; }
        public int TotalCalls { get; set; }
        public decimal TotalMinutes { get; set; }
        public double AverageCallDuration { get; set; }
        public decimal BaseCostSum { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalCostSum { get; set; }
    }
}
