using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;
using TelephoneConversations.DataAccess.Repository.IRepository;

namespace TelephoneConversations.DataAccess.Repository
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly ApplicationDbContext _db;

        public SubscriberRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Subscriber entity)
        {
            await _db.Subscribers.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Subscriber> GetAsync(Expression<Func<Subscriber, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Subscriber> query = _db.Subscribers;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Subscriber>> GetAllAsync(Expression<Func<Subscriber, bool>> filter = null)
        {
            IQueryable<Subscriber> query = _db.Subscribers;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(Subscriber entity)
        {
            _db.Subscribers.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subscriber entity)
        {
            _db.Update(entity);
            await SaveAsync();
        }
    }
}
