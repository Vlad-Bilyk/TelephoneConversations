using System.Linq.Expressions;
using TelephoneConversations.Core.Models;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface ICallRepository
    {
        Task<IEnumerable<Call>> GetSubscribersCallsForPeriod(int subscriberId, DateTime fromDate, DateTime toDate);
        Task<IEnumerable<Call>> GetCallsByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<Call>> SearchСallsAsync(string? cityName, string? subscriberName, CancellationToken cancellationToken = default);
        Task<List<Call>> GetAllAsync(Expression<Func<Call, bool>>? filter = null);
        Task<Call> GetAsync(Expression<Func<Call, bool>>? filter = null, bool tracked = true);
        Task CreateAsync(Call entity);
        Task SaveAsync();
    }
}
