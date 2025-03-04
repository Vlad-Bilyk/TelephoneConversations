using TelephoneConversations.Core.Models;

namespace TelephoneConversations.DataAccess.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> UpdateAsync(City entity);
    }
}
