using System.Collections.Generic;
using dotnet5_WebAPI.Models;

namespace dotnet5_WebAPI.Controllers.Services.CharacterService
{
    public interface ICharacterInterface
    {

        List<Character> GetAllCharacters();
        Character GetCharacterById(int id);
    }
}