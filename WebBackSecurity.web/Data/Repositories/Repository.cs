using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebBackSecurity.web.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TodoDbContext Context;

        public Repository(TodoDbContext context)
        {
            Context = context;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate) => Context.Set<T>().Where(predicate);

        public IEnumerable<T> GetAll() => Context.Set<T>();

        public T GetById(int id) => Context.Set<T>().Find(id);

        public Task<T> GetByIdAsync(int id) => Context.Set<T>().FindAsync(id);

        public void Create(T entity)
        {
            Context.Add(entity);
            Save();
        }

        public async Task CreateAsync(T entity)
        {
            Context.Add(entity);
            await SaveAsync();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
            Save();
        }

        public async Task DeleteAsync(T entity)
        {
            Context.Remove(entity);
            await SaveAsync();
        }

        public int Count(Func<T, bool> predicate) => Context.Set<T>().Where(predicate).Count();

        protected void Save() => Context.SaveChanges();

        protected async Task SaveAsync() => await Context.SaveChangesAsync();
    }
}