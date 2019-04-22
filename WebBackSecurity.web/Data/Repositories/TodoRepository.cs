using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebBackSecurity.web.Data.Entities;

namespace WebBackSecurity.web.Data.Repositories
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Task<IList<Todo>> GetAllByIdAsync(string userId);
        Task<Todo> GetByIdAsync(string userId, int id);
    }

    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext context) : base(context)
        {
        }

        public async Task<IList<Todo>> GetAllByIdAsync(string userId) =>
             await Context.Todos.Where(x => x.UserId == userId).ToListAsync();

        public async Task<Todo> GetByIdAsync(string userId, int id) =>
             await Context.Todos.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);
    }
}