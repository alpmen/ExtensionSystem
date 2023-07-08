using AutoMapper;
using Core.CacheServices;
using Data.ConsumerRepositories;
using Data.ExpenseRepositories;
using Services.Services.ConsumerSerivces.Dtos.ResultDtos;
using Services.Services.ExpenseServices.Dtos.ResultDtos;

namespace Services.CacheServices.ConsumerCacheServices
{
    public class ConsumerCacheService : IConsumerCacheServicecs
    {
        private readonly ICacheService _cacheManager;
        private readonly IConsumerRepository _consumerRepository;
        private readonly IMapper _mapper;

        public ConsumerCacheService(ICacheService cacheManager, IConsumerRepository consumerRepository, IMapper mapper)
        {
            _cacheManager = cacheManager;
            _consumerRepository = consumerRepository;
            _mapper = mapper;
        }

        public async Task<List<ConsumerListAllResult>> GetConsumerList()
        {
            var key = String.Format("{0}_{1}", "General", "Consumers");

            List<ConsumerListAllResult> consumers = new List<ConsumerListAllResult>();

            List<ConsumerListAllResult> cachedData = _cacheManager.Get<List<ConsumerListAllResult>>(key);

            if (cachedData != null && cachedData.Any())
            {
                consumers = cachedData;
            }
            else
            {
                consumers = _mapper.Map<List<ConsumerListAllResult>>(await _consumerRepository.FindListAsync(x => x.Status == true));

                _cacheManager.Set(key, consumers);
            }

            return consumers;
        }

        public async Task Remove()
        {
            string cacheKey = String.Format("{0}_{1}", "General", "Consumers");

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