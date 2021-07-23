using System.Collections.Generic;

namespace dotnet5_WebAPI.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Frodo";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10;

        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Knight;


        // Defining the user of this character
        // ONE-TO-MANY relationship
        public User User { get; set; }

        public Weapon Weapon { get; set; }

        // MANY-TO-MANY
        public List<Skill> Skills { get; set; }
    }
}