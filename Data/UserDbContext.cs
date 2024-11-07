using Microsoft.EntityFrameworkCore;
using MyWalletApi.Models;

namespace MyWalletApi.Data
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) 
        {
            
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
         .HasKey(u => u.Id); 
            
        }

    }
}
