using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDisplyDto>> GetReport();
        Task<ErrorOr<Report>> Report(ReportDto report, string userId);
        Task<ErrorOr<Success>> Delete(Guid reportId);
        Task DeleteAll(Guid TargetId);
    }
}
