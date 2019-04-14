using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebBackSecurity.web.Data.Entities;
using WebBackSecurity.web.Data.Interfaces;
using WebBackSecurity.web.ViewModels.Todo;

namespace WebBackSecurity.web.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(UserManager<IdentityUser> userManager, ITodoRepository todoRepository)
        {
            _userManager = userManager;
            _todoRepository = todoRepository;
        }


        // GET
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var todos = _todoRepository.GetAllById(user.Id).ToList();

            if (todos.Count == 0)
                return View("Empty");

            var list = todos.Select(x => new TodoViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsDone = x.IsDone
            });

            return View("Index", list);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var entity = new Todo
            {
                UserId = user.Id,
                Name = model.Name,
                Description = model.Description,
                IsDone = model.IsDone
            };

            _todoRepository.Create(entity);

            return RedirectToAction("Index");
        }
    }
}