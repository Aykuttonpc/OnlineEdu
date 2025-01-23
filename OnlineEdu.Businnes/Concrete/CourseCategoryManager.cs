using OnlineEdu.Businnes.Abstract;
using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.Businnes.Concrete
{
    public class CourseCategoryManager : GenericManager<CourseCategory>, ICourseCategoryService
    {

        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public CourseCategoryManager(IRepository<CourseCategory> _repository, ICourseCategoryRepository courseCategoryRepository) : base(_repository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }

        public void TDontShowOnHome(int CourseCategoryId)
        {
           
            _courseCategoryRepository.DontShowOnHome(CourseCategoryId);

        }

        public void TShowOnHome(int CourseCategoryId)
        {

            _courseCategoryRepository.ShowOnHome(CourseCategoryId);
        }
    }
}
