﻿using Microsoft.AspNetCore.Mvc;
using OnlineEdu.WebUI.DTOs.BlogDtos;
using OnlineEdu.WebUI.DTOs.SubscriberDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _client  = HttpClientInstance.CreateClient();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Subscribe(CreateSubscriberDto model)
        {
            await _client.PutAsJsonAsync("subscribers", model);
            return NoContent();
        }

        public async Task<IActionResult> BlogDetails(int id)
        {
            var blog = await _client.GetFromJsonAsync<ResultBlogDto>("blogs/"+id);
            return View(blog);
        }

        public async Task<IActionResult> BlogsByCategory(int id)
        {
            var blogs = await _client.GetFromJsonAsync<List<ResultBlogDto>>("blogs/GetBlogsByCategoryId/"+ id);

            ViewBag.CategoryName =blogs.Select(x=>x.BlogCategory.Name).FirstOrDefault();
            return View(blogs);


        }
    }
}
