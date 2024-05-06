using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Benaa.Infrastructure.Services
{
    [DisallowConcurrentExecution]
    public class ScheduleBackgroundJob : IJob
    {
        private readonly ILogger<ScheduleBackgroundJob> _logger;
        private readonly ISchedualeRepository _schedualeRepository;
        private readonly INotificationService _notificationService;
        private readonly IChatHubService _chatHubService;

        public ScheduleBackgroundJob(ILogger<ScheduleBackgroundJob> logger, ISchedualeRepository schedualeRepository,
            INotificationService notificationService, IChatHubService chatHubService)
        {
            _logger = logger;
            _schedualeRepository = schedualeRepository;
            _notificationService = notificationService;
            _chatHubService = chatHubService;
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
            int currentHour = DateTime.Now.Hour;
            int currentMinute = DateTime.Now.Minute;
            int nextHour = currentHour == 23 ? 0 : currentHour + 1;

            DateTime currentDay = DateTime.Today;

            List<Sceduale> sceduales = await _schedualeRepository.SelectByDay(currentDay);


            foreach (var sceduale in sceduales)
            {
                string timeStart24Hour = ConvertTo24HourFormat(sceduale.TimeStart);
                string timeEnd24Hour = ConvertTo24HourFormat(sceduale.TimeEnd);

                string[] timeStartParts = timeStart24Hour.Split(':');
                string[] timeEndParts = timeEnd24Hour.Split(':');

                string startHour = timeStartParts[0];
                string startMinutes = timeStartParts[1];

                string endHour = timeEndParts[0];
                string endMinutes = timeEndParts[1];


                if (startHour == currentHour.ToString()
                    && startMinutes == currentMinute.ToString()
                    && endHour == nextHour.ToString()
                    && endMinutes == startMinutes)
                {

                    sceduale.Status = ScedualeStatus.Opened;

                    var chat = await _chatHubService.GetScheduledChat(sceduale.Id);

                    SchedualeNotificationDto notificationDto = new SchedualeNotificationDto
                    {
                        FullName = sceduale.Teacher.FirstName + " " + sceduale.Teacher.LastName,
                        Date = sceduale.Date,
                        Time = sceduale.TimeStart,
                        chatId = chat.Id.ToString()
                    };

                    await _notificationService.Send(sceduale.StudentId!, "موعدك قرب مع", notificationDto);
                    await _notificationService.Send(sceduale.TeacherId, "موعدك قرب مع", notificationDto);
                }
            }


            _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
        }
    }
}
