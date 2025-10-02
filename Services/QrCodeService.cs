using FBR_Invoicing_Integration.Interfaces;
using QRCoder;

namespace FBR_Invoicing_Integration.Services
{
    public class QrCodeService : IQrCodeService
    {
        public string GenerateQrCode(string text)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

                var qrCode = new PngByteQRCode(qrCodeData);
                byte[] qrBytes = qrCode.GetGraphic(20);

                return $"data:image/png;base64,{Convert.ToBase64String(qrBytes)}";
            }
        }
    }
}
