using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet5_WebAPI.Data;
using dotnet5_WebAPI.Dtos.Character;
using dotnet5_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

// With the use of async and the Task interface we make the methods asynchrnous


namespace dotnet5_WebAPI.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        // Dummy list of characters
        // private static List<Character> characters = new List<Character>()
        // {
        //     new Character(),
        //     new Character() {Id = 1, Name = "Sam"}
        // };

        // Injecting an Imapper interface instance to implement methods
        // Passing the constructor the DataContext object parameter to initialize an instance of the database 
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }


        // Getting the id of the user so that we get the characters based on that user
        // Logged user gets hia characters
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));


        // NOTE**** IMPLEMANTING AutoMapper to every Http method to map each character object to a GetCharacterDto object
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {

            // Mapping the new added character object
            // then converting the response to a list
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // Map the given AddCharacterDto object to a Character object
            Character character = _mapper.Map<Character>(newCharacter);

            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId()); // Logged in user

            // Add character to list
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            // Mapping every object to a GetCharacterDto object
            // Asynchrnous
            serviceResponse.Data = await _context.Characters
            .Where(c => c.User.Id == GetUserId()) // Verifying the logged in user
            .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {

                // First will throw an exception if not found
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());

                if (character != null)
                {

                    _context.Characters.Remove(character);

                    await _context.SaveChangesAsync();

                    // Again mapping every character to a GetCharacterDto after updating the list
                    serviceResponse.Data = _context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = " Character not found";
                }

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
            // From this specific user
            var dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync(); // Based on the logged in user
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
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId()); // Only the logged in user
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
                Character character = await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
                if (character.User.Id == GetUserId())
                {
                    // We update manually each and every property so that the values don't revert 
                    // to default when updating the entity(object)
                    character.Name = updateCharacter.Name;
                    character.HitPoints = updateCharacter.HitPoints;
                    character.Strength = updateCharacter.Strength;
                    character.Defense = updateCharacter.Defense;
                    character.Intelligence = updateCharacter.Intelligence;
                    character.Class = updateCharacter.Class;

                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {

                // We get every property from the character that is stored in the database
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User.Id == GetUserId());

                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                    return serviceResponse;
                }
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