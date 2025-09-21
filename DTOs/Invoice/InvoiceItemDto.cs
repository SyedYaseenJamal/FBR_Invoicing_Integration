namespace FBR_Invoicing_Integration.DTOs.Invoice
{
    public class InvoiceItemDto
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public string ItemHsCode { get; set; }
        public string UOM { get; set; }
        public decimal ValueOfSalesExclST { get; set; }
        public decimal STWithheldAtSource { get; set; }
        public decimal ExtraTax { get; set; }
        public decimal FurtherTax { get; set; }
        public decimal FixedRetailPrice { get; set; }
        public string SroScheduleNo { get; set; }
        public int ItemSrNo { get; set; }
        public decimal TotalValue { get; set; }
    }
}
