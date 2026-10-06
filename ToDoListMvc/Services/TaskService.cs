using Microsoft.EntityFrameworkCore;
using ToDoListMvc.Data;
using ToDoListMvc.Models;
using ToDoListMvc.Exceptions;

namespace ToDoListMvc.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItem>>GetAllTasksAsync()
        {
            return await _context.TaskItems.ToListAsync();

        }
        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateTaskAsync(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskAsync(TaskItem taskItem)
        {
            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTaskAsync(int id)
        {
            var taskItem = await GetTaskByIdAsync(id);
            if (taskItem == null) { 
            throw new NotFoundExeption("Id dismtach");
            }
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

        }

    }
}
