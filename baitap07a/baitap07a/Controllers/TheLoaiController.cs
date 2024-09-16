using baitap07a.Data;
using baitap07a.Models;
using Microsoft.AspNetCore.Mvc;

namespace baitap07a.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var Theloai = _db.TheLoai.ToList();
            ViewBag.Theloai = Theloai;
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            

            return View();
        }
        [HttpPost]
        public IActionResult Create(TheLoai theLoai)
        {
            // them thong tin
            _db.TheLoai.Add(theLoai);
            // luu thong tin
            _db.SaveChanges();
            

            return View();
        }
    }
}
