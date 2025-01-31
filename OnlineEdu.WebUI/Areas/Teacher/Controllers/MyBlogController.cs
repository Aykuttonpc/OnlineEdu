﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.BlogCategoryDtos;
using OnlineEdu.WebUI.DTOs.BlogDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Area("Teacher")]
    public class MyBlogController(UserManager<AppUser> _userManager) : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async  Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = await _client.GetFromJsonAsync<List<ResultBlogDto>>("blogs/GetBlogByWriterId/" + user.Id);

            return View(values);
        }
        public async Task BlogCategoryDropDownAsync()
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

        public async  Task<IActionResult> CreateBlog()
        {
            await BlogCategoryDropDownAsync();


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            createBlogDto.WriterId = user.Id;
            await _client.PostAsJsonAsync("blogs",createBlogDto);
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> UpdateBlog(int id)
        {
            await BlogCategoryDropDownAsync();
            var value = await _client.GetFromJsonAsync<UpdateBlogDto>("blogs/" + id);
            return View(value);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            await _client.PutAsJsonAsync("blogs", updateBlogDto);
            return RedirectToAction("Index");
                
        }

        public async Task<IActionResult> deleteMyBlog(int id)
        {
            await _client.DeleteAsync($"blogs/{id}");
            return RedirectToAction("Index");

        }
    }
}
