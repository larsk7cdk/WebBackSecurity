using Microsoft.EntityFrameworkCore;
using WebBackSecurity.web.Models;

namespace WebBackSecurity.web.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Todo> Todos { get; set; }
    }
}