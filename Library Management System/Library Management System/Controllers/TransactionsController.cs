using Microsoft.AspNetCore.Mvc;
using Library_Management_System.Models;

namespace LibraryManagementSystem.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly LibraryManagementSystemContext _context;

        public TransactionsController(LibraryManagementSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var transactions = _context.Transactions
                .Join(_context.Users, t => t.UserId, u => u.UserId, (t, u) => new { t, u })
                .Join(_context.Books, tu => tu.t.BookId, b => b.BookId, (tu, b) => new
                {
                    tu.t.TransactionId,
                    tu.u.Name,
                    b.Title,
                    tu.t.IssueDate,
                    tu.t.DueDate,
                    tu.t.ReturnDate,
                    tu.t.Status
                }).ToList();

            return View(transactions);
        }

        public IActionResult Create()
        {
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Books = _context.Books.Where(b => b.AvailableCopies > 0).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Find(transaction.BookId);
                if (book == null || book.AvailableCopies <= 0)
                {
                    ModelState.AddModelError("", "Book not available");
                    return View(transaction);
                }

                book.AvailableCopies--;
                transaction.IssueDate = DateOnly.FromDateTime(DateTime.Now);
                transaction.DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(14));
                transaction.Status = "Issued";

                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }
    }
}

