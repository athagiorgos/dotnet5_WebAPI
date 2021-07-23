using dotnet5_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet5_WebAPI.Data
{

    // DataContext to connect with the database
    // must extend from DbContext to apply changes
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // enables us to save and query our model class
        // name of DbSet is usually pluralizing the name of the entity(model)
        // this way entity framework knows what tables it should create
        public DbSet<Character> Characters { get; set; }

        // Same here for users authentication
        public DbSet<User> Users { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Skill> Skills { get; set; }



        // Seeding some data about Skills to the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Fireball", Damage = 30 },
                new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
                new Skill { Id = 3, Name = "Blizzard", Damage = 50 }
            );
        }


    }
}