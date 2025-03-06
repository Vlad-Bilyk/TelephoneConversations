namespace TelephoneConversations.Core.Models.DTOs
{
    public class InvoiceDTO
    {
        // provider
        public string ProviderName { get; set; }
        public string ProviderAddress { get; set; }
        public string ProviderPhone { get; set; }
        public string ProviderEDRPOU { get; set; }
        public string ProviderBank { get; set; }
        public string ProviderMFO { get; set; }
        public string ProviderBankAccount { get; set; }

        // subscriber
        public string SubscriberName { get; set; }
        public string SubscriberAddress { get; set; }
        public string SubscriberPhone { get; set; }
        public string SubscriberEDRPOU { get; set; }

        // invoice details
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        // service
        public string ServiceName { get; set; }
        public int TotalMinutes { get; set; }
        public decimal AmountWithoutVAT { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal VATPercentage { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmountWithVAT { get; set; }

        public string ErrorMessage { get; set; }
    }
}
