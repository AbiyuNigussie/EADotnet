using EventAddis.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EventAddis.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UserInfo>()
                        .HasOne(c => c.UserCredential)
                        .WithOne(u => u.UserInfo)
                        .HasForeignKey<UserCredential>(c => c.UserId);
        }
    }
}