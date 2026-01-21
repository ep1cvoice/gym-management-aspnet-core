using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using GymApp.Models;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseSqlite("Data Source=gym.db");

        return new AppDbContext(options.Options);
    }
}
