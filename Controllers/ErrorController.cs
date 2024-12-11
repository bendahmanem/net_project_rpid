using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPBookProject.Controllers
{
    public class ErrorController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
