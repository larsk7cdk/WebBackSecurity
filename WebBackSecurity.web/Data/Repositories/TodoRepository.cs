using System.Collections.Generic;
using System.Linq;
using WebBackSecurity.web.Data.Entities;
using WebBackSecurity.web.Data.Interfaces;

namespace WebBackSecurity.web.Data.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext context) : base(context)
        {
        }

        public IEnumerable<Todo> GetAllById(string userId)
        {
            return Context.Todos.Where(x => x.UserId == userId);
        }
    }
}