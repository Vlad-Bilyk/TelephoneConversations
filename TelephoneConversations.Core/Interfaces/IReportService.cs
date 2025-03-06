using TelephoneConversations.Core.Models.DTOs;

namespace TelephoneConversations.Core.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<SubscriberReportDTO>> GetReportBySubscribersAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<CityReportDTO>> GetReportByCitiesAsync(DateTime fromDate, DateTime toDate);
    }
}
