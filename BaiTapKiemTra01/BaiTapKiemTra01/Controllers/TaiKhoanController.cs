using BaiTapKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapKiemTra01.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DangKy(TaiKhoanViewModel taiKhoan)
        {
            if (taiKhoan != null && taiKhoan.Password != null && (taiKhoan.Password).Length >0)
            {
                taiKhoan.Password = taiKhoan.Password + "chuoi_ma_hoa";
            }
            return View(taiKhoan);
        }

        public IActionResult SanPham()
        {
            ViewBag.TenSanPham= "phone";
            ViewBag.GiaSanPham = "5000000";

            return View();
        }
    }
}
