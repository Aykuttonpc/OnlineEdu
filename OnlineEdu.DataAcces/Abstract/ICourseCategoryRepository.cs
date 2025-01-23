using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAcces.Abstract
{
    public interface ICourseCategoryRepository:IRepository<CourseCategory>
    {
        void ShowOnHome(int CourseCategoryId);
        void DontShowOnHome(int CourseCategoryId);


    }
}
