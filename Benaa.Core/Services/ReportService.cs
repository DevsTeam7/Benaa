using Benaa.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Core.Entities.General;
using Benaa.Core.Entities.DTOs;

namespace Benaa.Core.Services
{
    public class ReportService: IReportService
    {
        private readonly IReportRepository _reoprtRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reoprtRepository, IMapper mapper)
        {
            _reoprtRepository = reoprtRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Report>> GetReport()
        {
            return await _reoprtRepository.GetAll();
        }

        public async Task<Report> Report(ReportDto report)
        {
            var Report = _mapper.Map<Report>(report);

            await _reoprtRepository.Create(Report);
            return Report;
        }

    }

}
