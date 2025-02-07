using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BannerDtos;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.UserServices;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
    public class _HomeCounterComponent:ViewComponent
    {
        private readonly HttpClient _client;
        private readonly IUserService _userService;

        public _HomeCounterComponent(IHttpClientFactory httpClientFactory, IUserService userService)
        {
            _client = httpClientFactory.CreateClient("EduClient");
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.blogCount = await _client.GetFromJsonAsync<int>("blogs/GetBlogCount");
            ViewBag.courseCount = await _client.GetFromJsonAsync<int>("courses/GetCourseCount");
            ViewBag.courseCategoryCount = await _client.GetFromJsonAsync<int>("courseCategories/GetCourseCategoryCount");
            ViewBag.teacherCount = await _userService.GetTeacherCount();
            


            return View();
        }
    }
}
