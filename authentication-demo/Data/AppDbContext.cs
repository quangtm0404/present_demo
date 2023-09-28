using authentication_demo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
namespace authentication_demo.Data;
public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }
    public DbSet<AppRole> Role { get; set; } = default!;
    public DbSet<AppUser> User { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        builder.Entity<AppRole>().HasData(
            new AppRole { Id = Guid.NewGuid(), Name = "Admin" }, new AppRole { Id = Guid.NewGuid(), Name = "User" }

        );
        
        
    }
}
