using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;

namespace TelephoneConversations.DataAccess.Repository
{
    public class CallRepository : ICallRepository
    {
        private readonly ApplicationDbContext _db;

        public CallRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Call entity)
        {
            await _db.Calls.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Call> GetAsync(Expression<Func<Call, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<Call> query = _db.Calls;

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

        public async Task<List<Call>> GetAllAsync(Expression<Func<Call, bool>>? filter = null)
        {
            IQueryable<Call> query = _db.Calls;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
