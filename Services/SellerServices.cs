using FBR_Invoicing_Integration.DTOs.Seller;
using FBR_Invoicing_Integration.Interfaces;

namespace FBR_Invoicing_Integration.Services
{
    public class SellerServices : ISellerServices
    {
        public Task<SellerDto> CreateAsync(CreateSellerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SellerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SellerDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, CreateSellerDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
