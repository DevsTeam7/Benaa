using Benaa.Infrastructure.Data;
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
        public Task Execute(IJobExecutionContext context)
        {
            //_dbContext.Sceduales.;
            _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
            return Task.CompletedTask;
        }
    }
}
