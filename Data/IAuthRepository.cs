using System.Threading.Tasks;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Data
{
    public interface IAuthRepository
    {
        // For the User object parameter make sure to use the object location under the Models folder
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<string>> Login(string uername, string password);

        Task<bool> UserExists(string username);
    }
}