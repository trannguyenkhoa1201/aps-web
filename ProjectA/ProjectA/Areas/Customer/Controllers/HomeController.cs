using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ProjectA.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<SanPham> sanPham = _db.SanPham.Include("TheLoai").ToList();
            return View(sanPham);
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
        [HttpGet]
        public IActionResult Details(int sanphamId)
        {
            // Tạo giỏ hàng ở trang Details để xử lý chức năng thêm vào giỏ sau này
            GioHang giohang = new GioHang()
            {
                SanPhamId = sanphamId,
                SanPham = _db.SanPham.Include("TheLoai").FirstOrDefault(sp=> sp.Id== sanphamId),
                Quantity = 1
            }; // Mặc định số lượng ban đầu sẽ là 1
            return View(giohang);

            //SanPham sanpham = _db.SanPham.Include(sp => sp.TheLoai).FirstOrDefault(sp => sp.Id == id);
            //return View(sanpham);
        }
        [HttpPost]
        [Authorize] // Yêu cầu đăng nhập
        public IActionResult Details(GioHang giohang)
        {
            // Lấy thông tin tài khoản
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.ApplicationUserId = claim.Value;
            // Thêm sản phẩm vào giỏ hàng
            _db.GioHang.Add(giohang);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FilterByTheLoai(int id)
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include("TheLoai")
                .Where(sp => sp.TheLoai.Id == id)
                .ToList();
            return View("Index", sanpham);
        }
    }
        
}

