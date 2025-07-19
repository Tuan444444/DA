using Microsoft.AspNetCore.Mvc;

namespace DA.Controllers
{
    public class NguoiThueController : Controller
    {
        public IActionResult Dashboard()
        {
            // return View();
            return View("Dashboard");
        }

        public IActionResult ThongTinCaNhan()
        {
            return View();
        }

        public IActionResult TaiKhoan()
        {
            return View();
        }

        public IActionResult LuuTru()
        {
            return View();
        }

        public IActionResult HoaDon()
        {
            return View();
        }
    }
}
