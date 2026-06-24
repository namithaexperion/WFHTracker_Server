using Microsoft.EntityFrameworkCore;
using WFHTracker.Entities;
using WFHTracker_Server;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

     public DbSet<WFHRequests> WFHRequests { get; set; }
     public DbSet<AccessRoles> AccessRoles { get; set; }
     public DbSet<Privileges> Privileges { get; set; }
     public DbSet<PrivilegeRoles> PrivilegeRoles { get; set; }
     public DbSet<Resources> Resources { get; set; }
     public DbSet<UserRoles>UserRoles { get; set; }
}