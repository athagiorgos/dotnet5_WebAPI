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
    }
}