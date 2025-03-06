using TelephoneConversations.Core.Models.DTOs;

namespace TelephoneConversations.Core.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> GetInvoiceDataAsync(int subscriberId);
        byte[] GenerateInvoicePdf(InvoiceDTO invoice);
    }
}
