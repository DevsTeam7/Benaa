using Microsoft.AspNetCore.Mvc;

namespace Benaa.Dashboard.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Teachers()
        {
            return View();
        }
        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult CodeGenration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CodeGenration(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
