using baitap05.Models;
using Microsoft.AspNetCore.Mvc;

namespace baitap05.Controllers
{
    public class TheLoaiController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Ngay = "Ngay 12";
            ViewBag.Thang = "Thang 01";
            ViewData["nam"] = "nam 2004";
            return View();
        }

        public IActionResult Index2()
        {
            var theloai = new TheLoaiViewModel 
            { 
                Id = 1,
                Name = "Trinh Tham"
            };  
            return View(theloai);
        }
    }
}
