using FBR_Invoicing_Integration.DTOs.Seller;

namespace FBR_Invoicing_Integration.Interfaces
{
    public interface ISellerServices
    {
        Task<IEnumerable<SellerDto>> GetAllAsync();
        Task<SellerDto> GetByIdAsync(int id);
        Task<SellerDto> CreateAsync(CreateSellerDto dto);
        Task<bool> UpdateAsync(int id, CreateSellerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
