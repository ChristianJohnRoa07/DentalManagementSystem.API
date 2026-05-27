using DentalManagementSystem.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Contracts.Persistence.Common
{
    public interface IBaseRepository<T> where T : BaseDomainEntity
    {
        Task<T> GetById(Guid Id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);

    }
}
