using Domain.EFCoreDbContext;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.ConsumerRepositories
{
    public class ConsumerRepository : RepositoryBase<Consumer>, IConsumerRepository
    {
        public ConsumerRepository(ExpenseSystemDbContext context) : base(context)
        {
        }
    }
}