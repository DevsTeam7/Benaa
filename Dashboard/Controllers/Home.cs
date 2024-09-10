using Microsoft.AspNetCore.Mvc;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Utils.Users;

namespace Benaa.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;
        public HomeController(IAuthService authService)
        {
            _authService = authService;
        }
        public ActionResult index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> index([FromForm] LoginRequestDto request)
        {
            try
            {
                var res = await _authService.Login(request);
                if (res.IsError) { return RedirectToAction("Home", "index", new { error = res.ErrorsOrEmptyList }); }
                HttpContext.Session.SetString("token", res.Value.Role);
                if(res.Value.Role == Role.Admin) { return RedirectToAction("Teachers", "Admin"); }
                return RedirectToAction("Income","Owner"); 
            }
            catch (Exception ex)
            {
                return RedirectToAction("Home", "index", new
                {
                    error = ex.Message
                });
            }
        }
        [HttpGet]
        public ActionResult Logout()
        {
            var UserToken = HttpContext.Session.GetString("token");
            if (UserToken != null)
            {
				HttpContext.Session.Remove("token");
				return RedirectToAction("index", "Home");
			}
			return RedirectToAction("index", "Home");
		}
    }
}
