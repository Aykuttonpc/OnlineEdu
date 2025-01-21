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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly OnlineEduContext _educontext;
        public BlogRepository(OnlineEduContext _context, OnlineEduContext context) : base(_context)
        {
            this._educontext = context;
        }

        public List<Blog> GetBlogsWithCategories()
        {
          return _educontext.Blogs.Include(x => x.BlogCategory).ToList();


        }
    }
}
