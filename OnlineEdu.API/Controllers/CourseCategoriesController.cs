using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Businnes.Abstract;
using OnlineEdu.DTO.DTOs.AboutDtos;
using OnlineEdu.DTO.DTOs.CourseCategoryDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoriesController(ICourseCategoryService _courseCategoryServices, IMapper _mapper) : ControllerBase
    {



        [HttpGet]
        public IActionResult Get()
        {
            var values = _courseCategoryServices.TGetList();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _courseCategoryServices.TGetById(id);
            return Ok(value);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            _courseCategoryServices.TDelete(id);
            return Ok("Kurs Kategori Alanı Silindi");
        }

        [HttpPost]
        public IActionResult Create(CreateCourseCategoryDto createCourseCategory)
        {
            var newValue = _mapper.Map<CourseCategory>(createCourseCategory);
            _courseCategoryServices.Tcreate(newValue);
            return Ok("Yeni kurs kategori alanı oluşturuldu");

        }
        [HttpPut]
        public IActionResult Update(UpdateCourseCategoryDto updateCourseCategoryDto)
        {
            var value = _mapper.Map<CourseCategory>(updateCourseCategoryDto);
            _courseCategoryServices.TUpdate(value);
            return Ok("kurs kategori alanı güncellendi");
        }

        [HttpGet("ShowOnHome/{id}")]
        public IActionResult ShowOnHome(int id)
        {
            _courseCategoryServices.TShowOnHome(id);
            return Ok("Kurs kategori alanı anasayfada gösterildi");

        }
        [HttpGet("DontShowOnHome/{id}")]
        public IActionResult DontShowOnHome(int id)
        {
            _courseCategoryServices.TDontShowOnHome(id);
            return Ok("Kurs kategori alanı anasayfada gösterilmedi");
        }

        [HttpGet("GetActiveCategories")]
        public IActionResult GetActiveCategories()
        {
            var values = _courseCategoryServices.TGetFilteredList(I => I.IsShown== true);
            return Ok(values);
        }
    }
}
