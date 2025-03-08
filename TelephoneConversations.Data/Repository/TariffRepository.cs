using Microsoft.EntityFrameworkCore;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.Entities;
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

        public async Task<IEnumerable<Tariff>> SearchTariffsAsync(string cityName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return await _db.Tariffs.Include(t => t.City).ToListAsync(cancellationToken);

            return await _db.Tariffs
                .Include(t => t.City)
                .Where(t => t.City.CityName.ToLower().Contains(cityName.ToLower()))
                .ToListAsync(cancellationToken);
        }

        public async Task<Tariff> UpdateAsync(Tariff entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Tariff>> GetAllTariffsWithCityAsync()
        {
            return await GetAllAsync(null, "City");
        }
    }
}
