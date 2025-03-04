using TelephoneConversations.Core.Models;

namespace TelephoneConversations.DataAccess.Repository.IRepository
{
    public interface ISubscriberRepository : IRepository<Subscriber>
    {
        Task<Subscriber> UpdateAsync(Subscriber entity);
    }
}
