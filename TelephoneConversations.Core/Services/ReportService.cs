using TelephoneConversations.Core.Interfaces;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.DTOs;

namespace TelephoneConversations.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly ICallRepository _callRepository;
        public ReportService(ICallRepository callRepository)
        {
            _callRepository = callRepository;
        }
        public async Task<IEnumerable<CityReportDTO>> GetReportByCitiesAsync(DateTime fromDate, DateTime toDate)
        {
            var calls = await _callRepository.GetCallsByDateRangeAsync(fromDate, toDate);

            var report = calls
                .GroupBy(c => c.City.CityName)
                .Select(g => new CityReportDTO
                {
                    CityName = g.Key,
                    TotalCalls = g.Count(),
                    TotalMinutes = Math.Round(g.Sum(c => (decimal)c.Duration) / 60, 2),
                    AverageCallDuration = g.Average(c => c.Duration),
                    BaseCostSum = g.Sum(c => c.BaseCost),
                    TotalDiscount = g.Sum(c => c.BaseCost - c.CostWithDiscount),
                    FinalCostSum = g.Sum(c => c.CostWithDiscount)
                });

            return report;
        }

        public async Task<IEnumerable<SubscriberReportDTO>> GetReportBySubscribersAsync(DateTime fromDate, DateTime toDate)
        {
            var calls = await _callRepository.GetCallsByDateRangeAsync(fromDate, toDate);

            var report = calls
                .GroupBy(c => c.Subscriber.CompanyName)
                .Select(g => new SubscriberReportDTO
                {
                    SubscriberName = g.Key,
                    TotalCalls = g.Count(),
                    TotalMinutes = Math.Round(g.Sum(c => (decimal)c.Duration) / 60, 2),
                    AverageCallDuration = g.Average(c => c.Duration),
                    BaseCostSum = g.Sum(c => c.BaseCost),
                    TotalDiscount = g.Sum(c => c.BaseCost - c.CostWithDiscount),
                    FinalCostSum = g.Sum(c => c.CostWithDiscount)
                });

            return report;
        }
    }
}
