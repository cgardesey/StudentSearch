using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSearch.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //         Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //         Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Map Entities to their tables.
            modelBuilder.Entity<AppRole>().ToTable("COLLEGEROLE");
            modelBuilder.Entity<AppUserRole>().ToTable("COLLEGEUSERROLE");
            modelBuilder.Entity<AppUser>().ToTable("COLLEGEUSER");
            modelBuilder.Entity<AppUserClaim>().ToTable("COLLEGEUSERCLAIMS");
            modelBuilder.Entity<AppUserLogin>().ToTable("COLLEGEUSERLOGINS");
            // Set AutoIncrement-Properties
            modelBuilder.Entity<AppUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppUserRole>().Property(r => r.RoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppUserLogin>().Property(r => r.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<AppUserClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Override some column mappings that do not match our default
            modelBuilder.Entity<AppRole>().Property(r => r.Id).HasColumnName("COLLEGEROLEID");
            modelBuilder.Entity<AppRole>().Property(r => r.Name).HasColumnName("ROLENAME");

            modelBuilder.Entity<AppUserRole>().Property(r => r.UserId).HasColumnName("COLLEGEUSERID");
            modelBuilder.Entity<AppUserRole>().Property(r => r.RoleId).HasColumnName("COLLEGEROLEID");

            modelBuilder.Entity<AppUser>().Property(r => r.Id).HasColumnName("COLLEGEUSERID");
            modelBuilder.Entity<AppUser>().Property(r => r.UserName).HasColumnName("USERNAME");

            modelBuilder.Entity<AppUserLogin>().Property(r => r.UserId).HasColumnName("UserId");

            modelBuilder.Entity<AppUserClaim>().Property(r => r.UserId).HasColumnName("UserId");


        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
