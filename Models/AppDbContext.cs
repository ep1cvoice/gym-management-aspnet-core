using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymApp.Models;

public class AppDbContext
    : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserAddress> UserAddresses => Set<UserAddress>();

    public DbSet<UserPass> UserPasses => Set<UserPass>();

    public DbSet<Trainer> Trainers => Set<Trainer>();

    public DbSet<TrainingClass> TrainingClasses => Set<TrainingClass>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<Package> Packages => Set<Package>();

}
