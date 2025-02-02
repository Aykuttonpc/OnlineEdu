using OnlineEdu.Businnes.Abstract;
using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.DataAcces.Concrete;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.Businnes.Concrete
{
    public class BlogCategoryManager : GenericManager<BlogCategory>, IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        public BlogCategoryManager(IRepository<BlogCategory> _repository, IBlogCategoryRepository blogCategoryRepository) : base(_repository)
        {
            _blogCategoryRepository = blogCategoryRepository;
        }

        public List<BlogCategory> TGetCategoriesWithBlogs()
        {
            return _blogCategoryRepository.GetCategoriesWithBlogs();
        }
    }
}
