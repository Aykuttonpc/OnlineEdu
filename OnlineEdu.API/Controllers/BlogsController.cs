﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.Businnes.Abstract;
using OnlineEdu.DTO.DTOs.BlogDtos;
using OnlineEdu.Entity.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController (IMapper _mapper ,IBlogService  _blogService) : ControllerBase 
    {
    
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var values = _blogService.TGetBlogsWithCategories();
            var blogs = _mapper.Map<List<ResultBlogDto>>(values);
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("GetLast4Blogs")]
        public IActionResult GetLast4Blogset()
        {
            var values = _blogService.TGetLastBlogsWithCategories();
            var blogs = _mapper.Map<List<ResultBlogDto>>(values);
            return Ok(blogs);
        }



        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var value = _blogService.TGetBlogsWithCategory(id); 
            return Ok(value);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            _blogService.TDelete(id);
            return Ok("blog Alanı Silindi");
        }

        [HttpPost]
        public IActionResult Create(CreateBlogDto createBlogDto)
        {
            var newValue = _mapper.Map<Blog>(createBlogDto);
            _blogService.Tcreate(newValue);
            return Ok("Yeni blog alanı oluşturuldu");

        }
        [HttpPut]
        public IActionResult Update(UpdateBlogDto updateBlogDto)
        {
            var value = _mapper.Map<Blog>(updateBlogDto);
            _blogService.TUpdate(value);
            return Ok("blog alanı güncellendi");
        }

        [HttpGet("GetBlogByWriterId/{id}")]
        public IActionResult GetBlogByWriterId(int id)
        {
            var values = _blogService.TGetBlogsWithCategoriesByWriterId(id);
            var mappedValues = _mapper.Map<List<Blog>>(values);
            return Ok(mappedValues);
        }

        [AllowAnonymous]
        [HttpGet("GetBlogCount")]
        public IActionResult GetBlogCount()
        {
            var blogCount = _blogService.TCount();
            return Ok(blogCount);
        }

        [AllowAnonymous]
        [HttpGet("GetBlogsByCategoryId/{id}")]
        public IActionResult GetBlogsByCategoryId(int id)
        {
            var blogs = _blogService.TGetBlogsByCategoryId(id);    
            return Ok(blogs);
        }
    }
}
