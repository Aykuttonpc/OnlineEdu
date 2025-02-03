using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace OnlineEdu.WebUI.ViewComponents.Blog
{
    public class _BlogSubscribe:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
