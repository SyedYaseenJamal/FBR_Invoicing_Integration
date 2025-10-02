using FBR_Invoicing_Integration.Interfaces;
using FBR_Invoicing_Integration.Models;
using Microsoft.AspNetCore.Mvc;

namespace FBR_Invoicing_Integration.Controllers
{
    [ApiController]
    [Route("mock/fbr")]
    public class FbrMockController : ControllerBase
    {
        private readonly IFbrService _fbrService;

        public FbrMockController(IFbrService fbrService)
        {
            _fbrService = fbrService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] dynamic request)
        {
            try
            {
                string clientId = request?.client_id;
                string clientSecret = request?.client_secret;

                var token = await _fbrService.AuthenticateAsync(clientId, clientSecret);
                return Ok(new { access_token = token, expires_in = 3600 });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpPost("submit-invoice")]
        public async Task<IActionResult> SubmitInvoice([FromBody] InvoiceEntity invoice)
        {
            var result = await _fbrService.SubmitInvoiceAsync(invoice);

            if (result.Status == "FAILED")
                return BadRequest(new { result.Status, result.Message });

            return Ok(new
            {
                result.Status,
                result.FbrInvoiceId,
                result.QrCode,
                result.Message
            });
        }

        [HttpGet("invoice-status/{id}")]
        public async Task<IActionResult> GetInvoiceStatus(string id)
        {
            var result = await _fbrService.GetInvoiceStatusAsync(id);

            if (result.Status == "FAILED")
                return BadRequest(new { result.Status, message = "Invalid invoice ID format" });

            return Ok(new
            {
                invoiceId = id,
                status = result.Status,
                verifiedAt = result.VerifiedAt
            });
        }
    }
}
