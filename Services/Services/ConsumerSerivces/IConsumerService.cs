using Services.Services.ConsumerSerivces.Dtos.RequestDtos;
using Services.Services.ConsumerSerivces.Dtos.ResultDtos;

namespace Services.Services.ConsumerSerivces
{
    public interface IConsumerService
    {
        Task<List<ConsumerListAllResult>> ListAll();
        Task<List<ConsumerGetByIdResult>> GetById(int id);
        Task DeleteById(int id);
        Task UpdateById(int id, ConsumerUpdateByIdRequest request);
        Task<int> Insert(ConsumerInsertRequest request);
    }
}