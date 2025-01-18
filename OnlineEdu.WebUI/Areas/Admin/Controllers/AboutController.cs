using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
    [Route("[area]/[Controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
