using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Businnes.Abstract;
using OnlineEdu.DTO.DTOs.TeacherSocialDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherSocialsController(IGenericService<TeacherSocial> _teacherSocialService, IMapper _mapper) : ControllerBase
    {

        [HttpGet("byTeacherId/{id}")]
        public IActionResult GetSocialByTeacherId(int id)
        {
            var values = _teacherSocialService.TGetFilteredList(x => x.TeacherId == id);
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _teacherSocialService.TGetById(id);
            return Ok(value);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            _teacherSocialService.TDelete(id);
            return Ok("Eğitmen Sosyal Medya Alanı Silindi");
        }

        [HttpPost]
        public IActionResult Create(CreateTeacherSocialDto createTeacherSocialDto)
        {
            var newValue = _mapper.Map<TeacherSocial>(createTeacherSocialDto);
            _teacherSocialService.Tcreate(newValue);
            return Ok("Yeni Eğitmen Sosyal Medya  alanı oluşturuldu");

        }
        [HttpPut]
        public IActionResult Update(UpdateTeacherSocialDto updateTeacherSocialDto)
        {
            var value = _mapper.Map<TeacherSocial>(updateTeacherSocialDto);
            _teacherSocialService.TUpdate(value);
            return Ok("Eğitmen Sosyal Medya  alanı güncellendi");
        }
    }
}


