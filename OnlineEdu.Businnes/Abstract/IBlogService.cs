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
        List<Blog> TGetLastBlogsWithCategories();
        List<Blog> TGetBlogsWithCategoriesByWriterId(int id);

    }
}
