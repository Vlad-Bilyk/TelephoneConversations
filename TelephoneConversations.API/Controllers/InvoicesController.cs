using Microsoft.AspNetCore.Mvc;
using TelephoneConversations.Core.Interfaces;

namespace TelephoneConversations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("{subscriberId}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInvoicePdf(int subscriberId, DateTime fromDate, DateTime toDate)
        {
            var path = Path.Combine("D:\\Invoices", "invoice_test.pdf");

            var invoiceData = await _invoiceService.GetInvoiceDataAsync(subscriberId, fromDate, toDate);
            if (invoiceData == null)
            {
                return NotFound("Invoice data not found");
            }

            var pdfBytes = _invoiceService.GenerateInvoicePdf(invoiceData);
            var fileName = $"Invoice_{subscriberId}_{DateTime.Now.ToShortDateString}.pdf";

            await System.IO.File.WriteAllBytesAsync(path, pdfBytes);

            return Ok(File(pdfBytes, "application/pdf", fileName));
        }
    }
}
