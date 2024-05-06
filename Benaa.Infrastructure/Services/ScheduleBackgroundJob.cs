using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
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
        private readonly ISchedualeRepository _schedualeRepository;

        public ScheduleBackgroundJob(ILogger<ScheduleBackgroundJob> logger, ISchedualeRepository schedualeRepository)
        {
            _logger = logger;
            _schedualeRepository = schedualeRepository;
        }
        static string ConvertTo24HourFormat(string timeString)
        {
            DateTime time = DateTime.ParseExact(timeString, "hh:mm:ss tt", null);
            string time24Hour = time.ToString("HH:mm:ss");

            if (timeString.EndsWith("PM") && time.Hour < 12)
            {
                time24Hour = time.AddHours(12).ToString("HH:mm:ss");
            }
            else if (timeString.EndsWith("AM") && time.Hour == 12)
            {
                time24Hour = time.AddHours(-12).ToString("HH:mm:ss");
            }

            return time24Hour;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int currentTime = DateTime.Now.Hour;

            int nextHour = currentTime == 23 ? 0 : currentTime + 1;
            DateTime currentDay = DateTime.Today;

            List<Sceduale> sceduales = await _schedualeRepository.Select(sceduale => sceduale.Date == currentDay);

            foreach (var sceduale in sceduales)
            {

               // sceduale.Status =  ScedualeStatus.Opened;
                //TODO: inject the Notfectionhubcontext and send the notfcation to the user and send the group neme to the user
                
            }
            

            _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
        }
    }
}
