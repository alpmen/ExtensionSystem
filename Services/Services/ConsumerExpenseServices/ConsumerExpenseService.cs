using AutoMapper;
using Data.ConsumerExpenseRepositories;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork;
using MyMessageApp.Core.Exceptions;
using Services.CacheServices.ConsumerExpenceCacheServices;
using Services.Services.ConsumerExpenseServices.Dtos.RequestDtos;
using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;
using Services.Services.ConsumerSerivces;
using Services.Services.LogServices;

namespace Services.Services.ConsumerExpenseServices
{
    public class ConsumerExpenseService : IconsumerExpenceService
    {
        private readonly IConsumerExpenseRepository _consumerExpenseRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConsumerExpenceCacheService _consumerExpenceCacheService;
        private readonly IConsumerService _consumerService;
        private readonly ILogService _logService;

        public ConsumerExpenseService(IConsumerExpenseRepository consumerExpenseRepository, IMapper mapper, IUnitOfWork unitOfWork, IConsumerExpenceCacheService consumerExpenceCacheService, IConsumerService consumerService, ILogService logService)
        {
            _consumerExpenseRepository = consumerExpenseRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _consumerExpenceCacheService = consumerExpenceCacheService;
            _consumerService = consumerService;
            _logService = logService;
        }

        public async Task<List<ConsumerExpenseListAllResult>> ListAll()
        {
            var result = await _consumerExpenceCacheService.GetConsumerExpenceList();

            return result;
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
            {
                await _logService.AddLog("ConsumerExpences", "Data not found");
                throw new Exception("Data not found");
            }

            entity.Status = false;

            _consumerExpenseRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data delete error!");

            await _consumerExpenceCacheService.Remove();
            await _consumerExpenceCacheService.RemoveByConsumerId(entity.ConsumerId);
        }

        public async Task UpdateById(int id, ConsumerExpenseUpdateByIdRequest request)
        {
            ConsumerExpense entity = await _consumerExpenseRepository.FindAsync(x => x.Id == id);

            if (entity == null)
            {
                await _logService.AddLog("ConsumerExpences", "Data not found");
                throw new Exception("Data not found");
            }

            var oldConsumerId = entity.ConsumerId;
            entity.ExpenseId = request.ExpenseId;
            entity.ConsumerId = request.ConsumerId;
            entity.Date = request.Date;
            entity.Cost = request.Cost;
            entity.Status = request.Status;

            _consumerExpenseRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data update error!");


            await _consumerExpenceCacheService.Remove();
            await _consumerExpenceCacheService.RemoveByConsumerId(entity.ConsumerId);
            await _consumerExpenceCacheService.RemoveByConsumerId(oldConsumerId);
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

            await _consumerExpenceCacheService.Remove();
            await _consumerExpenceCacheService.RemoveByConsumerId(entity.ConsumerId);

            return entity.Id;
        }

        public async Task<int> TotalCostByConsumerId(int consumerId)
        {
            var total = await _consumerExpenceCacheService.GetConsumerTotalCostByConsumerId(consumerId);

            return total;

            //var costs = (await _consumerExpenseRepository.FindListAsync(x => x.ConsumerId == consumerId)).Select(x => x.Cost).ToList();

            //var totalCost = 0;

            //foreach (var cost in costs)
            //{
            //    totalCost += cost;
            //}

            //return totalCost;
        }

        public async Task<List<AllTotalDataModel>> AllTotalCost()
        {
            var users = await _consumerService.ListAll();

            List<AllTotalDataModel> data = new List<AllTotalDataModel>();

            foreach (var user in users)
            {
                var costs = (await _consumerExpenseRepository.FindListAsync(x => x.ConsumerId == user.Id)).Select(x => x.Cost).ToList();

                var totalcost = 0;
                foreach (var cst in costs)
                {
                    totalcost += cst;
                }
                AllTotalDataModel model = new AllTotalDataModel
                {
                    TotalCost = totalcost,
                    UserName = user.Name
                };
                data.Add(model);
            }

            return data;
        }

    }
}