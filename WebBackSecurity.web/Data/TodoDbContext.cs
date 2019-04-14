﻿using Microsoft.EntityFrameworkCore;
using WebBackSecurity.web.Data.Entities;

namespace WebBackSecurity.web.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        public DbSet<Todo> Todos { get; set; }
    }
}