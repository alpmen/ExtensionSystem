using Data.LogRepositories;
using Domain.Entities;
using ExtraZone.Data.Domain.EfDbContext.EfCoreUnitOfWork;

namespace Services.Services.LogServices
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogService(ILogRepository logRepository, IUnitOfWork unitOfWork)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddLog(string entity, string exception)
        {
            SystemLog log = new SystemLog()
            {
                Entity = entity,
                Exception = exception,
                Date = DateTime.Now
            };

            try
            {
                _logRepository.Add(log);

                int result = await _unitOfWork.SaveChangesAsync();

                if (result <= 0)
                    throw new Exception("Data insert error!");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}