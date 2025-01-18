using Microsoft.AspNetCore.Mvc;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[Controller]/[action]/{id?}")]
    public class AboutController(HttpClient _client) : Controller
    {
        public IActionResult Index()
        {
           // var values = _client.GetFromJsonAsync
            return View();
        }
    }
}
