using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Interfaces.IServices;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Benaa.Core.Services
{
    public class ScedualeService : IScedualeService
    {
        private readonly ISchedualeRepository _schedualeRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        private readonly IWalletService _walletService;
        private readonly IChatHubService _chatHubService;
        private readonly IPaymentRepositoty _paymentRepositoty;


        public ScedualeService(ISchedualeRepository SchedualRepository,
            INotificationService notificationService, IUserRepository userRepository,
            IWalletService walletService, IChatHubService chatHubService, IPaymentRepositoty paymentRepositoty)
        {
            _schedualeRepository = SchedualRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
            _walletService = walletService;
            _chatHubService = chatHubService;
            _paymentRepositoty = paymentRepositoty;
        }

        public async Task<ErrorOr<IEnumerable<Sceduale>>> GetSceduales()
        {
            var sceduales = await _schedualeRepository.GetAll();
            if (sceduales == null) { return Error.Failure(); }
            return sceduales.ToList();
        }
        public async Task<ErrorOr<List<Sceduale>>> AddSceduales(List<SchedualDto> sceduales, string teacherId)
        {
            List<Sceduale> Sceduales = new List<Sceduale>();
            foreach (var schedual in sceduales)
            {
                var scheduale = new Sceduale
                {
                    Date = schedual.Date,
                    TimeStart = schedual.TimeStart,
                    TimeEnd = schedual.TimeEnd,
                    Price = schedual.Price,
                    TeacherId = teacherId,
                    Status = ScedualeStatus.Still
                };
                var sceduale = await _schedualeRepository.Create(scheduale);
                if (sceduale == null) { return Error.Failure(); }
                Sceduales.Add(sceduale);
            }
            return Sceduales;

        }
        public async Task<ErrorOr<Sceduale>> GetById(Guid id)
        {
            var sceduale = await _schedualeRepository.GetById(id);
            if (sceduale == null) { return Error.NotFound(); }
            return sceduale;
        }
        public async Task<ErrorOr<Object>> Delete(Guid id)
        {
            var schedule = await _schedualeRepository.GetById(id);
            if (schedule == null) { return Error.NotFound("schedule is not found"); }

            var scheduledDay = schedule.Date;
            DateTime currentDate = DateTime.Now.Date;

            if (scheduledDay < currentDate) { return Error.Unexpected("day has already passed"); }

            if (scheduledDay.Date == currentDate.AddDays(-1).Date)
            {
                if (scheduledDay.Month == currentDate.Month && scheduledDay.Year == currentDate.Year)
                {
                    var payment = await _paymentRepositoty.SelectOneItem(payment => payment.ItemId == schedule.Id);
                    if (payment == null) { return Error.NotFound(); }
                    await _paymentRepositoty.Delete(payment);
                    await _schedualeRepository.Delete(schedule);
                    await _walletService.RefundUser(schedule.Price, schedule.StudentId!);
                    await _notificationService.Send(schedule.StudentId!, "لقد تم الغاء موعدك بنجاح");
                    await _notificationService.Send(schedule.TeacherId, "لقد تم الغاء");
                    return new Success();
                }
            }
            return Error.Conflict("You can't cansle the appointment in the same day");
        }
        public async Task<ErrorOr<Sceduale>> UpdateSceduale(SchedualDetailsDto sceduale)
        {
            var scedualeToUpdate = await _schedualeRepository.GetById(sceduale.Id);
            if (scedualeToUpdate == null) { return Error.NotFound(); }

            scedualeToUpdate.TeacherId = sceduale.TeacherId;
            scedualeToUpdate.Date = sceduale.Date;
            scedualeToUpdate.TimeStart = sceduale.TimeStart;
            scedualeToUpdate.TimeEnd = sceduale.TimeEnd;

            await _schedualeRepository.Update(scedualeToUpdate);
            return scedualeToUpdate;
        }
        public async Task<ErrorOr<List<TimeRangeDto>>> GetByDay(int day, string userId)
        {
            var scheduals = await _schedualeRepository.GetAll();
            var dates = await _schedualeRepository.SelectTimes(x => (x.Date.Day == day) && (x.TeacherId == userId));
            if (scheduals.ToList().Count == 0 || dates.Count == 0) { return Error.NotFound(); }
            return dates;
        }
        public async Task<ErrorOr<Object>> BookAppointment(SchedualDetailsDto schedualDetails, string userId)
        {
            var Isnull = await _schedualeRepository.CheckAvailability(schedualDetails);
            if (Isnull)
            {
                string type = "schedual";
                var wallet = await _userRepository.GetUserWallet(userId);

                if (wallet.Amount >= schedualDetails.Price)
                {
                    var sceduale = await _schedualeRepository.GetById(schedualDetails.Id);
                    sceduale.TeacherId = schedualDetails.TeacherId;
                    sceduale.StudentId = userId;
                    sceduale.Price = schedualDetails.Price;
                    sceduale.Date = schedualDetails.Date;
                    sceduale.TimeStart = schedualDetails.TimeStart;
                    sceduale.TimeEnd = schedualDetails.TimeEnd;

                    await _schedualeRepository.Update(sceduale);
                    var payment = await _walletService.SetPayment(schedualDetails.Id, type, schedualDetails.Price, userId);
                    await _chatHubService.CreateChat(userId, sceduale.TeacherId, sceduale.Id);
                    //TODO : send sceduale info with the notification
                    //await _notificationService.Send(sceduale.StudentId, "تم حجز موعدك بنجاح");
                    //await _notificationService.Send(sceduale.TeacherId, "تم حجز موعد جديد");
                    return payment;
                }
                return Error.Failure("no money in wallet");
            }
            return Error.Failure("Is Booked");
        }
        public async Task<ErrorOr<Object>> RepeatSceduale(string userId)
        {
            DateTime currentDate = DateTime.Now;
            DateTime startDate = currentDate.AddDays(-7);

            for (int i = 1; i <= 7; i++)
            {
                var scheduales = await _schedualeRepository.Select(x => (x.Date == startDate.AddDays(+i)) && (x.TeacherId == userId));
                if (scheduales == null) { return Error.NotFound(); }
                foreach (var scheduale in scheduales)
                {
                    var newScheduale = new Sceduale
                    {
                        Date = currentDate.AddDays(+i),
                        TimeStart = scheduale.TimeStart,
                        TimeEnd = scheduale.TimeEnd,
                        Price = scheduale.Price,
                        TeacherId = scheduale.TeacherId,
                        StudentId = scheduale.StudentId
                    };
                    var createdscheduale = await _schedualeRepository.Create(scheduale);
                    if (createdscheduale == null) { return Error.Failure(); }
                    return scheduales;
                }
            }
            return new Success();
        }
		public async Task<ErrorOr<List<Sceduale>>> GetTeacherSceduales(string userId)
		{
            var sceduales =  await _schedualeRepository.Select(scheduale => scheduale.TeacherId == userId && scheduale.StudentId == null);
            if(sceduales == null) { return Error.NotFound(); }
            return sceduales;

		}
	}
}
