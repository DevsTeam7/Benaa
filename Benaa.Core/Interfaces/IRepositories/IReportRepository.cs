using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;

namespace Benaa.Core.Interfaces.IRepositories
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        new Task<List<ReportDisplyDto>> GetAll();
    }
}
