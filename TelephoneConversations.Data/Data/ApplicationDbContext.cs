using Microsoft.EntityFrameworkCore;
using TelephoneConversations.Core.Models.Entities;

namespace TelephoneConversations.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discount>()
                .ToTable(t => t.HasCheckConstraint("CK_Discount_Duration", "[DurationMax] >= [DurationMin]"));

            modelBuilder.Entity<Subscriber>()
                .ToTable(t => t.HasCheckConstraint("CK_Subscriber_BankAccount", "LEFT([BankAccount], 2) = 'UA'"));

            modelBuilder.Entity<Discount>()
                .HasOne(d => d.Tariff)
                .WithMany(t => t.Discounts)
                .HasForeignKey(d => d.TariffID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
