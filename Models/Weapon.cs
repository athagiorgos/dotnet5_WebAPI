namespace dotnet5_WebAPI.Models
{
    public class Weapon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }

        public Character Character { get; set; }

        // By using the class name with the word Id, ef knows that this is 
        // the corresponding entity with the foreign key
        public int CharacterId { get; set; }

    }
}