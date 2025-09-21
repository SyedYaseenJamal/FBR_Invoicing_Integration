using FBR_Invoicing_Integration.DTOs.Invoice;
using FBR_Invoicing_Integration.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FBR_Invoicing_Integration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceService;

        public InvoiceController(IInvoiceServices invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceAsync(id);

                if (invoice == null)
                    return NotFound(new { message = $"Invoice with id {id} not found." });

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Something went wrong.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var invoice = await _invoiceService.CreateInvoiceAsync(dto);
                return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unable to create invoice.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceDto dto)
        {
            try
            {
                await _invoiceService.UpdateInvoiceAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Invoice with id {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unable to update invoice.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                await _invoiceService.DeleteInvoiceAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Invoice with id {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unable to delete invoice.", details = ex.Message });
            }
        }
    }

}
