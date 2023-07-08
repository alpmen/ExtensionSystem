using Domain.Entities;
using Services.Services.ConsumerExpenseServices.Dtos.RequestDtos;
using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;

namespace Services.Services.ConsumerExpenseServices
{
    public interface IconsumerExpenceService
    {
        Task<List<ConsumerExpenseListAllResult>> ListAll();
        Task<List<ConsumerExpenseGetByIdResult>> GetById(int id);
        Task DeleteById(int id);
        Task UpdateById(int id, ConsumerExpenseUpdateByIdRequest request);
        Task<int> Insert(ConsumerExpenseInsertRequest request);
        Task<int> TotalCostByConsumerId(int consumerId);
        Task<List<AllTotalDataModel>> AllTotalCost();
    }
}
