using Microsoft.EntityFrameworkCore;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models;
using TelephoneConversations.DataAccess.Data;

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

        public async Task<Discount> GetDiscountByDurationAsync(int durationInSeconds)
        {
            int durationInMinutes = durationInSeconds / 60;

            return await _db.Discounts.FirstOrDefaultAsync(d =>
                durationInMinutes >= d.DurationMin && durationInMinutes < d.DurationMax);
        }

        public async Task<IEnumerable<Discount>> SearchDiscountsAsync(int tariffId, CancellationToken cancellationToken = default)
        {
            return await _db.Discounts
                .Where(d => d.TariffID == tariffId)
                .ToListAsync(cancellationToken);
        }
    }
}
