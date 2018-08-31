using Microsoft.EntityFrameworkCore;
using Santex.League.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Santex.League.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        public DbSet<T> dbSet;


        public Repository(DbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public async Task Create(params T[] items)
        {
            foreach (T item in items)
                await _context.Set<T>().AddAsync(item);
        }

        public Task Update(params T[] items)
        {
            foreach (T item in items)
                _context.Set<T>().Update(item);

            return Task.CompletedTask;
        }

        public Task Remove(params T[] items)
        {
            foreach (T item in items)
                _context.Set<T>().Remove(item);

            return Task.CompletedTask;
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = dbSet;

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.ToList<T>();
        }

        public virtual IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = dbSet;

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            dbQuery = dbQuery.Where(where);

            return dbQuery.ToList<T>();
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = dbSet;

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.FirstOrDefault(where);
        }
    }
}
