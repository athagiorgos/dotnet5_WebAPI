using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Services.CharacterService
{
    public interface ICharacterService
    {

        // Here we define the method that the service class should implement
        Task<List<Character>> GetAllCharacters();
        Task<Character> GetCharacterById(int id);
        Task<List<Character>> AddCharacter(Character character);
    }
}