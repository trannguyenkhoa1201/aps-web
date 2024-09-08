using Microsoft.AspNetCore.Mvc;

namespace BaiTapVN.Controllers
{
    public class Tuan02Controller : Controller
    {

        public IActionResult Index()
        {
            ViewBag.HoTen = "Trần Nguyên Khoa";
            ViewBag.MSSV = "1822041106";
            ViewBag.Nam = 2024;
            return View();
        }

        public IActionResult MayTinh(int a, int b, string pheptinh)
        {
            double ketqua = 0;
            switch (pheptinh)
            {
                case "cong":
                    ketqua = a + b;
                    break;
                case "tru":
                    ketqua = a - b;
                    break;
                case "nhan":
                    ketqua = a * b;
                    break;
                case "chia":
                    if (b != 0)
                        ketqua = (double)a / b;
                    else
                        ViewBag.Error = "Không thể chia cho 0";
                    break;
                default:
                    ViewBag.Error = "Phép tính không hợp lệ";
                    break;
            }
            ViewBag.KetQua = ketqua;
            return View();
        }

        public IActionResult Profile()
        {
            ViewBag.HoTen = "Trần Nguyên Khoa";
            ViewBag.MSSV = "1822041106";
            ViewBag.Nam = 2024;
            return View();
        }
    }
}
