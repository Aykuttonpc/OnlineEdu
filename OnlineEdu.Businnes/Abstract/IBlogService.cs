using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.Businnes.Abstract
{
    public interface IBlogService :IGenericService<Blog>
    {
        List<Blog> TGetBlogsWithCategories();
        public List<Blog> TGetBlogsByCategoryId(int id);
        Blog TGetBlogsWithCategory(int id);
        List<Blog> TGetLastBlogsWithCategories();
        List<Blog> TGetBlogsWithCategoriesByWriterId(int id);


    }
}
