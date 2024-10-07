using BaiKiemTra03_02.Data;
using Microsoft.AspNetCore.Mvc;
using BaiKiemTra03_02.Models;

namespace BaiKiemTra03_02.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var authors = _db.Author.ToList();
            ViewBag.Authors = authors;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Author.Add(author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var author = _db.Author.Find(id);
            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Author.Update(author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var author = _db.Author.Find(id);
            return View(author);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var author = _db.Author.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            _db.Author.Remove(author);
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
            var author = _db.Author.Find(id);
            return View(author);
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var authors = _db.Author.Where(a => a.AuthorName.Contains(searchString)).ToList();
                ViewBag.Authors = authors;
            }
            else
            {
                var authors = _db.Author.ToList();
                ViewBag.Authors = authors;
            }
            return View("Index");
        }
    }
}
