using AutoMapper;
using Core.CacheServices;
using Data.ConsumerExpenseRepositories;
using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;

namespace Services.CacheServices.ConsumerExpenceCacheServices
{
    public class ConsumerExpenceCacheService : IConsumerExpenceCacheService
    {
        private readonly ICacheService _cacheManager;
        private readonly IConsumerExpenseRepository _consumerExpenseRepository;
        private readonly IMapper _mapper;

        public ConsumerExpenceCacheService(ICacheService cacheManager, IConsumerExpenseRepository consumerExpenseRepository, IMapper mapper)
        {
            _cacheManager = cacheManager;
            _consumerExpenseRepository = consumerExpenseRepository;
            _mapper = mapper;
        }

        public async Task<List<ConsumerExpenseListAllResult>> GetConsumerExpenceList()
        {
            var key = String.Format("{0}_{1}", "General", "ConsumerExpences");

            List<ConsumerExpenseListAllResult> consumerexpenses = new List<ConsumerExpenseListAllResult>();

            List<ConsumerExpenseListAllResult> cachedData = _cacheManager.Get<List<ConsumerExpenseListAllResult>>(key);

            if (cachedData != null && cachedData.Any())
            {
                consumerexpenses = cachedData;
            }
            else
            {
                consumerexpenses = _mapper.Map<List<ConsumerExpenseListAllResult>>(await _consumerExpenseRepository.FindListAsync(x => x.Status == true));

                _cacheManager.Set(key, consumerexpenses);
            }

            return consumerexpenses;
        }

        public async Task Remove()
        {
            string cacheKey = String.Format("{0}_{1}", "General", "ConsumerExpences");

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