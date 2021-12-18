using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfAppTask.Context;
using WpfAppTask.DataAccess.Abstract;

namespace WpfAppTask.DataAccess.Concrete
{
    public class GenericRepository<T> : IGenericRepositoryPattern<T> where T : class
    {

        protected MyDbContext _context;

        public GenericRepository()
        {
            _context = new MyDbContext();
        }

        public IQueryable<T> GetAll()
        {
            return from entity in _context.Set<T>()
                   select entity;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp);
        }


        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            SubmitChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            SubmitChanges();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            SubmitChanges();
        }

        public void SubmitChanges()
        {
            _context.SaveChanges();
        }
    }
}
