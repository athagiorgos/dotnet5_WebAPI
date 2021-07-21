using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet5_WebAPI.Data;
using dotnet5_WebAPI.Dtos.Character;
using dotnet5_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

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
        // Passing the constructor the DataContext object parameter to initialize an instance of the database 
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }


        // NOTE**** IMPLEMANTING AutoMapper to every Http method to map each character object to a GetCharacterDto object
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {

            // Mapping the new added character object
            // then converting the response to a list
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // Map the given AddCharacterDto object to a Character object
            Character character = _mapper.Map<Character>(newCharacter);
            // Add character to list
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            // Mapping every object to a GetCharacterDto object
            // Asynchrnous
            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {

                // First will throw an exception if not found
                Character character = characters.First(c => c.Id == id);

                characters.Remove(character);


                // Again mapping every character to a GetCharacterDto after updating the list
                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            // We get all the objects from the database
            var dbCharacters = await _context.Characters.ToListAsync();
            // Same as the above method but now mapping every object to a GetCharacterDto object
            // using dbCharacters since we work with the database
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {

            // The parameter of Map function is the actual object that will be mapped
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            // Getting the character with the given id from the database
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            // Mapping the object to be returned from the list with the given id
            // to a GetCharacterDto object type
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);

                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

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