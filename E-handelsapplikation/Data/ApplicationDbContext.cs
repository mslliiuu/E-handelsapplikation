using Microsoft.EntityFrameworkCore;
using E_handelsapplikation.Models;

//Kod för att mellanhand mellan databas och applikation, hanterar databasanslutningar, datamodeller och CRUD-funktioner.
namespace E_handelsapplikation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
     
        public DbSet<E_handelsapplikation.Models.Review> Review { get; set; } = default!;
    }

}
