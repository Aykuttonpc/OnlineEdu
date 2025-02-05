
using OnlineEdu.Entity.Entities;
using OnlineEdu.WebUI.DTOs.CourseCategoryDtos;
using OnlineEdu.WebUI.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.WebUI.DTOs.CourseDtos
{
    public class ResultCourseDto
    {
        public int CourseId { get; set; }
        public string  CourseName { get; set; }
        public string ImageUrl { get; set; }
        public int CourseCategoryId { get; set; }
        public ResultCourseCategoryDto CourseCategory { get; set; }
        public decimal Price { get; set; }
        public bool IsShown { get; set; }
        public ResultUserDto AppUser { get; set; }

        public int AppUserId { get; set; }

    }
}
