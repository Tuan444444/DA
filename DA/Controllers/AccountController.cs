using Microsoft.AspNetCore.Mvc;
using DA.Models; // Namespace bạn
using DA.ViewModels;
using System.Linq;
using DA.Data;

public class AccountController : Controller
{
    private readonly MyDbContext _context;

    public AccountController(MyDbContext context)
    {
        _context = context;
    }

    // Hiển thị form đăng ký
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var exists = _context.TaiKhoans.Any(x => x.TenDangNhap == model.TenDangNhap);
        if (exists)
        {
            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
            return View(model);
        }

        var tk = new TaiKhoan
        {
            TenDangNhap = model.TenDangNhap,
            MatKhau = model.MatKhau,
            VaiTro = model.VaiTro,
            MaChuNha = model.VaiTro == "ChuNha" ? model.MaChuNha : null,
            MaNguoiThue = model.VaiTro == "NguoiThue" ? model.MaNguoiThue : null
        };

        _context.TaiKhoans.Add(tk);
        _context.SaveChanges();

        TempData["Message"] = "Đăng ký thành công!";
        return RedirectToAction("Login");
    }

    // Hiển thị form đăng nhập
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var tk = _context.TaiKhoans.FirstOrDefault(x =>
            x.TenDangNhap == model.TenDangNhap && x.MatKhau == model.MatKhau);

        if (tk == null)
        {
            ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu.");
            return View();
        }

        TempData["Message"] = $"Đăng nhập thành công với vai trò: {tk.VaiTro}";

        if (tk.VaiTro == "ChuNha")
            return RedirectToAction("DashboardChuNha", "Home");
        else if (tk.VaiTro == "NguoiThue")
            return RedirectToAction("DashboardNguoiThue", "Home");
        else if (tk.VaiTro == "Admin")
            return RedirectToAction("Index", "Admin");


            return RedirectToAction("Login");
    }
}
