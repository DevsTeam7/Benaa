using Microsoft.AspNetCore.Mvc;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.General;
using Benaa.Infrastructure.Utils.Users;

namespace Benaa.Dashboard.Controllers
{
    public class AuthentcationController : Controller
    {
        private readonly IAuthService _authService;
        public AuthentcationController(IAuthService authService)
        {
            _authService = authService;
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> login([FromForm] LoginRequestDto.Request request)
        {
            try
            {
                var res = await _authService.Login(request);
                if (res.IsError) { return RedirectToAction("Authentcation", "login", new { error = res.ErrorsOrEmptyList }); }
                HttpContext.Session.SetString("token", res.Value.Role);
                if(res.Value.Role == Role.Admin) { return RedirectToAction("Admin", "Teachers"); }
                return RedirectToAction("Owner", "Income"); 
            }
            catch (Exception ex)
            {
                return RedirectToAction("Authentcation", "login", new
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
				return RedirectToAction("login", "Authentcation");
			}
			return RedirectToAction("login", "Authentcation");
		}
    }
}
