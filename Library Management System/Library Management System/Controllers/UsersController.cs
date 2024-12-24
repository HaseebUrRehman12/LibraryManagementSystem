using Microsoft.AspNetCore.Mvc;
using Library_Management_System.Models;

namespace LibraryManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public UsersController(LibraryManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Details(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }
    }
}

