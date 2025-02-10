using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.WebUI.DTOs.MessagesDtos;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.UserServices;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
   
    public class MessageController : Controller
    {


        private readonly HttpClient _client;
        private readonly IUserService _userService;

        public MessageController(IHttpClientFactory clientFactory, IUserService userService)
        {
            _client = clientFactory.CreateClient("EduClient");
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _client.GetFromJsonAsync<List<ResultMessageDto>>("Messages");
            return View(values);
        }


        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _client.DeleteAsync($"Messages/{id}");
            return RedirectToAction("Index");

        }

       

        [HttpGet]
        public async Task<IActionResult> MessageDetail(int id)
        {
            
            var value = await _client.GetFromJsonAsync<ResultMessageDto>($"Messages/" + id);
            return View(value);
        }

      
    }
}
