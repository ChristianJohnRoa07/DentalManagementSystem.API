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

        Task<T> GetAll();

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);

    }
}
