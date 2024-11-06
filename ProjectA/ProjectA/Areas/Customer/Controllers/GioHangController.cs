using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectA.Data;
using ProjectA.Models;
using System.Security.Claims;

namespace ProjectA.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GioHangController( ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            // Lấy thông tin tài khoản
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            // Lấy danh sách sản phẩm trong giỏ hàng của User
            //IEnumerable<GioHang> dsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value) .ToList();

            //return View(dsGioHang);
            // Lấy danh sách sản phẩm trong giỏ hàng của User
            GioHangViewModel giohang = new GioHangViewModel
                {
                    DsGioHang = _db.GioHang.Include("SanPham").Where(gh=>gh.ApplicationUserId == claim.Value).ToList(),
                    HoaDon = new HoaDon()
                };
            foreach (var item in giohang.DsGioHang)
            { // Tính tiền sản phẩm theo số lượng
                double ProductPrice = item.Quantity * item.SanPham.Price;
                
                // Công dồn tổng số tiền trong giỏ hàng
                giohang.HoaDon.Total += ProductPrice;
            }
            return View(giohang);

        }
        [Authorize]
        public IActionResult ThanhToan()
        {
            // Lấy thông tin tài khoản
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            // Lấy danh sách sản phẩm trong giỏ hàng của User
            GioHangViewModel giohang = new GioHangViewModel
            {
                DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList(),
                HoaDon = new HoaDon()
            };
            // Tìm thông tin tài khoản trong CSDL để hiển thị lên trang thanh toán
            giohang.HoaDon.ApplicationUser = _db.ApplicationUser.FirstOrDefault(user=> user.Id== claim.Value);
            // Gán thông tin tài khoản vào hóa đơn
            giohang.HoaDon.Name= giohang.HoaDon.ApplicationUser.Name;
            giohang.HoaDon.Address =giohang. HoaDon.ApplicationUser.Address;
            giohang.HoaDon.PhoneNumber =giohang.HoaDon.ApplicationUser.PhoneNumber;


            foreach (var item in giohang.DsGioHang)
            { // Tính tiền sản phẩm theo số lượng
                double ProductPrice = item.Quantity * item.SanPham.Price;

                // Công dồn tổng số tiền trong giỏ hàng
                giohang.HoaDon.Total += ProductPrice;
            }
            return View(giohang);

        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(GioHangViewModel giohang)
        {
            // Lấy thông tin tài khoản
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            // Cập nhật thông tin danh sách giỏ hàng và hóa đơn
            
            giohang.DsGioHang = _db.GioHang.Include("SanPham").
                Where(gh=>gh.ApplicationUserId == claim.Value).ToList();

            giohang.HoaDon.ApplicationUserId = claim.Value;
            giohang.HoaDon.OrderDate = DateTime.Now;
            giohang.HoaDon.OrderStatus = "Đang xác nhận";

            foreach (var item in giohang.DsGioHang)
            { 
                // Tính tiền sản phẩm theo số lượng
                double ProductPrice = item.Quantity * item.SanPham.Price;
                // Công dồn tổng số tiền trong giỏ hàng
                giohang.HoaDon.Total += ProductPrice;
            }
            _db.HoaDon.Add(giohang.HoaDon);
            _db.SaveChanges();
            //them thong tin chi tiet hoa don
            foreach (var item in giohang.DsGioHang)
            {
                ChiTietHoaDon chiTiethoadon = new ChiTietHoaDon()
                {
                    SanPhamId = item.SanPhamId,
                    HoaDonId = giohang.HoaDon.Id,
                    ProductPrice = item.SanPham.Price*item.Quantity,
                    Quantity = item.Quantity,

                };
                _db.ChiTietHoaDon.Add(chiTiethoadon);
                _db.SaveChanges();
            }
            //Xoa thong tin trong gio hang
            _db.GioHang.RemoveRange(giohang.DsGioHang);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Authorize]
        public IActionResult Xoa(int giohangId)
        {
            // Lấy thông tin giỏ hàng tương ứng với giohangId
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            // Xóa giỏ hàng
            _db.GioHang.Remove(giohang);
            // Lưu lại CSDL
            _db.SaveChanges();
            // Quay về trang giỏ hàng
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Tang(int giohangId)
        {
            // Lấy thông tin giỏ hàng tương ứng với giohangId
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            // Tăng số lượng sản phẩm lên 1
            giohang.Quantity += 1;
            // Lưu lại CSDL
            _db.SaveChanges();
            // Quay về trang giỏ hàng
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Giam(int giohangId)
        {
            // Lấy thông tin giỏ hàng tương ứng với giohangId
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            // Giảm số lượng sản phẩm đi 1
            giohang.Quantity -= 1;
            // Nếu số lượng = 0 thì xóa giỏ hàng
            if (giohang.Quantity == 0)
            {
                _db.GioHang.Remove(giohang);
            }
            // Lưu lại CSDL
            _db.SaveChanges();
            // Quay về trang giỏ hàng
            return RedirectToAction("Index");
        }
        
    }
}
