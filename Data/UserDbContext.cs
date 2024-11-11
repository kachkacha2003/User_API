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
        public DbSet<RefreshToken> RefreshTokens { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
         .HasKey(u => u.Id);
          modelBuilder.Entity<RefreshToken>()
               .HasOne(rt => rt.User)
               .WithMany(u => u.RefreshTokens)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
