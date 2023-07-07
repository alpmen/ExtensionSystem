using Domain.EFCoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace ExtensionSystem.Workers
{
    public class ExpenseWorker : BackgroundService
    {
        private readonly ExpenseSystemDbContext DbContext;
        private readonly string FilePath;
        private readonly ILogger<ExpenseWorker> Logger;
        private readonly TimeSpan DailyReportTime;

        public ExpenseWorker(ExpenseSystemDbContext dbContext, ILogger<ExpenseWorker> logger)
        {
            DbContext = dbContext;
            FilePath = "C:\\Users\\OrhunAlpYamen\\Desktop\\ExpenseData.txt";
            Logger = logger;
            DailyReportTime = new TimeSpan(17, 35, 0);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;

                if (now.TimeOfDay == DailyReportTime)
                {
                    await GenerateAndSaveDailyReports();
                }

                if (now.DayOfWeek == DayOfWeek.Monday && now.TimeOfDay == DailyReportTime)
                {
                    await GenerateAndSaveWeeklyReports();
                }

                if (now.Day == 1 && now.TimeOfDay == DailyReportTime)
                {
                    await GenerateAndSaveMonthlyReports();
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private async Task GenerateAndSaveDailyReports()
        {
            try
            {
                var users = await DbContext.Consumers.ToListAsync();

                foreach (var user in users)
                {
                    decimal totalCost = await DbContext.ConsumerExpenses
                        .Where(e => e.Date.Date == DateTime.Now.Date && e.ConsumerId == user.Id)
                        .SumAsync(e => e.Cost);

                    string report = $"Kullanıcı: {user.Name}, Günlük toplam cost: {totalCost}";
                    SaveTextToFile(report);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Hata oluştu");
            }
        }

        private async Task GenerateAndSaveWeeklyReports()
        {
            try
            {
                var users = await DbContext.Consumers.ToListAsync();

                foreach (var user in users)
                {
                    DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(6);

                    decimal totalCost = await DbContext.ConsumerExpenses
                        .Where(e => e.Date.Date >= startOfWeek.Date && e.Date.Date <= endOfWeek.Date && e.ConsumerId == user.Id)
                        .SumAsync(e => e.Cost);

                    string report = $"Kullanıcı: {user.Name}, Haftalık toplam cost: {totalCost}";
                    SaveTextToFile(report);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Hata oluştu");
            }
        }

        private async Task GenerateAndSaveMonthlyReports()
        {
            try
            {
                var users = await DbContext.Consumers.ToListAsync();

                foreach (var user in users)
                {
                    DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                    decimal totalCost = await DbContext.ConsumerExpenses
                        .Where(e => e.Date.Date >= startOfMonth.Date && e.Date.Date <= endOfMonth.Date && e.ConsumerId == user.Id)
                        .SumAsync(e => e.Cost);

                    string report = $"Kullanıcı: {user.Name}, Aylık toplam cost: {totalCost}";
                    SaveTextToFile(report);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Hata oluştu");
            }
        }

        private void SaveTextToFile(string text)
        {
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine(text);
            }
        }
    }
}