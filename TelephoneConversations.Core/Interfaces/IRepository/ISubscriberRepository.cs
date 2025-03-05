using TelephoneConversations.Core.Models;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        Task<Subscriber> UpdateAsync(Subscriber entity);
    }
}
