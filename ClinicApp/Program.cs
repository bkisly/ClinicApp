using ClinicApp.Data;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.User;
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

builder.Services.AddIdentityCore<Doctor>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityCore<Manager>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityCore<Patient>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add repositories
builder.Services.AddScoped<ISpecialityRepository, SpecialityDbRepository>();

// Add infrastructural services
builder.Services.AddScoped<IUserManagerProvider, UserManagerProvider>();

// Add services
builder.Services.AddScoped<IIdentityAuthenticationService, IdentityAuthenticationService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

// Invoke configuration builder
var configurationBuilder = new ClinicConfigurationBuilder(builder.Configuration);
configurationBuilder.BuildManagerCredentials();

var app = builder.Build();

app.UseStaticFiles();

app.MapAreaControllerRoute(
    name: "ManageArea",
    areaName: Constants.Areas.ManageAreaName,
    pattern: Constants.Areas.ManageAreaName + "/{controller=Home}/{action=Index}/{id?}");
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
        serviceProvider.GetRequiredService<IRegistrationService>(), configurationBuilder.ManagerUserName,
        configurationBuilder.ManagerPassword);
}

app.Run();
