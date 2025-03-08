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
                .ToTable(t => t.HasCheckConstraint("CK_Discount_Duration", "DurationMax >= DurationMin"));

            modelBuilder.Entity<Subscriber>()
                .ToTable(t => t.HasCheckConstraint("CK_Subscriber_BankAccount", "BankAccount LIKE 'UA%'"));

            modelBuilder.Entity<Discount>()
                .HasOne(d => d.Tariff)
                .WithMany(t => t.Discounts)
                .HasForeignKey(d => d.TariffID)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Cities
            modelBuilder.Entity<City>().HasData(
                new City { CityID = 1, CityName = "Київ" },
                new City { CityID = 2, CityName = "Львів" },
                new City { CityID = 3, CityName = "Одеса" },
                new City { CityID = 4, CityName = "Харків" },
                new City { CityID = 5, CityName = "Дніпро" }
            );

            // Seed Tariffs
            modelBuilder.Entity<Tariff>().HasData(
                new Tariff { TariffID = 1, CityID = 1, DayPrice = 1.50m, NightPrice = 0.75m },
                new Tariff { TariffID = 2, CityID = 2, DayPrice = 1.20m, NightPrice = 0.60m },
                new Tariff { TariffID = 3, CityID = 3, DayPrice = 1.00m, NightPrice = 0.50m },
                new Tariff { TariffID = 4, CityID = 4, DayPrice = 1.40m, NightPrice = 0.70m },
                new Tariff { TariffID = 5, CityID = 5, DayPrice = 1.30m, NightPrice = 0.65m }
            );

            // Seed Subscribers
            modelBuilder.Entity<Subscriber>().HasData(
                new Subscriber { SubscriberID = 1, CompanyName = "Компанія A", TelephonePoint = "+380667898764", IPN = "1234567890", BankAccount = "UA123456789012345678901234567" },
                new Subscriber { SubscriberID = 2, CompanyName = "Компанія B", TelephonePoint = "+380232367890", IPN = "0987654321", BankAccount = "UA987654321098765432109876543" },
                new Subscriber { SubscriberID = 3, CompanyName = "Компанія C", TelephonePoint = "+380343454321", IPN = "1122334455", BankAccount = "UA112233445566778899001122334" }
            );

            // Seed Discounts
            modelBuilder.Entity<Discount>().HasData(
                new Discount { DiscountID = 1, TariffID = 1, DurationMin = 5, DurationMax = 10, DiscountRate = 5m },
                new Discount { DiscountID = 2, TariffID = 1, DurationMin = 10, DurationMax = 30, DiscountRate = 10m },
                new Discount { DiscountID = 3, TariffID = 2, DurationMin = 20, DurationMax = 30, DiscountRate = 7m },
                new Discount { DiscountID = 4, TariffID = 2, DurationMin = 30, DurationMax = 60, DiscountRate = 12m },
                new Discount { DiscountID = 5, TariffID = 3, DurationMin = 10, DurationMax = 15, DiscountRate = 8m },
                new Discount { DiscountID = 6, TariffID = 3, DurationMin = 20, DurationMax = 45, DiscountRate = 15m }
            );

            // Seed Calls
            modelBuilder.Entity<Call>().HasData(
                // ========== SubscriberID = 1 (7 дзвінків) ==========
                new Call
                {
                    CallID = 1,
                    SubscriberID = 1,
                    CityID = 1,  // Київ -> TariffID=1
                    CallDate = new DateTime(2025, 4, 1),
                    Duration = 250,    // 250 с < 300 с => Без знижки
                    TimeOfDay = "день",
                    BaseCost = 6.25m,  // (250/60≈4.17) * 1.50
                    Discount = 0m,
                    CostWithDiscount = 6.25m
                },
                new Call
                {
                    CallID = 2,
                    SubscriberID = 1,
                    CityID = 1,  // Київ -> TariffID=1
                    CallDate = new DateTime(2025, 4, 2),
                    Duration = 450,    // 300 < 450 ≤ 600 => 5% знижка
                    TimeOfDay = "день",
                    BaseCost = 11.25m, // (450/60=7.5) * 1.50
                    Discount = 5m,
                    CostWithDiscount = 10.69m // 11.25 - (11.25*0.05)=10.6875≈10.69
                },
                new Call
                {
                    CallID = 3,
                    SubscriberID = 1,
                    CityID = 1,  // Київ -> TariffID=1
                    CallDate = new DateTime(2025, 4, 3),
                    Duration = 700,    // 600 < 700 ≤ 1800 => 10% знижка
                    TimeOfDay = "ніч",
                    BaseCost = 8.75m,  // (700/60≈11.67) * 0.75
                    Discount = 10m,
                    CostWithDiscount = 7.88m // 8.75 - (8.75*0.10)=7.875≈7.88
                },
                new Call
                {
                    CallID = 4,
                    SubscriberID = 1,
                    CityID = 2,  // Львів -> TariffID=2
                    CallDate = new DateTime(2025, 4, 4),
                    Duration = 1250,   // >1200..≤1800 => 7% знижка (TariffID=2, DiscountID=3)
                    TimeOfDay = "день",
                    BaseCost = 25.00m, // (1250/60≈20.83)*1.20≈25
                    Discount = 7m,
                    CostWithDiscount = 23.25m // 25 - 7% = 23.25
                },
                new Call
                {
                    CallID = 5,
                    SubscriberID = 1,
                    CityID = 2,  // Львів -> TariffID=2
                    CallDate = new DateTime(2025, 4, 5),
                    Duration = 2000,   // >1800..≤3600 => 12% знижка (TariffID=2, DiscountID=4)
                    TimeOfDay = "ніч",
                    BaseCost = 20.00m, // (2000/60≈33.33)*0.60=20
                    Discount = 12m,
                    CostWithDiscount = 17.60m // 20-(20*0.12)=17.60
                },
                new Call
                {
                    CallID = 6,
                    SubscriberID = 1,
                    CityID = 3,  // Одеса -> TariffID=3
                    CallDate = new DateTime(2025, 4, 6),
                    Duration = 650,    // >600..≤900 => 8% знижка (TariffID=3, DiscountID=5)
                    TimeOfDay = "день",
                    BaseCost = 10.83m, // (650/60≈10.83)*1.00
                    Discount = 8m,
                    CostWithDiscount = 9.96m // 10.83 - (10.83*0.08)=9.96
                },
                new Call
                {
                    CallID = 7,
                    SubscriberID = 1,
                    CityID = 3,  // Одеса -> TariffID=3
                    CallDate = new DateTime(2025, 4, 7),
                    Duration = 2400,   // >1200..≤2700 => 15% знижка (TariffID=3, DiscountID=6)
                    TimeOfDay = "ніч",
                    BaseCost = 20.00m, // (2400/60=40)*0.50=20
                    Discount = 15m,
                    CostWithDiscount = 17.00m // 20 - 15% = 17
                },

                // ========== SubscriberID = 2 (5 дзвінків) ==========
                new Call
                {
                    CallID = 8,
                    SubscriberID = 2,
                    CityID = 4,  // Харків -> TariffID=4 (немає Discount)
                    CallDate = new DateTime(2025, 4, 8),
                    Duration = 500,
                    TimeOfDay = "день",
                    BaseCost = 11.67m, // (500/60≈8.33)*1.40≈11.67
                    Discount = 0m,
                    CostWithDiscount = 11.67m
                },
                new Call
                {
                    CallID = 9,
                    SubscriberID = 2,
                    CityID = 4,
                    CallDate = new DateTime(2025, 4, 9),
                    Duration = 999,
                    TimeOfDay = "ніч",
                    BaseCost = 11.66m, // (999/60≈16.65)*0.70≈11.66
                    Discount = 0m,
                    CostWithDiscount = 11.66m
                },
                new Call
                {
                    CallID = 10,
                    SubscriberID = 2,
                    CityID = 5,  // Дніпро -> TariffID=5 (немає Discount)
                    CallDate = new DateTime(2025, 4, 10),
                    Duration = 120,
                    TimeOfDay = "день",
                    BaseCost = 2.60m,  // (120/60=2)*1.30=2.60
                    Discount = 0m,
                    CostWithDiscount = 2.60m
                },
                new Call
                {
                    CallID = 11,
                    SubscriberID = 2,
                    CityID = 5,
                    CallDate = new DateTime(2025, 4, 11),
                    Duration = 1199,
                    TimeOfDay = "ніч",
                    BaseCost = 12.99m, // (1199/60≈19.98)*0.65≈12.99
                    Discount = 0m,
                    CostWithDiscount = 12.99m
                },
                new Call
                {
                    CallID = 12,
                    SubscriberID = 2,
                    CityID = 1,  // Київ -> TariffID=1
                    CallDate = new DateTime(2025, 4, 12),
                    Duration = 700,    // >600..≤1800 => 10% знижка
                    TimeOfDay = "день",
                    BaseCost = 17.50m, // (700/60≈11.67)*1.50≈17.50
                    Discount = 10m,
                    CostWithDiscount = 15.75m
                },

                // ========== SubscriberID = 3 (3 дзвінки) ==========
                new Call
                {
                    CallID = 13,
                    SubscriberID = 3,
                    CityID = 3,  // Одеса -> TariffID=3
                    CallDate = new DateTime(2025, 4, 13),
                    Duration = 700,    // >600..≤900 => 8% знижка
                    TimeOfDay = "день",
                    BaseCost = 11.67m, // (700/60≈11.67)*1.00
                    Discount = 8m,
                    CostWithDiscount = 10.73m // 11.67-(11.67*0.08)=10.7344≈10.73
                },
                new Call
                {
                    CallID = 14,
                    SubscriberID = 3,
                    CityID = 3,
                    CallDate = new DateTime(2025, 4, 14),
                    Duration = 2000,   // >1200..≤2700 => 15% знижка
                    TimeOfDay = "ніч",
                    BaseCost = 16.67m, // (2000/60≈33.33)*0.50=16.67
                    Discount = 15m,
                    CostWithDiscount = 14.17m // 16.67-(16.67*0.15)=14.1695≈14.17
                },
                new Call
                {
                    CallID = 15,
                    SubscriberID = 3,
                    CityID = 1,  // Київ -> TariffID=1
                    CallDate = new DateTime(2025, 4, 15),
                    Duration = 250,    // 250 c < 300 => без знижки
                    TimeOfDay = "день",
                    BaseCost = 6.25m,  // (250/60≈4.17)*1.50
                    Discount = 0m,
                    CostWithDiscount = 6.25m
                }
            );
        }
    }
}
