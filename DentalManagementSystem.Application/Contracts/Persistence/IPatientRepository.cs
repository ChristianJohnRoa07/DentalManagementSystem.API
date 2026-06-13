using DentalManagementSystem.Application.Contracts.Persistence.Common;
using DentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Contracts.Persistence
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task<Patient?> GetPatientWithImages(Guid id);
    }
}
