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
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private  readonly IBlogRepository _blogRepository;
        public BlogManager(IRepository<Blog> _repository, IBlogRepository blogRepository) : base(_repository)
        {
            this._blogRepository = blogRepository;
        }

        public Blog TGetBlogsWithCategory(int id)
        {
           return _blogRepository.GetBlogsWithCategory(id);
        }

        public List<Blog> TGetBlogsWithCategories()
        {
            return _blogRepository.GetBlogsWithCategories();
        }

        public List<Blog> TGetBlogsWithCategoriesByWriterId(int id)
        {
            return _blogRepository.GetBlogsWithCategoriesByWriterId(id);
        }

        public List<Blog> TGetLastBlogsWithCategories()
        {

            return _blogRepository.GetLastBlogsWithCategories();
        }
    }
}
