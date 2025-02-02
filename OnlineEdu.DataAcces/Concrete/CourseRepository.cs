using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.DataAcces.Context;
using OnlineEdu.DataAcces.Repositories;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAcces.Concrete
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(OnlineEduContext context) : base(context)
        {

        }

        public void DontShowOnHome(int id)
        {
            var value = _context.Courses.Find(id);
            value.IsShown = false;
            _context.SaveChanges();

        }

        public List<Course> GetAllCoursesWithCategories()
        {
           return  _context.Courses.Include( x => x.CourseCategory).Include(x=>x.AppUser).ToList();
        }

        public List<Course> GetAllCoursesWithCategories(Expression<Func<Course, bool>> filter = null)
        {
            IQueryable<Course> value = _context.Courses.Include(x =>x.CourseCategory).Include(x  => x.AppUser).AsQueryable();
            if (filter != null)
            {
                value = value.Where(filter);
            }
            return value.ToList();
            
        }

        public List<Course> GetCoursesByTeacherId(int id)
        {
           return _context.Courses.Include(x =>x.CourseCategory).Where(x =>x.AppUserId == id).ToList();
        }

        public void ShowOnHome(int id)
        {
            var value = _context.Courses.Find(id);
            value.IsShown = true;
            _context.SaveChanges();
        }
    }
}
