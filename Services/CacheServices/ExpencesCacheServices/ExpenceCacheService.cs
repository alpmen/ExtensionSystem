using AutoMapper;
using Core.CacheServices;
using Data.ExpenseRepositories;
using Services.Services.ExpenseServices.Dtos.ResultDtos;

namespace Services.CacheServices.ExpencesCacheServices
{
    public class ExpenceCacheService : IExpenceCacheService
    {
        private readonly ICacheService _cacheManager;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public ExpenceCacheService(ICacheService cacheManager, IExpenseRepository expenseRepository, IMapper mapper)
        {
            _cacheManager = cacheManager;
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<List<ExpenseListAllResult>> GetExpenseList()
        {
            var key = String.Format("{0}_{1}", "General", "Expenses");

            List<ExpenseListAllResult> expenses = new List<ExpenseListAllResult>();

            List<ExpenseListAllResult> cachedData = _cacheManager.Get<List<ExpenseListAllResult>>(key);

            if (cachedData != null && cachedData.Any())
            {
                expenses = cachedData;
            }
            else
            {
                expenses = _mapper.Map<List<ExpenseListAllResult>>(await _expenseRepository.FindListAsync(x => x.Status == true));

                _cacheManager.Set(key, expenses);
            }

            return expenses;
        }

        public async Task Remove()
        {
            string cacheKey = String.Format("{0}_{1}", "General", "Expenses");

            try
            {
                _cacheManager.Clear(cacheKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Error");
            }
        }

    }
}