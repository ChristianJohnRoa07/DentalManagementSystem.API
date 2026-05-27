using DentalManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Persistence
{
    public static partial class PersistenceServiceRegistration
    {
        public class DbFactory : IDesignTimeDbContextFactory<DentalManagementSystemDbContext>
        {
            public DentalManagementSystemDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<DentalManagementSystemDbContext>();
                var connectionString = configuration.GetConnectionString("DentalManagementApplicationConnection");

                builder.UseSqlServer(connectionString);

                return new DentalManagementSystemDbContext(builder.Options);
            }
        }
    }
    
}
