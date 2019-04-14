using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebBackSecurity.web.Data.Interfaces;

namespace WebBackSecurity.web.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TodoDbContext Context;

        public Repository(TodoDbContext context)
        {
            Context = context;
        }

        public int Count(Func<T, bool> predicate)
        {
            return Context.Set<T>().Where(predicate).Count();
        }

        public void Create(T entity)
        {
            Context.Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
            Save();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        protected void Save()
        {
            Context.SaveChanges();
        }
    }
}