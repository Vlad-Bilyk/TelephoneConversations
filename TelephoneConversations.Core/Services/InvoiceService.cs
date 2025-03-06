﻿using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TelephoneConversations.Core.Interfaces;
using TelephoneConversations.Core.Interfaces.IRepository;
using TelephoneConversations.Core.Models.DTOs;

namespace TelephoneConversations.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ICallRepository callRepository;
        private readonly ISubscriberRepository subscriberRepository;
        public InvoiceService(ISubscriberRepository subscriberRepository, ICallRepository callRepository)
        {
            this.subscriberRepository = subscriberRepository;
            this.callRepository = callRepository;
        }

        public byte[] GenerateInvoicePdf(InvoiceDTO invoice)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
                        // Інформація про постачальника
                        col.Item().Text("Постачальник:").FontSize(14).SemiBold();
                        col.Item().Text($"Назва компанії: {invoice.ProviderName}");
                        col.Item().Text($"Адреса: {invoice.ProviderAddress}");
                        col.Item().Text($"Телефон: {invoice.ProviderPhone}");
                        col.Item().Text($"ЄДРПОУ: {invoice.ProviderEDRPOU}");
                        col.Item().Text("Банківські реквізити:");
                        col.Item().Text($"Банк: {invoice.ProviderBank}, МФО: {invoice.ProviderMFO}");
                        col.Item().Text($"Рахунок: {invoice.ProviderBankAccount}");

                        col.Item().PaddingVertical(10);

                        // Інформація про клієнта (одержувача)
                        col.Item().Text("Одержувач:").FontSize(14).SemiBold();
                        col.Item().Text($"Назва компанії: {invoice.SubscriberName}");
                        col.Item().Text($"Адреса: {invoice.SubscriberAddress}");
                        col.Item().Text($"Телефон: {invoice.SubscriberPhone}");
                        col.Item().Text($"ЄДРПОУ: {invoice.SubscriberEDRPOU}");

                        col.Item().PaddingVertical(30);

                        // Заголовок (Номер рахунку)
                        col.Item().Text($"РАХУНОК НА ОПЛАТУ № {invoice.InvoiceNumber}")
                                    .FontSize(16).SemiBold().AlignCenter();
                        col.Item().Text($"від {DateTime.Now:dd.MM.yyyy}")
                                    .FontSize(12).SemiBold().AlignCenter();
                        col.Item().Text($"Підлягає сплаті до {DateTime.Now.AddDays(10):dd.MM.yyyy}")
                                    .FontSize(12).AlignCenter();

                        col.Item().PaddingVertical(10);

                        // Таблиця з деталями послуг
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.RelativeColumn();   // Послуга
                                cols.RelativeColumn();   // Хвилини
                                cols.RelativeColumn();   // Сума без ПДВ
                                cols.RelativeColumn();   // Знижка
                                cols.RelativeColumn();   // ПДВ
                            });

                            col.Item().PaddingVertical(10);

                            table.Header(header =>
                            {
                                header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).Text("Послуга").SemiBold();
                                header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).Text("Хвилини").SemiBold();
                                header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).Text("Сума без ПДВ").SemiBold();
                                header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).Text("Сума знижки").SemiBold();
                                header.Cell().BorderBottom(1).BorderColor(Colors.Black).Padding(5).Text("ПДВ").SemiBold();
                            });

                            // Додавання даних у таблицю (єдина послуга)
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{invoice.ServiceName}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{invoice.TotalMinutes}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{invoice.AmountWithoutVAT:C}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{invoice.TotalDiscount:C}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{invoice.VATPercentage}%");
                        });


                        col.Item().PaddingTop(10).AlignRight().Column(totalCol =>
                        {
                            totalCol.Item().Text($"Разом без ПДВ: {(invoice.AmountWithoutVAT - invoice.TotalDiscount):C}");
                            totalCol.Item().Text($"ПДВ ({invoice.VATPercentage}%): {invoice.VATAmount:C}");
                            totalCol.Item().Text($"До оплати: {invoice.TotalAmountWithVAT:C}").Bold();
                        });


                        col.Item().PaddingVertical(30);

                        // Дата та підпис
                        col.Item().Column(column =>
                        {
                            column.Item().AlignLeft().Text("Рахунок сформовано за дати:").FontSize(12);

                            column.Item().Column(dateColumn =>
                            {
                                dateColumn.Item().AlignLeft().Text($"з {invoice.FromDate:dd.MM.yyyy}").FontSize(12);
                                dateColumn.Item().AlignLeft().Text($"по {invoice.ToDate:dd.MM.yyyy}").FontSize(12);
                            });

                            column.Item().AlignRight().Text("Підпис: _______________").FontSize(12);
                        });

                    });
                });
            });

            return pdf.GeneratePdf();
        }

        public async Task<InvoiceDTO> GetInvoiceDataAsync(int subscriberId, DateTime fromDate, DateTime toDate)
        {
            var today = DateTime.Today;
            var subscriber = await subscriberRepository.GetAsync(s => s.SubscriberID == subscriberId);
            var calls = await callRepository.GetSubscribersCallsForPeriod(subscriberId, fromDate, toDate);

            int totalMinutes = calls.Sum(c => c.Duration) / 60;
            decimal amountWithoutVAT = calls.Sum(c => c.BaseCost);
            decimal totalDiscount = calls.Sum(c => c.BaseCost - c.CostWithDiscount);
            decimal vatPercentage = 20m;
            decimal vatAmount = (amountWithoutVAT - totalDiscount) * vatPercentage / 100m;
            decimal totalAmountWithVAT = (amountWithoutVAT - totalDiscount) + vatAmount;

            var invoiceData = new InvoiceDTO
            {
                ProviderName = "ТОВ Телефонна компанія",
                ProviderAddress = "вул. Прикладна, 10",
                ProviderPhone = "+380441234567",
                ProviderEDRPOU = "12345678",
                ProviderBank = "ПриватБанк",
                ProviderMFO = "305299",
                ProviderBankAccount = "UA1234567890123456789012",

                SubscriberName = subscriber.CompanyName,
                SubscriberAddress = "вул. Тестова 12",
                SubscriberPhone = subscriber.TelephonePoint,
                SubscriberEDRPOU = subscriber.IPN,

                InvoiceNumber = $"{today:yyyyMM}/{subscriberId}",
                InvoiceDate = today,
                PaymentDueDate = today.AddDays(10),
                FromDate = fromDate,
                ToDate = toDate,

                ServiceName = "Міжміські переговори",
                TotalMinutes = totalMinutes,
                AmountWithoutVAT = amountWithoutVAT,
                TotalDiscount = totalDiscount,
                VATPercentage = 20m,
                VATAmount = vatAmount,
                TotalAmountWithVAT = totalAmountWithVAT,
            };

            return invoiceData;
        }
    }
}
