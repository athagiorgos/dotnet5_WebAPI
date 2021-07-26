using AutoMapper;
using dotnet5_WebAPI.Dtos.Character;
using dotnet5_WebAPI.Dtos.Skill;
using dotnet5_WebAPI.Dtos.Weapon;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            // Create a map for the neccessary mapping
            // Mapping Character object to GetCharacterDto object
            CreateMap<Character, GetCharacterDto>();

            // Mapping AddCharacterDto object to Character object
            CreateMap<AddCharacterDto, Character>();

            CreateMap<Weapon, GetWeaponDto>();

            CreateMap<Skill, GetSkillDto>();
        }
    }
}