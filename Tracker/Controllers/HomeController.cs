using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tracker.Models;

namespace Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TrackerDbContext _context;

        // Inject both ILogger and TrackerDbContext into the constructor
        public HomeController(ILogger<HomeController> logger, TrackerDbContext context)
        {
            _logger = logger;
            _context = context; // Assign the injected context to the private field
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense()
        {
            var allExpense = _context.Expenses.ToList();

            var totalExpense=allExpense.Sum(x=>x.Value);

            ViewBag.Expense = totalExpense;
            return View(allExpense);
        }

        public IActionResult ExpenCreateEditExpensese()
        {
            return View();
        }

        public IActionResult CreateEditExpense(int? id)
        {
            var expenseInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expenseInDb);
        }
        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb= _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expense");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            // Add logic to handle the submitted model, EX: saving it to the database
            if (model.Id == 0)
            {
                _context.Expenses.Add(model);
            }
            else
            {
                _context.Expenses.Update(model);
            }
             
            _context.SaveChanges();

            return RedirectToAction("Expense");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
