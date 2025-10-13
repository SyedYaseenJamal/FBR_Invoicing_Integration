using FBR_Invoicing_Integration.AppDb;
using FBR_Invoicing_Integration.DTOs.Invoice;
using FBR_Invoicing_Integration.Interfaces;
using FBR_Invoicing_Integration.Models;
using Microsoft.EntityFrameworkCore;

namespace FBR_Invoicing_Integration.Services
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly AppDbContext _context;

        public InvoiceServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceEntity> GetInvoiceAsync(int id)
        {
            return await _context.Invoices.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<InvoiceEntity> CreateInvoiceAsync(InvoiceDto dto)
        {
            var invoice = new InvoiceEntity
            {
                InvoiceNumber = dto.InvoiceNumber,
                DateTime = dto.DateTime,
                SellerId = dto.SellerId,
                BuyerId = dto.BuyerId,
                BuyerRegistrationNo = dto.BuyerRegistrationNo,
                BuyerName = dto.BuyerName,
                BuyerType = dto.BuyerType,
                BuyerAddress = dto.BuyerAddress,
                InvoiceType = dto.InvoiceType,
                DestinationOfSupply = dto.DestinationOfSupply,
                SaleOriginProvince = dto.SaleOriginProvince,
                SaleType = dto.SaleType,
                TotalAmount = dto.TotalAmount,
                SalesTax = dto.SalesTax,
                FbrInvoiceId = dto.FbrInvoiceId,
                QrCode = dto.QrCode,
                Items = dto.Items.Select(x => new InvoiceItemEntity
                {
                    Description = x.Description,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TaxRate = x.TaxRate,
                    TaxAmount = x.TaxAmount,
                    Total = x.Total,
                    ItemHsCode = x.ItemHsCode,
                    UOM = x.UOM,
                    ValueOfSalesExclST = x.ValueOfSalesExclST,
                    STWithheldAtSource = x.STWithheldAtSource,
                    ExtraTax = x.ExtraTax,
                    FurtherTax = x.FurtherTax,
                    FixedRetailPrice = x.FixedRetailPrice,
                    SroScheduleNo = x.SroScheduleNo,
                    ItemSrNo = x.ItemSrNo,
                    TotalValue = x.TotalValue
                }).ToList()
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task UpdateInvoiceAsync(int id, InvoiceDto dto)
        {
            var invoice = await _context.Invoices.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }
            invoice.InvoiceNumber = dto.InvoiceNumber;
            invoice.DateTime = dto.DateTime;
            invoice.TotalAmount = dto.TotalAmount;
            invoice.SalesTax = dto.SalesTax;

            _context.InvoiceItems.RemoveRange(invoice.Items);

            invoice.Items = dto.Items.Select(x => new InvoiceItemEntity
            {
                Description = x.Description,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TaxRate = x.TaxRate,
                TaxAmount = x.TaxAmount,
                Total = x.Total,
                ItemHsCode = x.ItemHsCode,
                UOM = x.UOM,
                ValueOfSalesExclST = x.ValueOfSalesExclST,
                STWithheldAtSource = x.STWithheldAtSource,
                ExtraTax = x.ExtraTax,
                FurtherTax = x.FurtherTax,
                FixedRetailPrice = x.FixedRetailPrice,
                SroScheduleNo = x.SroScheduleNo,
                ItemSrNo = x.ItemSrNo,
                TotalValue = x.TotalValue
            }).ToList();

            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null) throw new KeyNotFoundException("Invoice not found");

            _context.InvoiceItems.RemoveRange(invoice.Items);
            _context.Invoices.Remove(invoice);

            await _context.SaveChangesAsync();
        }
    }
}
