using System.Threading.Tasks;
using dotnet5_WebAPI.Data;
using dotnet5_WebAPI.Dtos.User;
using dotnet5_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        // Injecting the Authentication Repository
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;

        }

        [HttpPost("Register")]
        // NOTE**** We can get the user information from the url but it is not good practice
        // We will receive it as a Dto, hence the use of UserRegisterDto
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User { Username = request.Username }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("Login")]
        // NOTE**** We can get the user information from the url but it is not good practice
        // We will receive it as a Dto, hence the use of UserRegisterDto
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
        {
            var response = await _authRepo.Login(
                request.Username, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}