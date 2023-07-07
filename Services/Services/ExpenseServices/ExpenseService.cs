using AutoMapper;
using Data.ExpenseRepositories;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork;
using Services.Services.ExpenseServices.Dtos.RequestDtos;
using Services.Services.ExpenseServices.Dtos.ResultDtos;

namespace Services.Services.ExpenseServices
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ExpenseListAllResult>> ListAll()
        {
            var result = await _expenseRepository.FindListAsync(x => x.Status == true);

            return _mapper.Map<List<ExpenseListAllResult>>(result);
        }

        public async Task<List<ExpenseGetByIdResult>> GetById(int id)
        {
            var result = await _expenseRepository.FindListAsync(x => x.Id == id);

            return _mapper.Map<List<ExpenseGetByIdResult>>(result);
        }

        public async Task DeleteById(int id)
        {
            Expense entity = await _expenseRepository.FindAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Data not found");

            entity.Status = false;

            _expenseRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data delete error!");
        }

        public async Task UpdateById(int id, ExpenceUpdateByIdRequest request)
        {
            Expense entity = await _expenseRepository.FindAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Data not found");

            entity.Name = request.Name;
            entity.Status = request.Status;

            _expenseRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data update error!");
        }

        public async Task<int> Insert(ExpenceInsertRequest request)
        {
            Expense entity = new Expense()
            {
                Status = true,
                Name = request.Name,
            };

            _expenseRepository.Add(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data insert error!");

            return entity.Id;
        }
    }
}