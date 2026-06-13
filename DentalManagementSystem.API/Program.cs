using DentalManagementSystem.API.Middlewares;
using DentalManagementSystem.Application;
using DentalManagementSystem.Identity;
using DentalManagementSystem.Identity.DbContext;
using DentalManagementSystem.Persistence;
using DentalManagementSystem.Infrastructure;
using DentalManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.EnableAnnotations();
    cfg.SwaggerDoc("v1", new()
    {
        Title = "Dental Management System API",
        Version = "v1",
    });

    cfg.MapType<System.Guid>(() => new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string", Format = "uuid" });

    cfg.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token.\n\nExample: Bearer ey12345abcdef"
    });
    cfg.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DentalManagementSystem.API v1");
    });
}

app.UseAuthentication();
app.UseMiddleware<BlacklistTokenMiddleware>();
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
