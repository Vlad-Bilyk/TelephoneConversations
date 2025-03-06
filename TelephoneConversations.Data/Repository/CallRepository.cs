using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.Entities;
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
            IQueryable<Call> query = _db.Calls
                .Include(c => c.City)
                .Include(s => s.Subscriber);

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

        public async Task<IEnumerable<Call>> SearchСallsAsync(string? cityName, string? subscriberName, CancellationToken cancellationToken = default)
        {
            IQueryable<Call> query = _db.Calls
                .Include(c => c.City)
                .Include(s => s.Subscriber);

            if (!string.IsNullOrWhiteSpace(cityName))
            {
                query = query.Where(c => c.City.CityName.ToLower().Contains(cityName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(subscriberName))
            {
                query = query.Where(c => c.Subscriber.CompanyName.ToLower().Contains(subscriberName.ToLower()));
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Call>> GetCallsByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _db.Calls
                .Include(c => c.City)
                .Include(c => c.Subscriber)
                .Where(c => c.CallDate >= fromDate && c.CallDate <= toDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Call>> GetSubscribersCallsForPeriod(int subscriberId, DateTime fromDate, DateTime toDate)
        {
            return await _db.Calls
                .Where(c => c.SubscriberID == subscriberId &&
                            c.CallDate >= fromDate && c.CallDate <= toDate.AddDays(1))
                .Include(c => c.Subscriber)
                .ToListAsync();
        }
    }
}
