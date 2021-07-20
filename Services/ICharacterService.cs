using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Services.CharacterService
{
    public interface ICharacterService
    {

        // Here we define the method that the service class should implement

        // Wrapping the ServiceResponse object in every method return type.
        Task<ServiceResponse<List<Character>>> GetAllCharacters();
        Task<ServiceResponse<Character>> GetCharacterById(int id);
        Task<ServiceResponse<List<Character>>> AddCharacter(Character character);
    }
}