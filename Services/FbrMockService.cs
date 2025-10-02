using FBR_Invoicing_Integration.Interfaces;
using FBR_Invoicing_Integration.Models;

namespace FBR_Invoicing_Integration.Services
{
    public class FbrMockService : IFbrService
    {
        private readonly IQrCodeService _qrCodeService;

        public FbrMockService(IQrCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        public Task<string> AuthenticateAsync(string clientId, string clientSecret)
        {
            if (clientId == "test-client" && clientSecret == "test-secret")
            {
                return Task.FromResult(Guid.NewGuid().ToString());
            }
            throw new UnauthorizedAccessException("Invalid client credentials");
        }

        public Task<(string FbrInvoiceId, string QrCode, string Status, string Message)> SubmitInvoiceAsync(InvoiceEntity invoice)
        {
            if (string.IsNullOrEmpty(invoice.BuyerName))
                return Task.FromResult((string.Empty, string.Empty, "FAILED", "Buyer name is required"));

            if (invoice.Items == null || !invoice.Items.Any())
                return Task.FromResult((string.Empty, string.Empty, "FAILED", "At least one invoice item required"));

            var expectedTax = invoice.Items.Sum(x => x.TaxAmount);
            if (expectedTax != invoice.SalesTax)
                return Task.FromResult((string.Empty, string.Empty, "FAILED", "Sales tax mismatch"));

            string qrCodeBase64 = _qrCodeService.GenerateQrCode($"InvoiceId:{Guid.NewGuid()}|Buyer:{invoice.BuyerName}");

            return Task.FromResult((
                Guid.NewGuid().ToString(),
                qrCodeBase64,
                "SUCCESS",
                "Invoice accepted in MOCK FBR"
            ));
        }

        public Task<(string Status, DateTime VerifiedAt)> GetInvoiceStatusAsync(string invoiceId)
        {
            if (!Guid.TryParse(invoiceId, out _))
                return Task.FromResult(("FAILED", DateTime.MinValue));

            var statuses = new[] { "VERIFIED", "PENDING", "REJECTED" };
            var status = statuses[new Random().Next(statuses.Length)];

            return Task.FromResult((status, DateTime.UtcNow));
        }
    }
}
