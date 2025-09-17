namespace FBR_Invoicing_Integration.Models
{
    public class InvoiceItemEntity
    {
        public int Id { get; set; } 

        public int InvoiceId { get; set; }
        public InvoiceEntity Invoice { get; set; }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }

        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
    }
}
