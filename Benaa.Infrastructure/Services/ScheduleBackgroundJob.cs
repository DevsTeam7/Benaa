using Benaa.Core.Entities.General;
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
          //List<Sceduale> sceduales = _dbContext.Sceduales.ToList();
          //  for (int i = 0; i < sceduales.Count(); i++)
          //  {
          //      if (sceduales[i].TimeStart == DateTime.Now)
          //      {

          //      }
          //  }
            _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
            return Task.CompletedTask;
        }
    }
}
