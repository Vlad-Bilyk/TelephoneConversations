using TelephoneConversations.Core.Models;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface ITariffRepository : IRepository<Tariff>
    {
        Task<Tariff> UpdateAsync(Tariff entity);
    }
}
