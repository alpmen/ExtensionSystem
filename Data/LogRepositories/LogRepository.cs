using Domain.EFCoreDbContext;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.LogRepositories
{
    public class LogRepository : RepositoryBase<SystemLog>, ILogRepository
    {
        public LogRepository(ExpenseSystemDbContext context) : base(context)
        {
        }
    }
}
