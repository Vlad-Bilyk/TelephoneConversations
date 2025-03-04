using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;
using TelephoneConversations.DataAccess.Repository.IRepository;

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

        public async Task<City> UpdateAsync(City entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
