using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Businnes.Abstract;
using OnlineEdu.DTO.DTOs.AboutDtos;
using OnlineEdu.DTO.DTOs.TestimonialDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController (IGenericService<Testimonial> _testimonialService ,IMapper _mapper): ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var values = _testimonialService.TGetList();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _testimonialService.TGetById(id);
            return Ok(value);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            _testimonialService.TDelete(id);
            return Ok("Testimonial Alanı Silindi");
        }

        [HttpPost]
        public IActionResult Create(CreateTestimonialDto createTestimonialDto)
        {
            var newValue = _mapper.Map<Testimonial>(createTestimonialDto);
            _testimonialService.Tcreate(newValue);
            return Ok("Yeni testimonial alanı oluşturuldu");

        }
        [HttpPut]
        public IActionResult Update(UpdateTestimonialDto updateTestimonialDto)
        {
            var value = _mapper.Map<Testimonial>(updateTestimonialDto);
            _testimonialService.TUpdate(value);
            return Ok("testimonial alanı güncellendi");
        }
        [AllowAnonymous]
        [HttpGet("GetTestimoinalCount")]
        public IActionResult GetTestimoinalCount()
        {
            var courseCount = _testimonialService.TCount();
            return Ok(courseCount);
        }
    }
}
