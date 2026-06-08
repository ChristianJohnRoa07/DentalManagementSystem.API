using DentalManagementSystem.API.Middlewares;
using DentalManagementSystem.Application;
using DentalManagementSystem.Identity;
using DentalManagementSystem.Identity.DbContext;
using DentalManagementSystem.Persistence;
using DentalManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Automatically apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Applying Dental Management System migrations...");
        var context = services.GetRequiredService<DentalManagementSystemDbContext>();
        await context.Database.MigrateAsync();

        logger.LogInformation("Applying Identity Security database migrations...");
        var identityDbContext = services.GetRequiredService<ApplicationUserDbContext>();
        await identityDbContext.Database.MigrateAsync();

        logger.LogInformation("Database initial migration routines completed successfully.");
    }
    catch (Exception ex)
    {
        logger.LogCritical(ex, "An execution failure occurred while applying target database migrations.");
    }
}

app.Run();
