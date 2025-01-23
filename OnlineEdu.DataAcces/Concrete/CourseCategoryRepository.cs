using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.DataAcces.Context;
using OnlineEdu.DataAcces.Repositories;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAcces.Concrete
{
    public class CourseCategoryRepository : GenericRepository<CourseCategory>, ICourseCategoryRepository
    {
        public CourseCategoryRepository(OnlineEduContext _context) : base(_context)
        {
        }

        public void DontShowOnHome(int CourseCategoryId)
        {
            var value = _context.CourseCategories.Find(CourseCategoryId);
            value.IsShown = false;
            _context.SaveChanges();
        }
         
        public void ShowOnHome(int CourseCategoryId)
        {
            var value = _context.CourseCategories.Find(CourseCategoryId);
            value.IsShown = true;
            _context.SaveChanges();
        }
    }
}
