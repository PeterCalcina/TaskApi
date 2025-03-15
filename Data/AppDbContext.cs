using Microsoft.EntityFrameworkCore;

namespace TaskApi.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Models.Task> Tasks { get; set; }
  }

}