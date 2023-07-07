using Domain.EFCoreDbContext;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.ConsumerExpenseRepositories
{
    public class ConsumerExpenseRepository : RepositoryBase<ConsumerExpense>, IConsumerExpenseRepository
    {
        public ConsumerExpenseRepository(ExpenseSystemDbContext context) : base(context)
        {
        }
    }
}
