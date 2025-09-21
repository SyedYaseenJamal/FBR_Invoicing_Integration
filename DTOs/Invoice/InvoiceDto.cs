namespace FBR_Invoicing_Integration.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int? Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateTime { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public string BuyerRegistrationNo { get; set; }
        public string BuyerName { get; set; }
        public string BuyerType { get; set; }
        public string BuyerAddress { get; set; }
        public string InvoiceType { get; set; }
        public string DestinationOfSupply { get; set; }
        public string SaleOriginProvince { get; set; }
        public string SaleType { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SalesTax { get; set; }
        public string FbrInvoiceId { get; set; }
        public string QrCode { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
    }
}
