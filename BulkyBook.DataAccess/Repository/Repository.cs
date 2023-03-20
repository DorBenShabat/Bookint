using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDB _db;
        internal DbSet<T> DBSet;

        public Repository(ApplicationDB db)
        {
            _db = db;
            //_db.ShoppingCarts.AsNoTracking()
            //_db.ShoppingCarts.Include(u => u.Product).Include(u => u.CoverType);
            this.DBSet = _db.Set<T>();
        }

        public void ADD(T entity)
        {
           DBSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter=null, string? includeProperties = null)
        {
            IQueryable<T> query = DBSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query= query.Include(includeProp);
                }
            }
            return query.ToList();  
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = DBSet;
            }
            else
            {
                query = DBSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        
        }

        public void REMOVE(T entity)
        {
           DBSet.Remove(entity);
        }

        public void REMOVERange(IEnumerable<T> entity)
        {
            DBSet.RemoveRange(entity);
        }
    }
}
