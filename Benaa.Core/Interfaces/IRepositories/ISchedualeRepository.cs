using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using System.Linq.Expressions;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface ISchedualeRepository : IBaseRepository<Sceduale>
    {
        Task<List<TimeRangeDto>> SelectTimes(Expression<Func<Sceduale, bool>> predicate);
        Task<bool> CheckAvailability(SchedualDetailsDto schedualDetails);
        Task<List<Sceduale>> SelectByDay(DateTime date);
    }
}
