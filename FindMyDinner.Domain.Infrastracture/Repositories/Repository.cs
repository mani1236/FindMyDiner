using FindMydinner.Domain.Model.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FindMydinner.Domain.Model.Entities;

namespace FindMyDinner.Domain.Infrastracture.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> Table;
        private readonly Func<CancellationToken, Task<int>> _saveAsync;

        public Repository(FindMyDinnerContext dbContext)
        {
            Table = dbContext.Set<T>();
            _saveAsync = dbContext.SaveChangesAsync;
        }
        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Table
               .AddAsync(entity, cancellationToken)
               ;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            Table.Remove(entity);
            await SaveAsync(cancellationToken);
        }

        public async Task DeleteByKeyAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByKeyAsync(id, cancellationToken);

            if (entity != null)
            {
                Table.Remove(entity);
                await SaveAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await Table
            .ToListAsync(cancellationToken)
            ;
        }

        public async Task<T> GetByKeyAsync(object id, CancellationToken cancellationToken = default)
        {
            return await Table
              .FindAsync(id)
              ;
        }

      

        public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        {
            var result = await _saveAsync(cancellationToken)
                ;
            return result == 0;
        }
    }
}
