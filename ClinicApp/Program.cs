using ClinicApp.Data;
using ClinicApp.Infrastructure;
using ClinicApp.Infrastructure.Configuration;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.Schedule;
using ClinicApp.Services.User;
using ClinicApp.Services.Visit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServerSideBlazor();

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

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
    options.AccessDeniedPath = "/Error/403";
});

// Add repositories
builder.Services.AddScoped<ISpecialityRepository, SpecialityDbRepository>();
builder.Services.AddScoped<IScheduleEntryRepository, ScheduleEntryDbRepository>();
builder.Services.AddScoped<IVisitRepository, VisitDbRepository>();
builder.Services.AddScoped<IVisitStatusRepository, VisitStatusDbRepository>();

// Add infrastructural services
builder.Services.AddScoped<IUserDependenciesProvider, UserDependenciesProvider>();
builder.Services.AddScoped<IClinicConfigurationBuilder, ClinicConfigurationBuilder>();

// Add services
builder.Services.AddScoped<IIdentityAuthenticationService, IdentityAuthenticationService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IVisitsReportService, VisitsReportService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "ManageArea",
    areaName: Constants.Areas.ManageAreaName,
    pattern: Constants.Areas.ManageAreaName + "/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/app/{*catchall}", "/Index");

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    var config = serviceProvider.GetRequiredService<IClinicConfigurationBuilder>()
        .BuildManagerCredentials()
        .Build();

    if(context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();

    DataFactory.PopulateSpecialities(serviceProvider.GetRequiredService<ISpecialityRepository>());
    await DataFactory.PopulateVisitsStatus(serviceProvider.GetRequiredService<IVisitStatusRepository>());

    await DataFactory.EnsureRoles(serviceProvider.GetRequiredService<RoleManager<IdentityRole>>());
    await DataFactory.CreateManagerAccount(
        serviceProvider.GetRequiredService<IRegistrationService>(), config.ManagerUserName,
        config.ManagerPassword);
}

app.Run();
