using Microsoft.EntityFrameworkCore;
using WFHTracker_Server;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

     public DbSet<WFH> WFHRequests { get; set; }
}