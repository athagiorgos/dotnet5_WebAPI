using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet5_WebAPI.Dtos.Character;
using dotnet5_WebAPI.Models;

// With the use of async and the Task interface we make the methods asynchrnous


namespace dotnet5_WebAPI.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        // Dummy list of characters
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character() {Id = 1, Name = "Sam"}
        };

        // Injecting an Imapper interface instance to implement methods
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }


        // NOTE**** IMPLEMANTING AutoMapper to every Http method to map each character object to a GetCharacterDto object
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {

            // Mapping the new added character object
            // then converting the response to a list
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // map the given AddCharacterDto object to a Character object
            Character character = _mapper.Map<Character>(newCharacter);
            // find the maximum id of the already saved object and increment it by 1. The pass it
            // as the id of the new object 
            character.Id = characters.Max(c => c.Id) + 1;
            // add character to list
            characters.Add(character);
            // mapping every object to a GetCharacterDto object
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // Same as the above method but now mapping every object to a GetCharacterDto object
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {

            // The parameter of Map function is the actual object that will be mapped
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            // Mapping the object to be returned from the list with the given id
            // to a GetCharacterDto object type
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }
    }
}