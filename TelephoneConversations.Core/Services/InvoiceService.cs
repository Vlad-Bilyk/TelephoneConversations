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
                        col.Item().Text($"Назва компанії: {data.ProviderName}");
                        col.Item().Text($"Адреса: {data.ProviderAddress}");
                        col.Item().Text($"Телефон: {data.ProviderPhone}");
                        col.Item().Text($"ЄДРПОУ: {data.ProviderEDRPOU}");
                        col.Item().Text("Банківські реквізити:");
                        col.Item().Text($"Банк: {data.ProviderBank}, МФО: {data.ProviderMFO}");
                        col.Item().Text($"Рахунок: {data.ProviderBankAccount}");

                        col.Item().PaddingVertical(10);

                        // Інформація про клієнта (одержувача)
                        col.Item().Text("Одержувач:").FontSize(14).SemiBold();
                        col.Item().Text($"Назва компанії: {data.SubscriberName}");
                        col.Item().Text($"Адреса: {data.SubscriberAddress}");
                        col.Item().Text($"Телефон: {data.SubscriberPhone}");
                        col.Item().Text($"ЄДРПОУ: {data.SubscriberEDRPOU}");

                        col.Item().PaddingVertical(30);

                        // Заголовок (Номер рахунку)
                        col.Item().Text($"РАХУНОК НА ОПЛАТУ № {data.InvoiceNumber}")
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
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{data.ServiceName}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{data.TotalMinutes}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{data.AmountWithoutVAT:C}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{data.TotalDiscount:C}");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"{data.VATPercentage}%");
                        });


                        col.Item().PaddingTop(10).AlignRight().Column(totalCol =>
                        {
                            totalCol.Item().Text($"Разом без ПДВ: {(data.AmountWithoutVAT - data.TotalDiscount):C}");
                            totalCol.Item().Text($"ПДВ ({data.VATPercentage}%): {data.VATAmount:C}");
                            totalCol.Item().Text($"До оплати: {data.TotalAmountWithVAT:C}").Bold();
                        });


                        col.Item().PaddingVertical(30);

                        // Дата та підпис
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text($"Дата: {DateTime.Now:dd.MM.yyyy}").FontSize(12);
                            row.RelativeItem().AlignRight().Text("Підпис: _______________").FontSize(12);
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
