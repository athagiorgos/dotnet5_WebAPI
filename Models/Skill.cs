using System.Collections.Generic;

namespace dotnet5_WebAPI.Models
{
    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }


        // MANY-TO-MANY
        public List<Character> Characters { get; set; }
    }
}