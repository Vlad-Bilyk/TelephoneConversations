using Microsoft.EntityFrameworkCore;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;

namespace TelephoneConversations.DataAccess.Repository
{
    public class TariffRepository : Repository<Tariff>, ITariffRepository
    {
        private readonly ApplicationDbContext _db;

        public TariffRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Tariff>> SearchTariffsAsync(int cityId, CancellationToken cancellationToken = default)
        {
            return await _db.Tariffs
                .Where(t => t.CityID == cityId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Tariff> UpdateAsync(Tariff entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
