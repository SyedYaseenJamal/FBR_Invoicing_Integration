namespace FBR_Invoicing_Integration.Models
{
    public class SellerEntity
    {
        public int Id { get; set; } 
        public string NTN { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }

        public List<InvoiceEntity> Invoices { get; set; }
    }
}
