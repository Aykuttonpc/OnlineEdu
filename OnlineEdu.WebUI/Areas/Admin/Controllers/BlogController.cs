using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.BlogCategoryDtos;
using OnlineEdu.WebUI.DTOs.BlogDtos;
using OnlineEdu.WebUI.Helpers;
using OnlineEdu.WebUI.Services.TokenServices;
using System.Net.Http;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    
    public class BlogController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly HttpClient _client;
        public BlogController(UserManager<AppUser> userManager,IHttpClientFactory httpClientFactory,ITokenService tokenService)
        {
            _client = httpClientFactory.CreateClient("EduClient");
            _tokenService = tokenService;
        }

        public async Task CategoryDropdown()
        {
            var CategoryList = await _client.GetFromJsonAsync<List<ResultBlogCategoryDto>>("blogCategories");

            // Kategorileri doğru şekilde ViewBag'e atadığınızdan emin olun
            List<SelectListItem> categories = (from c in CategoryList
                                               select new SelectListItem
                                               {
                                                   Text = c.Name,
                                                   Value = c.BlogCategoryId.ToString()
                                               }).ToList();

            ViewBag.CategoryList = categories;
        }

        public async  Task<IActionResult> Index()
        {
            var values = await _client.GetFromJsonAsync<List<ResultBlogDto>>("blogs");
            return View(values);
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _client.DeleteAsync($"blogs/{id}");
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {
            await CategoryDropdown();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            var userId = _tokenService.GetUserId;
            createBlogDto.WriterId = userId;

            await _client.PostAsJsonAsync("blogs", createBlogDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog(int id)
        {
            await CategoryDropdown();
            var value = await _client.GetFromJsonAsync<UpdateBlogDto>($"blogs/" + id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            await _client.PutAsJsonAsync("blogs", updateBlogDto);
            return RedirectToAction("Index");
        }



    }
}
