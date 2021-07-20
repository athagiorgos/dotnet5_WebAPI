using System.Collections.Generic;
using System.Linq;
using dotnet5_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using dotnet5_WebAPI.Services.CharacterService;
using System.Threading.Tasks;
using dotnet5_WebAPI.Dtos.Character;


// ServiceResponse object is wrapped around ActionResult
// Best practice to see the response in action

namespace dotnet5_WebAPI.Controllers
{

    [ApiController]

    // This means that the controller can be accesed by his name.
    // In this case 'Character'
    [Route("[controller]")]
    // NOTE**** Controller without support for Views
    // IF we want to support Views implementation the we extend from Controller class.
    public class CharacterController : ControllerBase
    {

        // Dependecy Injection
        public ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {

            // sent status code 200 (OK) and with our object (our character)
            return Ok(await _characterService.GetAllCharacters());
        }

        // Route attribute in HttpGet to indicate the id as parameter
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }


        // In the above method the id is sent via the url. In the below method...

        // NOTE*** The JSON or the data is sent via the body of this request.
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddCharacterDto>>>> AddCharacters(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
    }
}