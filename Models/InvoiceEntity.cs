namespace FBR_Invoicing_Integration.Models
{
    public class InvoiceEntity
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateTime { get; set; }

        public int SellerId { get; set; }
        public SellerEntity Seller { get; set; }

        public int BuyerId { get; set; }
        public BuyerEntity Buyer { get; set; }

        public List<InvoiceItemEntity> Items { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal SalesTax { get; set; }

        public string FbrInvoiceId { get; set; }
        public string QrCode { get; set; }
    }
}
