using FBR_Invoicing_Integration.DTOs.Invoice;
using FBR_Invoicing_Integration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FBR_Invoicing_Integration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceService;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoiceServices invoiceService, ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            _logger.LogInformation("Fetching invoice with ID {InvoiceId}", id);
            try
            {
                _logger.LogWarning("Invoice with ID {InvoiceId} not found", id);
                var invoice = await _invoiceService.GetInvoiceAsync(id);

                if (invoice == null)
                {

                    return NotFound(new { message = $"Invoice with id {id} not found." });
                }
                _logger.LogInformation("Invoice with ID {InvoiceId} retrieved successfully", id);
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching invoice with ID {InvoiceId}", id);
                return StatusCode(500, new { message = "Something went wrong.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto dto)
        {
            _logger.LogInformation("Creating a new invoice");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state while creating invoice.");
                    return BadRequest(ModelState);
                }
                var invoice = await _invoiceService.CreateInvoiceAsync(dto);
                _logger.LogInformation("Invoice created successfully with ID {InvoiceId}", invoice.Id);
                return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating invoice.");
                return StatusCode(500, new { message = "Unable to create invoice.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceDto dto)
        {
            _logger.LogInformation("Updating invoice with ID {InvoiceId}", id);
            try
            {
                await _invoiceService.UpdateInvoiceAsync(id, dto);
                _logger.LogInformation("Invoice with ID {InvoiceId} updated successfully", id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Invoice with ID {InvoiceId} not found during update", id);
                return NotFound(new { message = $"Invoice with id {id} not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating invoice with ID {InvoiceId}", id);
                return StatusCode(500, new { message = "Unable to update invoice.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            _logger.LogInformation("Deleting invoice with ID {InvoiceId}", id);
            try
            {
                await _invoiceService.DeleteInvoiceAsync(id);
                _logger.LogInformation("Invoice with ID {InvoiceId} deleted successfully", id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Invoice with ID {InvoiceId} not found during delete", id);
                return NotFound(new { message = $"Invoice with id {id} not found." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting invoice with ID {InvoiceId}", id);
                return StatusCode(500, new { message = "Unable to delete invoice.", details = ex.Message });
            }
        }
    }

}
