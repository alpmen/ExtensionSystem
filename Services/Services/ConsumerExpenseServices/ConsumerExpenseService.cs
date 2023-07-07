using AutoMapper;
using Data.ConsumerExpenseRepositories;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork;
using Services.Services.ConsumerExpenseServices.Dtos.RequestDtos;
using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;

namespace Services.Services.ConsumerExpenseServices
{
    public class ConsumerExpenseService : IconsumerExpenceService
    {
        private readonly IConsumerExpenseRepository _consumerExpenseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ConsumerExpenseService(IConsumerExpenseRepository consumerExpenseRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _consumerExpenseRepository = consumerExpenseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ConsumerExpenseListAllResult>> ListAll()
        {
            var result = await _consumerExpenseRepository.FindListAsync(x => x.Status == true);

            return _mapper.Map<List<ConsumerExpenseListAllResult>>(result);
        }

        public async Task<List<ConsumerExpenseGetByIdResult>> GetById(int id)
        {
            var result = await _consumerExpenseRepository.FindListAsync(x => x.Id == id);

            return _mapper.Map<List<ConsumerExpenseGetByIdResult>>(result);
        }

        public async Task DeleteById(int id)
        {
            ConsumerExpense entity = await _consumerExpenseRepository.FindAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Data not found");

            entity.Status = false;

            _consumerExpenseRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data delete error!");
        }

        public async Task UpdateById(int id, ConsumerExpenseUpdateByIdRequest request)
        {
            ConsumerExpense entity = await _consumerExpenseRepository.FindAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Data not found");

            entity.ExpenseId = request.ExpenseId;
            entity.ConsumerId = request.ConsumerId;
            entity.Date = request.Date;
            entity.Cost = request.Cost;
            entity.Status = request.Status;

            _consumerExpenseRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data update error!");
        }

        public async Task<int> Insert(ConsumerExpenseInsertRequest request)
        {
            ConsumerExpense entity = new ConsumerExpense
            {
                Status = true,
                ConsumerId = request.ConsumerId,
                Cost = request.Cost,
                Date = DateTime.Now,
                ExpenseId = request.ExpenseId
            };

            _consumerExpenseRepository.Add(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data insert error!");

            return entity.Id;
        }

        public async Task<int> TotalCostByConsumerId(int consumerId)
        {
            var costs = (await _consumerExpenseRepository.FindListAsync(x => x.ConsumerId == consumerId)).Select(x => x.Cost).ToList();

            var totalCost = 0;

            foreach (var cost in costs)
            {
                totalCost += cost;
            }

            return totalCost;
        }

    }
}