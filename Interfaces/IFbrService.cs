using FBR_Invoicing_Integration.Models;

namespace FBR_Invoicing_Integration.Interfaces
{
    public interface IFbrService
    {
        Task<string> AuthenticateAsync(string clientId, string clientSecret);
        Task<(string FbrInvoiceId, string QrCode, string Status, string Message)> SubmitInvoiceAsync(InvoiceEntity invoice);
        Task<(string Status, DateTime VerifiedAt)> GetInvoiceStatusAsync(string invoiceId);
    }
}
