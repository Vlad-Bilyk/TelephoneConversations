using QuestPDF.Fluent;
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

        public byte[] GenerateInvoicePdf(InvoiceDTO data)
        {
            var pdf = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Заголовок (Номер рахунку)
                    page.Header().Text($"Рахунок на оплату № {data.InvoiceNumber}")
                                 .FontSize(22).SemiBold().AlignCenter();

                    page.Content().Column(col =>
                    {
                        // Інформація про постачальника
                        col.Item().Text("Постачальник").FontSize(14).SemiBold().Underline();
                        col.Item().Text($"{data.ProviderName}");
                        col.Item().Text($"Адреса: {data.ProviderAddress}");
                        col.Item().Text($"Телефон: {data.ProviderPhone}");
                        col.Item().Text($"ЄДРПОУ: {data.ProviderEDRPOU}");
                        col.Item().Text($"Банк: {data.ProviderBank}, МФО: {data.ProviderMFO}");
                        col.Item().Text($"Рахунок: {data.ProviderBankAccount}");

                        col.Item().PaddingVertical(10).LineHorizontal(1); // Лінія для розділення

                        // Інформація про клієнта (одержувача)
                        col.Item().Text("Одержувач").FontSize(14).SemiBold().Underline();
                        col.Item().Text($"{data.SubscriberName}");
                        col.Item().Text($"Адреса: {data.SubscriberAddress}");
                        col.Item().Text($"Телефон: {data.SubscriberPhone}");
                        col.Item().Text($"ЄДРПОУ: {data.SubscriberEDRPOU}");

                        col.Item().PaddingVertical(10).LineHorizontal(1); // Лінія для розділення

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

                            table.Header(header =>
                            {
                                header.Cell().Text("Послуга").SemiBold();
                                header.Cell().Text("Хвилини").SemiBold();
                                header.Cell().Text("Сума без ПДВ").SemiBold();
                                header.Cell().Text("Знижка").SemiBold();
                                header.Cell().Text("ПДВ").SemiBold();
                            });

                            // Додавання даних у таблицю (єдина послуга)
                            table.Cell().Text($"{data.ServiceName}");
                            table.Cell().Text($"{data.TotalMinutes}");
                            table.Cell().Text($"{data.AmountWithoutVAT:C}");
                            table.Cell().Text($"{data.TotalDiscount:C}");
                            table.Cell().Text($"{data.VATPercentage}%");
                        });

                        col.Item().PaddingTop(10).AlignRight().Column(totalCol =>
                        {
                            totalCol.Item().Text($"Разом без ПДВ: {(data.AmountWithoutVAT - data.TotalDiscount):C}");
                            totalCol.Item().Text($"ПДВ ({data.VATPercentage}%): {data.VATAmount:C}");
                            totalCol.Item().Text($"До оплати: {data.TotalAmountWithVAT:C}").Bold();
                        });

                        // Дата та підпис
                        col.Item().AlignRight().Column(signatureCol =>
                        {
                            signatureCol.Item().Text($"Дата створення: {DateTime.Now:dd.MM.yyyy}").Italic();
                            signatureCol.Item().PaddingTop(20).Text("________________________");
                            signatureCol.Item().Text("Підпис").Italic();
                        });
                    });

                    page.Footer().AlignRight().Text("Цей документ сформовано автоматично.");
                });
            });

            return pdf.GeneratePdf();
        }

        public async Task<InvoiceDTO> GetInvoiceDataAsync(int subscriberId)
        {
            var today = DateTime.Today;
            var fromDate = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var toDate = new DateTime(today.Year, today.Month, 1).AddDays(-1);

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
                ProviderBankAccount = "UA123456789012345678901234567",

                SubscriberName = subscriber.CompanyName,
                SubscriberAddress = "вул. Тестова 12",
                SubscriberPhone = "+380111111111",
                SubscriberEDRPOU = "12345678",

                InvoiceNumber = $"{today:yyyyMM}/{subscriberId}",
                InvoiceDate = today,
                PaymentDueDate = today.AddDays(10),

                ServiceName = "Міжміські переговори",
                TotalMinutes = totalMinutes,
                AmountWithoutVAT = amountWithoutVAT,
                TotalDiscount = totalDiscount,
                VATPercentage = 20m,
                VATAmount = vatAmount,
                TotalAmountWithVAT= totalAmountWithVAT,
            };

            return invoiceData;
        }
    }
}
