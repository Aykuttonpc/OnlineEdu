﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.CourseCategoryDtos;
using OnlineEdu.WebUI.DTOs.CourseDtos;
using OnlineEdu.WebUI.DTOs.CourseVideoDtos;
using OnlineEdu.WebUI.Helpers;

namespace OnlineEdu.WebUI.Areas.Teacher.Controllers
{
    [Authorize(Roles ="Teacher")]
    [Area("Teacher")]
    public class MyCourseController : Controller
    {

        private readonly HttpClient _client = HttpClientInstance.CreateClient();

        public MyCourseController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        private readonly UserManager<AppUser> _userManager;

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = await _client.GetFromJsonAsync<List<ResultCourseDto>>("courses/GetCoursesByTeacherId/" + user.Id);
            
            return View(values);
        }

        public async  Task<IActionResult> CreateCourse()
        {
            var categoryList = await _client.GetFromJsonAsync<List<ResultCourseCategoryDto>>("courseCategories");

            List<SelectListItem> categories = (from x in  categoryList
                                               select new SelectListItem
                                               {
                                                   Text=x.CategoryName,
                                                   Value =x.CourseCategoryId.ToString(),
                                               }).ToList();
            ViewBag.categories = categories;
                                               
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            createCourseDto.AppUserId = user.Id;
            createCourseDto.IsShown = false;
            await _client.PostAsJsonAsync("courses", createCourseDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _client.DeleteAsync("courses/" + id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var categoryList = await _client.GetFromJsonAsync<List<ResultCourseCategoryDto>>("courseCategories");

            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CourseCategoryId.ToString(),
                                               }).ToList();
            ViewBag.categories = categories;

            var values = await _client.GetFromJsonAsync<UpdateCourseDto>("courses/" + id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            updateCourseDto.AppUserId = user.Id;

            await _client.PutAsJsonAsync("courses",updateCourseDto);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> CourseVideos(int id)
        {
            var values = await _client.GetFromJsonAsync<List<ResultCourseVideoDto>>("courseVideos/GetCourseVideosByCourseId/" + id);
            TempData["courseId"] = id;
            ViewBag.courseName = values.Select(x => x.Course.CourseName).FirstOrDefault();

            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> CreateVideo()
        {
            var courseId = (int)TempData["courseId"];
            var course = await _client.GetFromJsonAsync<ResultCourseDto>("courses/" + courseId);
            ViewBag.courseName = course.CourseName;
            ViewBag.courseId = course.CourseId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateVideo(CreateCourseVideoDto model)
        {
          
            await _client.PostAsJsonAsync("courseVideos", model);
            return RedirectToAction("Index");
        }
    }
}
