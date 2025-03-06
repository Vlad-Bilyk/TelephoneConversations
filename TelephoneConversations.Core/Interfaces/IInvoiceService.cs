using TelephoneConversations.Core.Models.DTOs;

namespace TelephoneConversations.Core.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> GetInvoiceDataAsync(int subscriberId, DateTime fromDate, DateTime toDate);
        byte[] GenerateInvoicePdf(InvoiceDTO invoice);
    }
}
