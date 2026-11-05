using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_HW___5.Models;
using Web_HW___5.ViewModels;

namespace Web_HW___5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Task 1: In-memory lists and seed data
        private static List<Author> authors = new List<Author>
        {
            new Author { Id = 1, Name = "J.K. Rowling", Country = "United Kingdom" },
            new Author { Id = 2, Name = "George R.R. Martin", Country = "United States" }
        };

        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", Price = 15.99m, AuthorId = 1 },
            new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", Price = 18.50m, AuthorId = 1 },
            new Book { Id = 3, Title = "A Game of Thrones", Price = 24.99m, AuthorId = 2 }
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Task 2: Index action building HomeViewModel
        public IActionResult Index()
        {
            var bookCardViewModels = books.Select(b => new BookCardViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                AuthorName = authors.FirstOrDefault(a => a.Id == b.AuthorId)?.Name ?? "Unknown"
            }).ToList();

            var viewModel = new HomeViewModel
            {
                Books = bookCardViewModels,
                Authors = authors
            };

            return View(viewModel);
        }

        // Task 3: About and Contact actions
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // Task 5: POST action to add a book with auto-generated ID (last Id + 1)
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                int newId = books.Any() ? books.Max(b => b.Id) + 1 : 1;
                books.Add(new Book
                {
                    Id = newId,
                    Title = title,
                    Price = price,
                    AuthorId = authorId
                });
            }
            return RedirectToAction("Index");
        }

        // Task 5: POST action to add an author with auto-generated ID
        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                int newId = authors.Any() ? authors.Max(a => a.Id) + 1 : 1;
                authors.Add(new Author
                {
                    Id = newId,
                    Name = name,
                    Country = country ?? string.Empty
                });
            }
            return RedirectToAction("Index");
        }

        // Task 5: POST action to remove a book by ID
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}