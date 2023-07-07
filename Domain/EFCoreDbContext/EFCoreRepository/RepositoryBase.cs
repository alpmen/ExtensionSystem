using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExtraZone.Data.Domain.EfDbContext.EfCoreRepository
{
    public class RepositoryBase<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _context;
        public readonly DbSet<TEntity> _collection;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _collection = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().Where(expression).SingleOrDefaultAsync();
        }

        public async Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await _context.Set<TEntity>().Where(expression)
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Update(TEntity entity, params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                _context.Set<TEntity>().Update(entity).Property(propertyName).IsModified = true;
            }
        }
    }
}