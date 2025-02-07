using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineEdu.WebUI.DTOs.ContactDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.ViewComponents.Contact
{
    public class _ContactInfo :ViewComponent
    {
        private readonly HttpClient _client;

        public _ContactInfo(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("EduClient");

        }
        public  async Task<IViewComponentResult> InvokeAsync()
        {

            var values = await _client.GetFromJsonAsync<List<ResultContactDto>>("contacts");
            return View(values);

        }
    }
}
