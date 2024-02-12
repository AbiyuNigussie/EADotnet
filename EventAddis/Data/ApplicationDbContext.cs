using EventAddis.Entity;
using Microsoft.EntityFrameworkCore;
using WebService.API.Entity;

namespace WebService.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserCredential> UserCredentials { get; set; }
        public DbSet<UserInfo> UserInfos{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserCredential>(entity =>
            {

                entity.Property(e => e.PasswordHash)
                    .HasColumnType("nvarchar(max)")
                    .HasMaxLength(70)
                    .IsUnicode(false);
                entity.Property(e => e.CredentialId)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.PasswordSalt)
                    .HasColumnType("nvarchar(max)")
                    .IsUnicode(false);
                entity.HasOne(e => e.User).WithOne(e => e.userCrendential)
                    .HasForeignKey<UserCredential>(e => e.UserId);
            });
                
        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.HasDefaultSchema("Identity");
        //    builder.Entity<IdentityUser>(entity =>
        //    {
        //        entity.ToTable(name: "User");
        //    });
        //    builder.Entity<IdentityRole>(entity =>
        //    {
        //        entity.ToTable(name: "Role");
        //    });
        //    builder.Entity<IdentityUserRole<string>>(entity =>
        //    {
        //        entity.ToTable("UserRoles");
        //    });
        //    builder.Entity<IdentityUserClaim<string>>(entity =>
        //    {
        //        entity.ToTable("UserClaims");
        //    });
        //    builder.Entity<IdentityUserLogin<string>>(entity =>
        //    {
        //        entity.ToTable("UserLogins");
        //    });
        //    builder.Entity<IdentityRoleClaim<string>>(entity =>
        //    {
        //        entity.ToTable("RoleClaims");
        //    });
        //    builder.Entity<IdentityUserToken<string>>(entity =>
        //    {
        //        entity.ToTable("UserTokens");
        //    });
        //}
    }
}
