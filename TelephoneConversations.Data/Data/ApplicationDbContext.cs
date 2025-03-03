using Microsoft.EntityFrameworkCore;

namespace TelephoneConversations.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base()
        {
        }
    }
}
