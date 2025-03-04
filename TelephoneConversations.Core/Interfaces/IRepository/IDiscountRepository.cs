using TelephoneConversations.Core.Models;

namespace TelephoneConversations.Core.Interfaces.IRepository
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount> UpdateAsync(Discount entity);
    }
}
