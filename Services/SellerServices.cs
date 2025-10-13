using FBR_Invoicing_Integration.AppDb;
using FBR_Invoicing_Integration.DTOs.Seller;
using FBR_Invoicing_Integration.Interfaces;
using FBR_Invoicing_Integration.Models;
using Microsoft.EntityFrameworkCore;

namespace FBR_Invoicing_Integration.Services
{
    public class SellerService : ISellerServices
    {
        private readonly AppDbContext _context;

        public SellerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SellerResponseDTO> RegisterSellerAsync(SellerRegisterDTO dto)
        {
            var existingSeller = await _context.Sellers.FirstOrDefaultAsync(s => s.Email == dto.Email || s.NTN == dto.NTN);

            if (existingSeller != null)
            {
                throw new InvalidOperationException("Seller with this Email or NTN already exists.");
            }

            var seller = new SellerEntity
            {
                Name = dto.Name,
                NTN = dto.NTN,
                Email = dto.Email,
                BusinessName = dto.BusinessName,
                Address = dto.Address,
                ContactNumber = dto.ContactNumber
            };

            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();
            return MapToResponse(seller);
        }

        public async Task<IEnumerable<SellerResponseDTO>> GetAllSellersAsync()
        {
            return await _context.Sellers
                .Select(s => MapToResponse(s))
                .ToListAsync();
        }

        public async Task<SellerResponseDTO> GetSellerByIdAsync(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                throw new KeyNotFoundException("Seller not found.");
            }

            return MapToResponse(seller);
        }

        public async Task<SellerResponseDTO> UpdateSellerAsync(int id, SellerUpdateDTO dto)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                throw new KeyNotFoundException("Seller not found.");
            }

            seller.Name = dto.Name ?? seller.Name;
            seller.BusinessName = dto.BusinessName ?? seller.BusinessName;
            seller.Address = dto.Address ?? seller.Address;
            seller.ContactNumber = dto.ContactNumber ?? seller.ContactNumber;

            await _context.SaveChangesAsync();
            return MapToResponse(seller);
        }

        public async Task<bool> DeleteSellerAsync(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
                return false;

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return true;
        }

        private static SellerResponseDTO MapToResponse(SellerEntity s) => new()
        {
            Id = s.Id,
            Name = s.Name,
            NTN = s.NTN,
            Email = s.Email,
            BusinessName = s.BusinessName,
            Address = s.Address,
            ContactNumber = s.ContactNumber
        };
    }
}
