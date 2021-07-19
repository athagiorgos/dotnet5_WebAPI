using System.Collections.Generic;
using System.Linq;
using dotnet5_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character() {Id = 1, Name = "Sam"}
        };


        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get()
        {

            // sent status code 200 (OK) and with our object (our character)
            return Ok(characters);
        }

        // Route attribute in HttpGet to indicate the id as parameter
        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }


        // In the above method the id is sent via the url. In the below method...

        // NOTE*** The JSON or the data is sent via the body of this request.
        [HttpPost]
        public ActionResult<List<Character>> AddCharacters(Character newCharacter)
        {
            characters.Add(newCharacter);
            return Ok(characters);
        }
    }
}