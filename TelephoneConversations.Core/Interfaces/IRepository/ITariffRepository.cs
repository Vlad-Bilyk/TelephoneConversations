using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface ITariffRepository : IRepository<Tariff>
    {
        Task<Tariff> UpdateAsync(Tariff entity);

        Task<IEnumerable<Tariff>> SearchTariffsAsync(int cityId, CancellationToken cancellationToken = default);
    }
}
