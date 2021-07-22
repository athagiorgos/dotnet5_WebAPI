using System.Threading.Tasks;
using dotnet5_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet5_WebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public Task<ServiceResponse<string>> Login(string uername, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {

            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User already exists.";
                return serviceResponse;
            }
            // Creating the password hash
            CreatePeasswordHash(password, out byte[] passwordHash, out byte[] passswordSalt);

            // assigning password hash and password salt to the user entity fields
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passswordSalt;

            // Adding user to database and return id as service response
            _context.Add(user);
            await _context.SaveChangesAsync();
            serviceResponse.Data = user.Id;
            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(user => user.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }


        // Use of out keyword is to pass the byte arrays by reference rather than by value
        private void CreatePeasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // Return the key to use in HMAC calculation
                passwordSalt = hmac.Key;
                // Computing the hash value for the specified byte array
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}