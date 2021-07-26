using System;
using System.Linq;
using System.Threading.Tasks;
using dotnet5_WebAPI.Data;
using dotnet5_WebAPI.Dtos.Character.Fight;
using dotnet5_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet5_WebAPI.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        public FightService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var serviceResponse = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0)
                    opponent.HitPoints -= damage;

                if (opponent.HitPoints <= 0)
                    serviceResponse.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var serviceResponse = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                var skill = attacker.Skills.FirstOrDefault(c => c.Id == request.SkillId);
                if (skill == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{attacker.Name} doesn't know this skill";

                    return serviceResponse;
                }

                int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0)
                    opponent.HitPoints -= damage;

                if (opponent.HitPoints <= 0)
                    serviceResponse.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}