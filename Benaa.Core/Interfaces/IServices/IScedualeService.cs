using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{

    public interface IScedualeService
    {
        Task<ErrorOr<List<Sceduale>>> AddSceduales(List<SchedualDto> sceduales, string teacherId);
        Task<ErrorOr<Sceduale>> UpdateSceduale(SchedualDetailsDto sceduale);
        Task<ErrorOr<Object>> Delete(Guid id);
        Task<ErrorOr<Sceduale>> GetById(Guid id);
        Task<ErrorOr<List<TimeRangeDto>>> GetByDay(int day, string userId);
        Task<ErrorOr<Object>> BookAppointment(SchedualDetailsDto schedualDetails, string userId);
        Task<ErrorOr<Object>> RepeatSceduale(string userId);
        Task<ErrorOr<IEnumerable<Sceduale>>> GetSceduales();
        Task<ErrorOr<List<Sceduale>>> GetTeacherSceduales(string userId);

	}

}
