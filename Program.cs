using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GymApp.Models;
using GymApp.Models.Factories;
using GymApp.Services;
using GymApp.Services.Mediators;
using GymApp.Data;



var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddScoped<IPassFactory, PassFactory>();
builder.Services.AddScoped<IBookingMediator, BookingMediator>();
builder.Services.AddScoped<IBookingService, BookingService>();

// -------------------- APP --------------------

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(context);
}


app.Run();
