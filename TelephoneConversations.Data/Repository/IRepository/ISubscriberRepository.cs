using System.Linq.Expressions;
using TelephoneConversations.Core.Models;

namespace TelephoneConversations.DataAccess.Repository.IRepository
{
    public interface ISubscriberRepository
    {
        Task<List<Subscriber>> GetAllAsync(Expression<Func<Subscriber, bool>> filter = null);
        Task<Subscriber> GetAsync(Expression<Func<Subscriber, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Subscriber entity);
        Task UpdateAsync(Subscriber entity);
        Task RemoveAsync(Subscriber entity);
        Task SaveAsync();
    }
}
