using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;
using TelephoneConversations.DataAccess.Repository.IRepository;

namespace TelephoneConversations.DataAccess.Repository
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        private readonly ApplicationDbContext _db;

        public SubscriberRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Subscriber> UpdateAsync(Subscriber entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
