namespace FBR_Invoicing_Integration.Models
{
    public class BuyerEntity
    {
        public int Id { get; set; } 
        public string CNIC { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<InvoiceEntity> Invoices { get; set; }
    }
}

