using System.Threading.Tasks;

namespace ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}