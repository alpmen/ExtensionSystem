using AutoMapper;
using Data.ConsumerRepositories;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork;
using Services.CacheServices.ConsumerCacheServices;
using Services.Services.ConsumerExpenseServices.Dtos.RequestDtos;
using Services.Services.ConsumerExpenseServices.Dtos.ResultDtos;
using Services.Services.ConsumerSerivces.Dtos.RequestDtos;
using Services.Services.ConsumerSerivces.Dtos.ResultDtos;
using Services.Services.LogServices;

namespace Services.Services.ConsumerSerivces
{
    public class ConsumerService : IConsumerService
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConsumerCacheServicecs _consumerCacheServicecs;
        private readonly ILogService _logService;

        public ConsumerService(IConsumerRepository consumerRepository, IMapper mapper, IUnitOfWork unitOfWork, IConsumerCacheServicecs consumerCacheServicecs, ILogService logService)
        {
            _consumerRepository = consumerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _consumerCacheServicecs = consumerCacheServicecs;
            _logService = logService;
        }

        public async Task<List<ConsumerListAllResult>> ListAll()
        {
            var result = await _consumerCacheServicecs.GetConsumerList();

            return result;
        }

        public async Task<List<ConsumerGetByIdResult>> GetById(int id)
        {
            var result = await _consumerRepository.FindListAsync(x => x.Id == id);

            return _mapper.Map<List<ConsumerGetByIdResult>>(result);
        }

        public async Task DeleteById(int id)
        {
            Consumer entity = await _consumerRepository.FindAsync(x => x.Id == id);

            if (entity == null)
            {
                await _logService.AddLog("ConsumerExpences", "Data not found");
                throw new Exception("Data not found");
            }

            entity.Status = false;

            _consumerRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data delete error!");

            await _consumerCacheServicecs.Remove();
        }

        public async Task UpdateById(int id, ConsumerUpdateByIdRequest request)
        {
            Consumer entity = await _consumerRepository.FindAsync(x => x.Id == id);

            if (entity == null)
            {
                await _logService.AddLog("ConsumerExpences", "Data not found");
                throw new Exception("Data not found");
            }

            entity.Name = request.Name;
            entity.Password = request.Password;
            entity.Status = request.Status;

            _consumerRepository.Update(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data update error!");

            await _consumerCacheServicecs.Remove();
        }

        public async Task<int> Insert(ConsumerInsertRequest request)
        {
            Consumer entity = new Consumer
            {
                Status = true,
                Name = request.Name,
                Password = request.Password
            };

            _consumerRepository.Add(entity);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
                throw new Exception("Data insert error!");

            await _consumerCacheServicecs.Remove();

            return entity.Id;
        }

        public async Task<bool> GetByNameandPassword(string email, string password)
        {
            var result = await _consumerRepository.FindAsync(x => x.Name == email && x.Password == password);

            if (result != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}