using Services.Services.ExpenseServices.Dtos.ResultDtos;

namespace Services.CacheServices.ExpencesCacheServices
{
    public interface IExpenceCacheService
    {
        Task<List<ExpenseListAllResult>> GetExpenseList();
        Task Remove();
    }
}