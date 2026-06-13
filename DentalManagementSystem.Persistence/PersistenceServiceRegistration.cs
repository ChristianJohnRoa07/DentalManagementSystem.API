using DentalManagementSystem.Application.Contracts.Persistence;
using DentalManagementSystem.Application.Contracts.Persistence.Common;
using DentalManagementSystem.Persistence.DatabaseContext;
using DentalManagementSystem.Persistence.Repositories;
using DentalManagementSystem.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Persistence
{
    public static partial class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DentalManagementSystemDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DentalManagementApplicationConnection")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientImageRepository, PatientImageRepository>();
            services.AddScoped<IProcedureRepository, ProcedureRepository>();

            return services;
        }
    }
}
