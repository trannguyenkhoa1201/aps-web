using Microsoft.AspNetCore.Mvc;

namespace baitap03.Controllers
{
    public class NhomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
