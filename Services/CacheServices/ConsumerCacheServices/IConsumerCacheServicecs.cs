using Services.Services.ConsumerSerivces.Dtos.ResultDtos;

namespace Services.CacheServices.ConsumerCacheServices
{
    public interface IConsumerCacheServicecs
    {
        Task<List<ConsumerListAllResult>> GetConsumerList();
        Task Remove();
    }
}