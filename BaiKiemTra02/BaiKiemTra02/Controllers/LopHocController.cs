using BaiKiemTra02.Data;
using BaiKiemTra02.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiKiemTra02.Controllers
{
    public class LopHocController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LopHocController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var lopHoc = _db.LopHoc.ToList();
            ViewBag.LopHoc = lopHoc;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                // Thêm thông tin vào bảng TheLoai
                _db.LopHoc.Add(lopHoc);
                // Lưu lại
                _db.SaveChanges();
                // Chuyển trang về index
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.LopHoc.Find(id);
            return View(theloai);
        }

        [HttpPost]
        public IActionResult Edit(LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                // Thêm thông tin vào bảng TheLoai
                _db.LopHoc.Update(lopHoc);
                // Lưu lại
                _db.SaveChanges();
                // Chuyển trang về index
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.LopHoc.Find(id);
            return View(theloai);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var theloai = _db.LopHoc.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            _db.LopHoc.Remove(theloai); _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.LopHoc.Find(id);
            return View(theloai);
        }
        [HttpGet]
        public IActionResult Search(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Sử dụng LINQ để tìm kiếm
                var lopHoc = _db.LopHoc.Where(tl => tl.TenLopHoc.Contains(SearchString)).ToList();
                ViewBag.TheLoai = lopHoc;
            }
            else
            {
                var lopHoc = _db.LopHoc.ToList();
                ViewBag.LopHoc = lopHoc;
            }
            return View("Index"); // Sử dụng lại View Index
        }

    }
}
