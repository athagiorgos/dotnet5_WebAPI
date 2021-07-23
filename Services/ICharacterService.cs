using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet5_WebAPI.Dtos.Character;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Services.CharacterService
{
    public interface ICharacterService
    {

        // Here we define the method that the service class should implement

        // Wrapping the ServiceResponse object in every method return type.
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId);
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);


        // param type is AddCharacterDto, but method returns <List<GetCharacterDto>> after adding object
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);

        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}