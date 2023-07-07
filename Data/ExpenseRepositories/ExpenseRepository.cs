using Domain.EFCoreDbContext;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreRepository;
using Microsoft.EntityFrameworkCore;

namespace Data.ExpenseRepositories
{
    public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseSystemDbContext context) : base(context)
        {
        }
    }
}
