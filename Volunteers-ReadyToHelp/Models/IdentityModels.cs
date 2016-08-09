using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Volunteers_ReadyToHelp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : CustomUser
    {
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //createing custome table using entity framework
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Avatar> Avatar { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationState> OrganizationState { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<UserPoint> UserPoint { get; set; }
        public DbSet<Abbreviation> Abbreviation { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    ////modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
        //    ////modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        //    ////modelBuilder.Entity<IdentityUser>().ToTable("User");
        //    ////modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
        //    ////modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
        //    //modelBuilder.Entity<IdentityUser>().ToTable("Users");
        //}
    }
}