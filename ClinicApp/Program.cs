using ClinicApp.Data;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add identity

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentityCore<Doctor>().AddEntityFrameworkStores<ApplicationDbContext>();

// Add repositories

builder.Services.AddScoped<ISpecialityRepository, SpecialityDbRepository>();

// Invoke configuration builder

var configurationBuilder = new ClinicConfigurationBuilder(builder.Configuration);
configurationBuilder.BuildManagerCredentials();

var app = builder.Build();

app.MapDefaultControllerRoute();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

    if(context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();

    DataInitializer.PopulateSpecialities(serviceProvider.GetRequiredService<ISpecialityRepository>());

    await DataInitializer.EnsureRoles(serviceProvider.GetRequiredService<RoleManager<IdentityRole>>());
    await DataInitializer.CreateManagerAccount(
        serviceProvider.GetRequiredService<UserManager<IdentityUser>>(), configurationBuilder.ManagerUserName,
        configurationBuilder.ManagerPassword);
}

app.Run();
