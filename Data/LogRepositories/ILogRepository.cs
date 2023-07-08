using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreRepository;

namespace Data.LogRepositories
{
    public interface ILogRepository : IRepositoryBase<SystemLog>
    {
    }
}
