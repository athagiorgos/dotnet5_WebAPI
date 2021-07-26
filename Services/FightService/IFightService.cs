using System.Threading.Tasks;
using dotnet5_WebAPI.Dtos.Character.Fight;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
    }
}