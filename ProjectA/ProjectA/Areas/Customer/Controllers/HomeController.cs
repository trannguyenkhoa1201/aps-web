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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(string searchString, string sortOrder)
        {
            // Get products and include category information
            var sanPham = _db.SanPham.Include(sp => sp.TheLoai).AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPham = sanPham.Where(sp => sp.Name.Contains(searchString));
            }

            // Apply sorting based on the sortOrder
            sanPham = sortOrder switch
            {
                "name_desc" => sanPham.OrderByDescending(sp => sp.Name),
                "price" => sanPham.OrderBy(sp => sp.Price),
                "price_desc" => sanPham.OrderByDescending(sp => sp.Price),
                _ => sanPham.OrderBy(sp => sp.Name),
            };

            return View(sanPham.ToList());
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
            // Initialize a shopping cart item with default quantity
            var giohang = new GioHang
            {
                SanPhamId = sanphamId,
                SanPham = _db.SanPham.Include(sp => sp.TheLoai).FirstOrDefault(sp => sp.Id == sanphamId),
                Quantity = 1 // Default quantity set to 1
            };
            return View(giohang);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(GioHang giohang)
        {
            // Retrieve user information
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.ApplicationUserId = claim?.Value;

            // Check if the product is already in the cart
            var giohangdb = _db.GioHang.FirstOrDefault(gh => gh.SanPhamId == giohang.SanPhamId &&
                                                             gh.ApplicationUserId == giohang.ApplicationUserId);

            if (giohangdb == null)
            {
                // Add product to cart if it's not already present
                _db.GioHang.Add(giohang);
            }
            else
            {
                // If present, increase the quantity
                giohangdb.Quantity += giohang.Quantity;
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FilterByTheLoai(int id)
        {
            var sanpham = _db.SanPham.Include(sp => sp.TheLoai)
                                     .Where(sp => sp.TheLoai.Id == id)
                                     .ToList();
            return View("Index", sanpham);
        }


        public IActionResult NhaCungCap()
        {
            // Lấy danh sách nhà cung cấp
            var nhaCungCapList = _db.NhaCungCap.ToList();
            return View(nhaCungCapList);
        }
        public IActionResult DetailsNhaCungCap(int id)
        {
            var nhaCungCap = _db.NhaCungCap.Find(id);

            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }
        public IActionResult GioiThieu()
        {
            return View();
        }
        public IActionResult TrangChu()
        {
            // Lấy danh sách sản phẩm và bao gồm thông tin thể loại
            var sanPham = _db.SanPham.Include(sp => sp.TheLoai).ToList();
            return View(sanPham);
        }

    }
}
