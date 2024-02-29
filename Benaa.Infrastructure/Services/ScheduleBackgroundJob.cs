using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Benaa.Infrastructure.Services
{
    [DisallowConcurrentExecution]
    public class ScheduleBackgroundJob : IJob
    {
        private readonly ILogger<ScheduleBackgroundJob> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ScheduleBackgroundJob(ILogger<ScheduleBackgroundJob> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int currentTime = DateTime.Now.Hour;
            int nextHour = currentTime == 23 ? 0 : currentTime + 1;
            DateTime currentDay = DateTime.Today;

             List<Sceduale> sceduales = await _dbContext.Sceduales
                .Where(sceduale =>
                sceduale.TimeStart == currentTime 
                && sceduale.TimeEnd == nextHour 
                && sceduale.Date == currentDay).ToListAsync();

            _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
        }
    }
}
