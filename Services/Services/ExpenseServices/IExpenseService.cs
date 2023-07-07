using Services.Services.ExpenseServices.Dtos.RequestDtos;
using Services.Services.ExpenseServices.Dtos.ResultDtos;

namespace Services.Services.ExpenseServices
{
    public interface IExpenseService
    {
        Task<List<ExpenseListAllResult>> ListAll();
        Task<List<ExpenseGetByIdResult>> GetById(int id);
        Task DeleteById(int id);
        Task UpdateById(int id, ExpenceUpdateByIdRequest request);
        Task<int> Insert(ExpenceInsertRequest request);
    }
}