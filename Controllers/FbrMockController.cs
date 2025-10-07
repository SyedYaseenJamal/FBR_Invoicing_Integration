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
        private readonly ILogger<FbrMockController> _logger;

        public FbrMockController(IFbrService fbrService, ILogger<FbrMockController> logger)
        {
            _fbrService = fbrService;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] dynamic request)
        {
            try
            {
                string clientId = request?.client_id;
                string clientSecret = request?.client_secret;

                _logger.LogInformation("FBR mock authentication requested for ClientId: {ClientId}", clientId);
                var token = await _fbrService.AuthenticateAsync(clientId, clientSecret);
                _logger.LogInformation("Mock authentication successful for ClientId: {ClientId}", clientId);
                return Ok(new { access_token = token, expires_in = 3600 });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Unauthorized attempt during mock FBR authentication. Error: {Error}", ex.Message);
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpPost("submit-invoice")]
        public async Task<IActionResult> SubmitInvoice([FromBody] InvoiceEntity invoice)
        {
            _logger.LogInformation("Mock FBR invoice submission started for Buyer: {BuyerName}", invoice.BuyerName);
             var result = await _fbrService.SubmitInvoiceAsync(invoice);
            if (result.Status == "FAILED")
            {
                _logger.LogWarning("Mock FBR invoice submission failed for Buyer: {BuyerName}. Reason: {Message}", invoice.BuyerName, result.Message);

                    return BadRequest(new { result.Status, result.Message });
            }
            _logger.LogInformation("Mock FBR invoice submitted successfully. FBRInvoiceID: {FbrInvoiceId}", result.FbrInvoiceId);
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
            _logger.LogInformation("Checking mock FBR invoice status for InvoiceID: {InvoiceId}", id);

            var result = await _fbrService.GetInvoiceStatusAsync(id);

            if (result.Status == "FAILED")
            {
                _logger.LogWarning("Mock FBR invoice status check failed for InvoiceID: {InvoiceId}", id);

                return BadRequest(new { result.Status, message = "Invalid invoice ID format" });
            }
            _logger.LogInformation("Mock FBR invoice status retrieved for InvoiceID: {InvoiceId}, Status: {Status}", id, result.Status);

            return Ok(new
            {
                invoiceId = id,
                status = result.Status,
                verifiedAt = result.VerifiedAt
            });
        }
    }
}
