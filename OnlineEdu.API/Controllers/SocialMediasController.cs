using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Businnes.Abstract;
using OnlineEdu.DTO.DTOs.AboutDtos;
using OnlineEdu.DTO.DTOs.SocialMediaDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController(IGenericService<SocialMedia> _socialNediaService,IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var values = _socialNediaService.TGetList();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _socialNediaService.TGetById(id);
            return Ok(value);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            _socialNediaService.TDelete(id);
            return Ok("Sosyal medya Alanı Silindi");
        }

        [HttpPost]
        public IActionResult Create(CreateSocialMediaDto createSocialMediaDto)
        {
            var newValue = _mapper.Map<SocialMedia>(createSocialMediaDto);
            _socialNediaService.Tcreate(newValue);
            return Ok("Yeni Sosyal medya  alanı oluşturuldu");

        }
        [HttpPut]
        public IActionResult Update(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var value = _mapper.Map<SocialMedia>(updateSocialMediaDto);
            _socialNediaService.TUpdate(value);
            return Ok("Sosyal medya  alanı güncellendi");
        }
    }
}
