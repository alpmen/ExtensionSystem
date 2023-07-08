using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;

namespace Services.CacheServices.ConsumerExpenceCacheServices
{
    public interface IConsumerExpenceCacheService
    {
        Task<List<ConsumerExpenseListAllResult>> GetConsumerExpenceList();
        Task Remove();
    }
}