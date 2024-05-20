using AutoMapper;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IRepositories;
using Benaa.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Benaa.Infrastructure.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IChatRepository _chatRepository;
        private readonly UserManager<User> _userManager;    
        public ReportRepository(ApplicationDbContext dbContext, ICourseRepository courseRepository,
            IChatRepository chatRepository, UserManager<User> userManager) : base(dbContext)
        {
            _chatRepository = chatRepository;
            _courseRepository = courseRepository;
            _userManager = userManager;
        }
        public new async Task<List<ReportDisplyDto>> GetAll()
        {
            List<ReportDisplyDto> reportDisplyDtos = new List<ReportDisplyDto>();
            IEnumerable<Report> reports = await _dbContext.Reports.ToListAsync(); 
           

            foreach (var report in reports)
            {
                ReportDisplyDto reportToDisplay = new ReportDisplyDto();
                if (report.Type == ReportType.Content)
                {
                    var course = await _courseRepository.GetById(report.TargetId);
                    User ReportedAt = await _userManager.Users.FirstAsync(user => user.Id == course.TeacherId);
                    User Reporter = await _userManager.Users.FirstAsync(user => user.Id == report.UserId);
                    
                    reportToDisplay.ReportedAt = ReportedAt.FirstName + " " + ReportedAt.LastName;
                    reportToDisplay.Reporter = Reporter.FirstName + " " + Reporter.LastName;
                    reportToDisplay.Description = report.Description;
                    reportToDisplay.Problem = report.Problem;
                    reportToDisplay.TypeContent = "محتوى";
                    reportToDisplay.CourseId = course.Id;
                    reportToDisplay.reportId = report.Id;
                    reportToDisplay.Type = report.Type;
                }
                else if (report.Type == ReportType.Chat)
                {
                    var chat = await _chatRepository.GetById(report.TargetId);
                    User ReportedAt;
                    if (report.UserId != chat.SenderId)
                    {
                        ReportedAt = await _userManager.Users.FirstAsync(user => user.Id == chat.SenderId);
                    }
                    ReportedAt = await _userManager.Users.FirstAsync(user => user.Id == chat.ReceiverId);
                    User Reporter = await _userManager.Users.FirstAsync(user => user.Id == report.UserId);

                    reportToDisplay.ReportedAt = ReportedAt.FirstName + " " + ReportedAt.LastName;
                    reportToDisplay.Reporter = Reporter.FirstName + " " + Reporter.LastName;
                    reportToDisplay.Description = report.Description;
                    reportToDisplay.Problem = report.Problem;
                    reportToDisplay.TypeContent = "محادثة";
                    reportToDisplay.UserId = ReportedAt.Id;
                    reportToDisplay.reportId = report.Id;
                    reportToDisplay.Type = report.Type;
                }
                reportDisplyDtos.Add(reportToDisplay);
            }
            return reportDisplyDtos;
        }
    }
}

