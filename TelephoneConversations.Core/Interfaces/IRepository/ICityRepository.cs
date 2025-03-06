using TelephoneConversations.Core.Models;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> UpdateAsync(City entity);
        Task<IEnumerable<City>> SearchCitiesAsync(string cityName, CancellationToken cancellationToken = default);
    }
}
