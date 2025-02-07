using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.ContactDtos;
using OnlineEdu.WebUI.DTOs.CourseDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
    public class _HomeCourseComponent : ViewComponent
    {
        private readonly HttpClient _client;

        public _HomeCourseComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("EduClient");

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _httpClient.GetFromJsonAsync<List<ResultCourseDto>>("courses/GetActiveCourses");

            // Pass the values to the view
            return View(values);
        }

    }
}
