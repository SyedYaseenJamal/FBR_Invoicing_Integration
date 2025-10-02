namespace FBR_Invoicing_Integration.Interfaces
{
    public interface IQrCodeService
    {
        string GenerateQrCode(string text);
    }
}
