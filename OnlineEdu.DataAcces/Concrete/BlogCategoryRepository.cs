using Microsoft.EntityFrameworkCore;
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
    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoryRepository

    {
        public BlogCategoryRepository(OnlineEduContext context) : base(context)
        {
        }

        public List<BlogCategory> GetCategoriesWithBlogs()
        {
          return _context.BlogCategories.Include(x=>x.Blogs).ToList();
        }
    }
}
