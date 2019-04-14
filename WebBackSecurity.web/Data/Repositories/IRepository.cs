using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebBackSecurity.web.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> predicate);

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        void Create(T entity);

        Task CreateAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        int Count(Func<T, bool> predicate);
    }
}