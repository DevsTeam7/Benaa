using Microsoft.AspNetCore.Mvc;

namespace Benaa.Dashboard.Controllers
{
    public class OwnerController : Controller
    {
        public ActionResult Income()
        {
            return View();
        }

        public ActionResult Admins()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            return View();
        }



    }
}
