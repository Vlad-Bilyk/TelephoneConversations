﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TelephoneConversations.DataAccess.Data;

#nullable disable

namespace TelephoneConversations.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250308153642_AddTablesAndSeedData")]
    partial class AddTablesAndSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.13");

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Call", b =>
                {
                    b.Property<int>("CallID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BaseCost")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CallDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CityID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CostWithDiscount")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discount")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubscriberID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TimeOfDay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CallID");

                    b.HasIndex("CityID");

                    b.HasIndex("SubscriberID");

                    b.ToTable("Calls");

                    b.HasData(
                        new
                        {
                            CallID = 1,
                            BaseCost = 6.25m,
                            CallDate = new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 1,
                            CostWithDiscount = 6.25m,
                            Discount = 0m,
                            Duration = 250,
                            SubscriberID = 1,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 2,
                            BaseCost = 11.25m,
                            CallDate = new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 1,
                            CostWithDiscount = 10.69m,
                            Discount = 5m,
                            Duration = 450,
                            SubscriberID = 1,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 3,
                            BaseCost = 8.75m,
                            CallDate = new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 1,
                            CostWithDiscount = 7.88m,
                            Discount = 10m,
                            Duration = 700,
                            SubscriberID = 1,
                            TimeOfDay = "ніч"
                        },
                        new
                        {
                            CallID = 4,
                            BaseCost = 25.00m,
                            CallDate = new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 2,
                            CostWithDiscount = 23.25m,
                            Discount = 7m,
                            Duration = 1250,
                            SubscriberID = 1,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 5,
                            BaseCost = 20.00m,
                            CallDate = new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 2,
                            CostWithDiscount = 17.60m,
                            Discount = 12m,
                            Duration = 2000,
                            SubscriberID = 1,
                            TimeOfDay = "ніч"
                        },
                        new
                        {
                            CallID = 6,
                            BaseCost = 10.83m,
                            CallDate = new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 3,
                            CostWithDiscount = 9.96m,
                            Discount = 8m,
                            Duration = 650,
                            SubscriberID = 1,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 7,
                            BaseCost = 20.00m,
                            CallDate = new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 3,
                            CostWithDiscount = 17.00m,
                            Discount = 15m,
                            Duration = 2400,
                            SubscriberID = 1,
                            TimeOfDay = "ніч"
                        },
                        new
                        {
                            CallID = 8,
                            BaseCost = 11.67m,
                            CallDate = new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 4,
                            CostWithDiscount = 11.67m,
                            Discount = 0m,
                            Duration = 500,
                            SubscriberID = 2,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 9,
                            BaseCost = 11.66m,
                            CallDate = new DateTime(2025, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 4,
                            CostWithDiscount = 11.66m,
                            Discount = 0m,
                            Duration = 999,
                            SubscriberID = 2,
                            TimeOfDay = "ніч"
                        },
                        new
                        {
                            CallID = 10,
                            BaseCost = 2.60m,
                            CallDate = new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 5,
                            CostWithDiscount = 2.60m,
                            Discount = 0m,
                            Duration = 120,
                            SubscriberID = 2,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 11,
                            BaseCost = 12.99m,
                            CallDate = new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 5,
                            CostWithDiscount = 12.99m,
                            Discount = 0m,
                            Duration = 1199,
                            SubscriberID = 2,
                            TimeOfDay = "ніч"
                        },
                        new
                        {
                            CallID = 12,
                            BaseCost = 17.50m,
                            CallDate = new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 1,
                            CostWithDiscount = 15.75m,
                            Discount = 10m,
                            Duration = 700,
                            SubscriberID = 2,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 13,
                            BaseCost = 11.67m,
                            CallDate = new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 3,
                            CostWithDiscount = 10.73m,
                            Discount = 8m,
                            Duration = 700,
                            SubscriberID = 3,
                            TimeOfDay = "день"
                        },
                        new
                        {
                            CallID = 14,
                            BaseCost = 16.67m,
                            CallDate = new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 3,
                            CostWithDiscount = 14.17m,
                            Discount = 15m,
                            Duration = 2000,
                            SubscriberID = 3,
                            TimeOfDay = "ніч"
                        },
                        new
                        {
                            CallID = 15,
                            BaseCost = 6.25m,
                            CallDate = new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CityID = 1,
                            CostWithDiscount = 6.25m,
                            Discount = 0m,
                            Duration = 250,
                            SubscriberID = 3,
                            TimeOfDay = "день"
                        });
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("CityID");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            CityID = 1,
                            CityName = "Київ"
                        },
                        new
                        {
                            CityID = 2,
                            CityName = "Львів"
                        },
                        new
                        {
                            CityID = 3,
                            CityName = "Одеса"
                        },
                        new
                        {
                            CityID = 4,
                            CityName = "Харків"
                        },
                        new
                        {
                            CityID = 5,
                            CityName = "Дніпро"
                        });
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Discount", b =>
                {
                    b.Property<int>("DiscountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DiscountRate")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<int>("DurationMax")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DurationMin")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TariffID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscountID");

                    b.HasIndex("TariffID");

                    b.ToTable("Discounts", t =>
                        {
                            t.HasCheckConstraint("CK_Discount_Duration", "DurationMax >= DurationMin");
                        });

                    b.HasData(
                        new
                        {
                            DiscountID = 1,
                            DiscountRate = 5m,
                            DurationMax = 10,
                            DurationMin = 5,
                            TariffID = 1
                        },
                        new
                        {
                            DiscountID = 2,
                            DiscountRate = 10m,
                            DurationMax = 30,
                            DurationMin = 10,
                            TariffID = 1
                        },
                        new
                        {
                            DiscountID = 3,
                            DiscountRate = 7m,
                            DurationMax = 30,
                            DurationMin = 20,
                            TariffID = 2
                        },
                        new
                        {
                            DiscountID = 4,
                            DiscountRate = 12m,
                            DurationMax = 60,
                            DurationMin = 30,
                            TariffID = 2
                        },
                        new
                        {
                            DiscountID = 5,
                            DiscountRate = 8m,
                            DurationMax = 15,
                            DurationMin = 10,
                            TariffID = 3
                        },
                        new
                        {
                            DiscountID = 6,
                            DiscountRate = 15m,
                            DurationMax = 45,
                            DurationMin = 20,
                            TariffID = 3
                        });
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Subscriber", b =>
                {
                    b.Property<int>("SubscriberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasMaxLength(29)
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("IPN")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TelephonePoint")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SubscriberID");

                    b.ToTable("Subscribers", t =>
                        {
                            t.HasCheckConstraint("CK_Subscriber_BankAccount", "BankAccount LIKE 'UA%'");
                        });

                    b.HasData(
                        new
                        {
                            SubscriberID = 1,
                            BankAccount = "UA123456789012345678901234567",
                            CompanyName = "Компанія A",
                            IPN = "1234567890",
                            TelephonePoint = "+380667898764"
                        },
                        new
                        {
                            SubscriberID = 2,
                            BankAccount = "UA987654321098765432109876543",
                            CompanyName = "Компанія B",
                            IPN = "0987654321",
                            TelephonePoint = "+380232367890"
                        },
                        new
                        {
                            SubscriberID = 3,
                            BankAccount = "UA112233445566778899001122334",
                            CompanyName = "Компанія C",
                            IPN = "1122334455",
                            TelephonePoint = "+380343454321"
                        });
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Tariff", b =>
                {
                    b.Property<int>("TariffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DayPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("NightPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.HasKey("TariffID");

                    b.HasIndex("CityID")
                        .IsUnique();

                    b.ToTable("Tariffs");

                    b.HasData(
                        new
                        {
                            TariffID = 1,
                            CityID = 1,
                            DayPrice = 1.50m,
                            NightPrice = 0.75m
                        },
                        new
                        {
                            TariffID = 2,
                            CityID = 2,
                            DayPrice = 1.20m,
                            NightPrice = 0.60m
                        },
                        new
                        {
                            TariffID = 3,
                            CityID = 3,
                            DayPrice = 1.00m,
                            NightPrice = 0.50m
                        },
                        new
                        {
                            TariffID = 4,
                            CityID = 4,
                            DayPrice = 1.40m,
                            NightPrice = 0.70m
                        },
                        new
                        {
                            TariffID = 5,
                            CityID = 5,
                            DayPrice = 1.30m,
                            NightPrice = 0.65m
                        });
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Call", b =>
                {
                    b.HasOne("TelephoneConversations.Core.Models.Entities.City", "City")
                        .WithMany("Calls")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TelephoneConversations.Core.Models.Entities.Subscriber", "Subscriber")
                        .WithMany("Calls")
                        .HasForeignKey("SubscriberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Discount", b =>
                {
                    b.HasOne("TelephoneConversations.Core.Models.Entities.Tariff", "Tariff")
                        .WithMany("Discounts")
                        .HasForeignKey("TariffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tariff");
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Tariff", b =>
                {
                    b.HasOne("TelephoneConversations.Core.Models.Entities.City", "City")
                        .WithOne("Tariff")
                        .HasForeignKey("TelephoneConversations.Core.Models.Entities.Tariff", "CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.City", b =>
                {
                    b.Navigation("Calls");

                    b.Navigation("Tariff")
                        .IsRequired();
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Subscriber", b =>
                {
                    b.Navigation("Calls");
                });

            modelBuilder.Entity("TelephoneConversations.Core.Models.Entities.Tariff", b =>
                {
                    b.Navigation("Discounts");
                });
#pragma warning restore 612, 618
        }
    }
}
