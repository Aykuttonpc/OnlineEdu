﻿using Microsoft.EntityFrameworkCore;
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
       
        public BlogRepository(OnlineEduContext _context, OnlineEduContext context) : base(_context)
        {
          
        }

        public List<Blog> GetBlogsByCategoryId(int id)
        {
           return _context.Blogs.Include(x =>x.BlogCategory).Include(x =>x.Writer).Where(x => x.BlogCategoryId == id).ToList();
        }

        public List<Blog> GetBlogsWithCategories()
        {
          return _context.Blogs.Include(x => x.BlogCategory).Include(x=>x.Writer).ToList();


        }

        public List<Blog> GetBlogsWithCategoriesByWriterId(int id)
        {
            return  _context.Blogs.Include(x=>x.BlogCategory).Where(x=>x.WriterId == id).ToList();
        }

        public Blog GetBlogsWithCategory(int id)
        {
            return _context.Blogs.Include(x => x.BlogCategory).Include(x => x.Writer).ThenInclude(x=>x.TeacherSocials).FirstOrDefault(x => x.BlogId == id);

        }

        public List<Blog> GetLastBlogsWithCategories()
        {
            return _context.Blogs.Include(x=>x.BlogCategory).OrderByDescending(x=>x.BlogId).Take(4).ToList();
        }
    }
}
