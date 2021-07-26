using System.Threading.Tasks;
using dotnet5_WebAPI.Data;
using dotnet5_WebAPI.Dtos.Character.Fight;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        public FightService(DataContext context)
        {
            _context = context;
        }

        public Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            throw new System.NotImplementedException();
        }
    }
}