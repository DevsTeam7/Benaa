using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetReport();
        Task<Report> Report(ReportDto report);

    }
}
