using DentalClinic.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace DentalClinic.Services
{
    public interface IAuthService
    {
        Task<Auth> RegisterAsync(RegisterPatient model);

        Task<Auth> GetTokenAsync(RequestToken model);

        Task<string> AddUserToRoleAsync(AddRole model);

        Task<Auth> RefreshTokenAsync(string token);

        Task<bool> RevokeTokenAsync(User user,string token);  

    }
}
