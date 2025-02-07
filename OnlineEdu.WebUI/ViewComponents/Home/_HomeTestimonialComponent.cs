using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using OnlineEdu.WebUI.DTOs.TestimonialDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.ViewComponents.Home
{
    public class _HomeTestimonialComponent:ViewComponent
    {
        private readonly HttpClient _client;

        public _HomeTestimonialComponent(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("EduClient");

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _client.GetFromJsonAsync<List<ResultTestimonialDto>>("testimonials");
            return View(values);
        }
    }
}
