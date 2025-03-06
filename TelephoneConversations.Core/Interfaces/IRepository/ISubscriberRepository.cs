using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        Task<Subscriber> UpdateAsync(Subscriber entity);
        Task<IEnumerable<Subscriber>> SearchSubscribersAsync(string companyName, CancellationToken cancellationToken = default);
    }
}
