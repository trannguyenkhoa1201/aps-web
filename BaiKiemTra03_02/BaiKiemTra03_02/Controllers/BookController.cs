using BaiKiemTra03_02.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaiKiemTra03_02.Models;


namespace BaiKiemTra03_02.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var books = _db.Book.Include(b => b.Author).ToList();
            ViewBag.Books = books;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors = _db.Author.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = a.AuthorName
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Add(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var book = _db.Book.Find(id);
            ViewBag.Authors = _db.Author.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = a.AuthorName
            }).ToList();

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Update(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var book = _db.Book.Find(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var book = _db.Book.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var book = _db.Book.Include(b => b.Author).FirstOrDefault(b => b.BookId == id);
            return View(book);
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var books = _db.Book.Include(b => b.Author)
                    .Where(b => b.Title.Contains(searchString)).ToList();
                ViewBag.Books = books;
            }
            else
            {
                var books = _db.Book.Include(b => b.Author).ToList();
                ViewBag.Books = books;
            }
            return View("Index");
        }
    }
}
