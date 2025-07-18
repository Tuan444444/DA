using Microsoft.AspNetCore.Mvc;

namespace DA.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
