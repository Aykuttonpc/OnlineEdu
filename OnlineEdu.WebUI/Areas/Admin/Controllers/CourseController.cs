﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.WebUI.DTOs.CourseCategoryDtos;
using OnlineEdu.WebUI.DTOs.CourseDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
   
    public class CourseController : Controller
    {
        private readonly HttpClient _client;

        public CourseController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("EduClient");

        }


        public async Task CourseCategoryDropDown()
        {
        var courseCategoryList = await _client.GetFromJsonAsync<List<ResultCourseCategoryDto>>("CourseCategories");

            List <SelectListItem> courseCategories = (from c in courseCategoryList
                                                      select new SelectListItem
                                                      {
                                                          Text = c.CategoryName,
                                                          Value = c.CourseCategoryId.ToString()
                                                      }).ToList();
            ViewBag.courseCategories = courseCategories;
        }

       
        
        public async Task<IActionResult> Index()
        {
            var values = await _client.GetFromJsonAsync<List<ResultCourseDto>>("Courses");
            return View(values);
        }


        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _client.DeleteAsync($"Courses/{id}");
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            await CourseCategoryDropDown();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            await _client.PostAsJsonAsync("Courses", createCourseDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            await CourseCategoryDropDown();
            var value = await _client.GetFromJsonAsync<UpdateCourseDto>($"Courses/" + id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            await _client.PutAsJsonAsync("Courses", updateCourseDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowOnHome(int id)
        {
           await _client.GetAsync("courses/ShowOnHome/" + id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DontShowOnHome(int id)
        {
            await _client.GetAsync("courses/DontShowOnHome/" + id);
            return RedirectToAction("Index");
        }

      
    }
}
