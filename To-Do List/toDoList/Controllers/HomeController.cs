using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using toDoList.Data;
using toDoList.Models;

namespace toDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Ana sayfa
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return View(tasks);
        }

        // Yeni görev ekle
        [HttpPost]
        public async Task<IActionResult> AddTask(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var newTask = new TaskItem
                {
                    Title = title,
                    IsCompleted = false,
                    CreatedDate = DateTime.Now
                };

                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Görev durumunu güncelle (tamamlandı/tamamlanmadı)
        [HttpPost]
        public async Task<IActionResult> ToggleTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                task.ModifiedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Görev sil
        [HttpPost]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Görev güncelle
        [HttpPost]
        public async Task<IActionResult> UpdateTask(int id, string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task != null)
                {
                    task.Title = title;
                    task.ModifiedDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

    }
}
