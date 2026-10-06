using Microsoft.EntityFrameworkCore;
using ToDoListMvc.Models;
using Microsoft.EntityFrameworkCore;
using ToDoListMvc.Models;

namespace ToDoListMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}