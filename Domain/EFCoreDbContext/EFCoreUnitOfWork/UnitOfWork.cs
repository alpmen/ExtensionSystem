using Domain.EFCoreDbContext;

namespace ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExpenseSystemDbContext _context;

        public UnitOfWork(ExpenseSystemDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}