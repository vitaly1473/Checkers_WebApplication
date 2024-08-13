// file CheckersClub/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using CheckersClub.Models;

namespace CheckersClub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();  // создаем базу данных при первом обращении
            
        }

        public DbSet<User> Users { get; set; }


    }
}
