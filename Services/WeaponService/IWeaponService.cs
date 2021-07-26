using System.Threading.Tasks;
using dotnet5_WebAPI.Dtos.Character;
using dotnet5_WebAPI.Dtos.Weapon;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}