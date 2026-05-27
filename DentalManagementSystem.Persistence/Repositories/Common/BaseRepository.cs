using DentalManagementSystem.Application.Contracts.Persistence.Common;
using DentalManagementSystem.Domain.Entities.Common;
using DentalManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Persistence.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseDomainEntity
    {
        private readonly DentalManagementSystemDbContext _dbContext;
        
        public BaseRepository(DentalManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
