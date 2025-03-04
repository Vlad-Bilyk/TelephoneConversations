using TelephoneConversations.Core.Models;

namespace TelephoneConversations.DataAccess.Repository.IRepository
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount> UpdateAsync(Discount entity);
    }
}
