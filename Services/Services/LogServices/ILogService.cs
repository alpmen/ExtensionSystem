namespace Services.Services.LogServices
{
    public interface ILogService
    {
        Task AddLog(string entity, string exception);
    }
}