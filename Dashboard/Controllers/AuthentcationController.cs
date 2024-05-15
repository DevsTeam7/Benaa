using Microsoft.AspNetCore.Mvc;
using Benaa.Core.Entities.DTOs;
using Benaa.Core.Interfaces.IServices;
using Benaa.Core.Entities.General;

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
                HttpContext.Session.SetString("token", res.Value.Token);
                return RedirectToAction("Authentcation", "login", new { success = res.Value.Token });

            }
            catch (Exception ex)
            {
                return RedirectToAction("Authentcation", "login", new
                {
                    bad = ex.Message
                });
            }

        }
        public ActionResult Logout()
        {
            var UserToken = HttpContext.Session.GetString("token");
            
            if (UserToken != null)
            {
                HttpContext.Session.Remove("token");
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }
    }
}
