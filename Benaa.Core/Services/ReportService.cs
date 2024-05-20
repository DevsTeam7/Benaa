using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;
using ErrorOr;
using System.Runtime.InteropServices;
using AutoMapper.Configuration.Annotations;

namespace Benaa.Core.Services
{
    public class ReportService: IReportService
    {
        private readonly IReportRepository _reoprtRepository;


        public ReportService(IReportRepository reoprtRepository)
        {
            _reoprtRepository = reoprtRepository;
        }

        public async Task<IEnumerable<ReportDisplyDto>> GetReport()
        {
            return await _reoprtRepository.GetAll();
        }
        public async Task<ErrorOr<Success>> Delete(Guid reportId)
        {
            var report = await _reoprtRepository.GetById(reportId);
            if (report == null) { Error.NotFound(); }
            await _reoprtRepository.Delete(report);
            return new Success();
        }
        public async Task DeleteAll(Guid TargetId)
        {
          List<Report> reports = await _reoprtRepository.Select(report => report.TargetId ==  TargetId);
            foreach (Report report in reports)
            {
                await _reoprtRepository.Delete(report);
            }
        }

        public async Task<ErrorOr<Report>> Report(ReportDto reportDto, string userId)
        {
            var report = new Report();
            report.Problem = reportDto.Problem;
            report.Description = reportDto.Description;
            report.Type = reportDto.Type;
            report.UserId = userId;
            report.TargetId = Guid.Parse(reportDto.TargetId);
            var res =await _reoprtRepository.Create(report);
            if(res == null) { return Error.Failure(description:"Falid To Create The Report"); }
            return report;
        }

    }

}
