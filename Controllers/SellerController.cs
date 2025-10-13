using FBR_Invoicing_Integration.DTOs.Seller;
using FBR_Invoicing_Integration.Interfaces;
using FBR_Invoicing_Integration.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace FBR_Invoicing_Integration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerServices _sellerService;
        private readonly ILogger<SellerController> _logger;

        public SellerController(ISellerServices sellerService, ILogger<SellerController> logger)
        {
            _sellerService = sellerService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SellerRegisterDTO dto)
        {
            _logger.LogInformation("Received seller registration request for {Name}", dto.Name);

            try
            {
                var seller = await _sellerService.RegisterSellerAsync(dto);
                _logger.LogInformation("Seller registered successfully with ID {SellerId}", seller.Id);
                return CreatedAtAction(nameof(GetById), new { id = seller.Id }, seller);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering seller {Name}", dto.Name);
                return StatusCode(500, new { message = "Error registering seller", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all sellers");

            try
            {
                var sellers = await _sellerService.GetAllSellersAsync();
                _logger.LogInformation("Fetched {Count} sellers", sellers.Count());
                return Ok(sellers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching seller list");
                return StatusCode(500, new { message = "Error fetching seller list", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching seller with ID {SellerId}", id);

            try
            {
                var seller = await _sellerService.GetSellerByIdAsync(id);
                if (seller == null)
                {
                    _logger.LogWarning("Seller with ID {SellerId} not found", id);
                    return NotFound(new{ message = $"Seller with ID {id} not found" });
                }

                return Ok(seller);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching seller with ID {SellerId}", id);
                return StatusCode(500, new { message = "Error fetching seller", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SellerUpdateDTO dto)
        {
            _logger.LogInformation("Updating seller with ID {SellerId}", id);

            try
            {
                var updated = await _sellerService.UpdateSellerAsync(id, dto);
                _logger.LogInformation("Seller with ID {SellerId} updated successfully", id);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Seller with ID {SellerId} not found for update", id);
                return NotFound(new { message = $"Seller with ID {id} not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating seller with ID {SellerId}", id);
                return StatusCode(500, new { message = "Error updating seller", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting seller with ID {SellerId}", id);

            try
            {
                bool deleted = await _sellerService.DeleteSellerAsync(id);

                if (!deleted)
                {
                    _logger.LogWarning("Seller with ID {SellerId} not found for deletion", id);
                    return NotFound(new { message = $"Seller with ID {id} not found" });
                }

                _logger.LogInformation("Seller with ID {SellerId} deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting seller with ID {SellerId}", id);
                return StatusCode(500, new { message = "Error deleting seller", details = ex.Message });
            }
        }
    }
}
