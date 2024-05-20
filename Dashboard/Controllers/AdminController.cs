using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Benaa.Dashboard.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOwnerService _ownerService;
        private readonly IMoneyCodeService _moneyCodeService;
        private readonly IReportService _reportService;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        public AdminController(IOwnerService ownerService,IMoneyCodeService moneyCodeService,
            IReportService reportService, ICourseService courseService, IUserService userService) {
            _ownerService = ownerService;
            _moneyCodeService = moneyCodeService;
            _reportService = reportService;
            _courseService = courseService;
            _userService = userService;
        }
        public async Task<ActionResult> Teachers()
        {
            IEnumerable<User> teachers = await _ownerService.Get();
            return View(teachers);
        }
        public async Task<ActionResult> Accept(string Id)
        {
            await _ownerService.ac(Id);
			return RedirectToAction("Teachers","Admin");
		}
		public async Task<ActionResult> Delete(string Id)
		{
			await _ownerService.del(Id);
			return RedirectToAction("Teachers", "Admin");
		}
		public async Task<ActionResult> Reports()
        {
           IEnumerable<ReportDisplyDto> reports = await _reportService.GetReport();
            return View(reports);
        }

        public ActionResult CodeGenration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CodeGenration(int amount, int number)
        {
            var result = await _moneyCodeService.Generate(amount, number);  
            return View(result.Value);
        }

        public async Task<ActionResult> DeleteCousrse(Guid courseId)
        {
            await _courseService.Delete(courseId);
            await _reportService.DeleteAll(courseId);
            return RedirectToAction("Reports", "Admin");
        }
        public async Task<ActionResult> DeleteUser(string userId)
        {
            await _userService.Delete(userId);
            await _reportService.DeleteAll(Guid.Parse(userId));
            return RedirectToAction("Reports", "Admin");
        }
        public async Task<ActionResult> DeleteReport(Guid reportId)
        {
            await _reportService.Delete(reportId);
            return RedirectToAction("Reports", "Admin");
        }
    }

}
