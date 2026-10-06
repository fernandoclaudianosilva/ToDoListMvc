using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoListMvc.Exceptions;
using ToDoListMvc.Models;
using ToDoListMvc.Services;

namespace ToDoListMvc.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var list = await _taskService.GetAllTasksAsync();
            return View(list);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(taskItem);
                return RedirectToAction(nameof(Index));
            }
            return View(taskItem);
        }
        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _taskService.GetTaskByIdAsync(id.Value);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _taskService.UpdateTaskAsync(task);
                }
                catch (NotFoundExeption)
                {
                    return RedirectToAction(nameof(Error), new { message = "Tarefa não encontrada para atualização." });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _taskService.GetTaskByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundExeption e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}