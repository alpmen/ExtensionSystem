using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExtraZone.Data.Domain.EfDbContext.EfCoreRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        void Add(TEntity entity);

        void Delete(TEntity entity);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression);

        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<List<TEntity>> GetAllAsync();

        void Update(TEntity entity);

        void Update(TEntity entity, params string[] propertyNames);
    }
}