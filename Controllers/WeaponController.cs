using System.Threading.Tasks;
using dotnet5_WebAPI.Dtos.Character;
using dotnet5_WebAPI.Dtos.Weapon;
using dotnet5_WebAPI.Models;
using dotnet5_WebAPI.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_WebAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto weaponDto)
        {
            return Ok(await _weaponService.AddWeapon(weaponDto));
        }
    }
}