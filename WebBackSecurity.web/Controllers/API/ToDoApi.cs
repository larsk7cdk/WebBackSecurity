using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBackSecurity.web.Data.Entities;
using WebBackSecurity.web.Data.Repositories;
using WebBackSecurity.web.ViewModels.Todo;

namespace WebBackSecurity.web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [IgnoreAntiforgeryToken]
    public class ToDoApiController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ToDoApiController(UserManager<IdentityUser> userManager, ITodoRepository todoRepository)
        {
            _userManager = userManager;
            _todoRepository = todoRepository;
        }

        // LIST
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type.EndsWith("emailaddress"))?.Value;

            if (email == null)
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(email);


            var entities = await _todoRepository.GetAllByIdAsync(user.Id);


            if (entities.Count == 0)
                return Ok();

            var todos = entities.Select(MapToViewModel);

            return Ok(todos);
        }

        // DETAILS
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details([Required]int id)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type.EndsWith("emailaddress"))?.Value;

            if (email == null)
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(email);

            var entity = await _todoRepository.GetByIdAsync(user.Id, id);

            if (entity == null) return NotFound();

            var todo = MapToViewModel(entity);

            return Ok(todo);
        }

        // MAPPER
        private static TodoViewModel MapToViewModel(Todo entity)
        {
            return new TodoViewModel
            {
                Id = entity.Id,
                CreatedDateTime = entity.CreatedDateTime,
                Name = entity.Name,
                Description = entity.Description,
                IsDone = entity.IsDone
            };
        }
    }
}