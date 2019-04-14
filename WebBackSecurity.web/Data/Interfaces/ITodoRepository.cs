using System.Collections.Generic;
using WebBackSecurity.web.Data.Entities;

namespace WebBackSecurity.web.Data.Interfaces
{
    public interface ITodoRepository : IRepository<Todo>
    {
        IEnumerable<Todo> GetAllById(string userId);
    }
}