using Microsoft.EntityFrameworkCore;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.Entities;
using TelephoneConversations.DataAccess.Data;

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

        public async Task<IEnumerable<Subscriber>> SearchSubscribersAsync(string companyName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                return await _db.Subscribers.ToListAsync(cancellationToken);
            }

            return await _db.Subscribers
                .Where(s => s.CompanyName.ToLower().Contains(companyName.ToLower()))
                .ToListAsync(cancellationToken);
        }

        public async Task<Subscriber> UpdateAsync(Subscriber entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
