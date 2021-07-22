using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet5_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet5_WebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {

        // Injecting a DataContext instance
        private readonly DataContext _context;

        // Getting access with the configurator from the appsettings.json
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";

            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
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
                // Generating a new password salt
                passwordSalt = hmac.Key;
                // Computing the hash value for the specified byte array
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        // We use the passwordSalt from the database and then use the compute hash method
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // We check byte by byte the computedHash with the passwordHash to authenticate the user 
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private string CreateToken(User user)
        {

            // With that token we can read the user id and username but NOT the password
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // This package includes types that provide support for security tokens and cryptographic operations
            // like signing and verifying signatures
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            // Token descriptor is set with claims (user id and username), symmetric key from appsettings.json
            // and signging credentials based on algorithm
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscriptor);

            // Serializing the security token into a json web token and return it as string
            return tokenHandler.WriteToken(token);
        }
    }
}