using FBR_Invoicing_Integration.DTOs.Invoice;
using FBR_Invoicing_Integration.Models;

namespace FBR_Invoicing_Integration.Interfaces
{
    public interface IInvoiceServices
    {
        Task<InvoiceEntity> GetInvoiceAsync(int id);
        Task<InvoiceEntity> CreateInvoiceAsync(InvoiceDto dto);
        Task UpdateInvoiceAsync(int id, InvoiceDto dto);
        Task DeleteInvoiceAsync(int id);
    }
}
