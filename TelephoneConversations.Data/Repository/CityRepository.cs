using Microsoft.EntityFrameworkCore;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.Entities;
using TelephoneConversations.DataAccess.Data;

namespace TelephoneConversations.DataAccess.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<City>> SearchCitiesAsync(string cityName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return await _db.Cities.ToListAsync(cancellationToken);
            }

            return await _db.Cities
                .Where(s => s.CityName.ToLower().Contains(cityName.ToLower()))
                .ToListAsync(cancellationToken);
        }

        public async Task<City> UpdateAsync(City entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
