using Microsoft.AspNetCore.Mvc;

namespace WebBackSecurity.web.Controllers
{
    public class TodoController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}