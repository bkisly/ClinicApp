using ClinicApp.Data;
using ClinicApp.Infrastructure;
using ClinicApp.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISpecialityRepository, SpecialityDbRepository>();

var app = builder.Build();

app.MapDefaultControllerRoute();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if(context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();

    DataInitializer.PopulateSpecialities(scope.ServiceProvider.GetRequiredService<ISpecialityRepository>());
}

app.Run();
