using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet5_WebAPI.Dtos.Fight;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);

        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);

        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto fightRequestDto);

        Task<ServiceResponse<List<HighScoreDto>>> GetHighScore();
    }
}