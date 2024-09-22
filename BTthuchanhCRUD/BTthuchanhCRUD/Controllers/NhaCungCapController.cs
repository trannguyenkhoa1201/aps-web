using BTthuchanhCRUD.Data;
using BTthuchanhCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTthuchanhCRUD.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NhaCungCapController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var NhaCungCap = _db.NhaCungCap.ToList();
            ViewBag.NhaCungCap = NhaCungCap;
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                // them thong tin
                _db.NhaCungCap.Add(nhaCungCap);
                // luu thong tin
                _db.SaveChanges();
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
            var theloai = _db.TheLoai.Find(id);
            return View(theloai);
        }
        [HttpPost]
        public IActionResult Edit(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                // Thêm thông tin vào bảng TheLoai
                _db.NhaCungCap.Update(nhaCungCap);
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
            var NhaCungCap = _db.NhaCungCap.Find(id);
            return View(NhaCungCap);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var nhaCungCap = _db.NhaCungCap.Find(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }
            _db.NhaCungCap.Remove(nhaCungCap); _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TheLoai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _db.NhaCungCap
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }
    }
}
