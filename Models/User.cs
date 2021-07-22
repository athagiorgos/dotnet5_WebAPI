using System.Collections.Generic;

namespace dotnet5_WebAPI.Models
{

    // Class User is used to authenticate each user and we set some properties
    // that will be needed for identifying the user 
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        // Hash calue of the password
        public byte[] PasswordHash { get; set; }

        // Salt to create a unique password hash
        public byte[] PasswordSalt { get; set; }

        // List of Characters of a user
        // ONE-TO-MANY relationship
        public List<Character> Characters { get; set; }

    }
}