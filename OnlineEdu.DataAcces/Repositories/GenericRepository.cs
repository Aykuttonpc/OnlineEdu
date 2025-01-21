using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.DataAcces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAcces.Repositories
{
    public class GenericRepository<T>(OnlineEduContext _context) : IRepository<T> where T : class
    {

        //private readonly OnlineEduContext _context;

        //public GenericRepository(OnlineEduContext context)
        //{
        //    _context = context;
        //}


        public DbSet<T> Table { get => _context.Set<T>(); }

        public int Count()
        {
            return Table.Count();

        }

        public void create(T entity)
        {
           Table.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Table.Find(id);
            Table.Remove(entity);
            _context.SaveChanges();

        }

        public int FilterCount(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate).Count();
        }

        public T GetByFilter(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate).FirstOrDefault();
        }

        public T GetById(int id)
        {
            return Table.Find(id);
        }

        public List<T> GetFilteredList(Expression<Func<T, bool>> predicate)
        {
           return Table.Where(predicate).ToList();
        }

        public List<T> GetList()
        {
            return Table.ToList();

        }

        public void Update(T entity)
        {
            Table.Update(entity);
            _context.SaveChanges();

        }
    }
}
