using System.Linq.Expressions;
using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.Core.Interfaces
{
    public interface ICallService
    {
        Task<IEnumerable<Call>> SearchСallsAsync(string? cityName, string? subscriberName, CancellationToken cancellationToken = default);
        Task<List<Call>> GetAllAsync(Expression<Func<Call, bool>>? filter = null);
        Task<Call> GetAsync(Expression<Func<Call, bool>>? filter = null, bool tracked = true);
        Task<Call> CreateCallAsync(Call call);
    }
}
