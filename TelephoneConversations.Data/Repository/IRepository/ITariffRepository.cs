using TelephoneConversations.Core.Models;

namespace TelephoneConversations.DataAccess.Repository.IRepository
{
    public interface ITariffRepository : IRepository<Tariff>
    {
        Task<Tariff> UpdateAsync(Tariff entity);
    }
}
