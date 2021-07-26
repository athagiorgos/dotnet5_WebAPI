using System.Threading.Tasks;
using dotnet5_WebAPI.Dtos.Character.Fight;
using dotnet5_WebAPI.Models;
using dotnet5_WebAPI.Services.FightService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet5_WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }


        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(WeaponAttackDto request)
        {
            return Ok(await _fightService.WeaponAttack(request));
        }
    }
}