using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;
using TelephoneConversations.Core.Interfaces.IRepository;

namespace TelephoneConversations.DataAccess.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly ApplicationDbContext _db;

        public DiscountRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Discount> UpdateAsync(Discount entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
