using FBR_Invoicing_Integration.DTOs.Seller;

namespace FBR_Invoicing_Integration.Interfaces
{
    public interface ISellerServices
    {
        Task<SellerResponseDTO> RegisterSellerAsync(SellerRegisterDTO dto);
        Task<IEnumerable<SellerResponseDTO>> GetAllSellersAsync();
        Task<SellerResponseDTO> GetSellerByIdAsync(int id);
        Task<SellerResponseDTO> UpdateSellerAsync(int id, SellerUpdateDTO dto);
        Task<bool> DeleteSellerAsync(int id);
    }
}
