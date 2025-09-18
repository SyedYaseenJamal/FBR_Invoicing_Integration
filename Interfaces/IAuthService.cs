using FBR_Invoicing_Integration.DTOs.Auth;
using FBR_Invoicing_Integration.Models;

namespace FBR_Invoicing_Integration.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(UserEntity user);
        Task<UserEntity> ValidateUserAsync(string username, string password);
        Task<UserEntity> RegisterUserAsync(RegisterationDTO registerdto);
    }
}
