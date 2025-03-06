using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount> UpdateAsync(Discount entity);
        Task<Discount> GetDiscountByDurationAsync(int durationInSeconds);
        Task<IEnumerable<Discount>> SearchDiscountsAsync(int tariffId, CancellationToken cancellationToken = default);
    }
}
