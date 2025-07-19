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

    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (_context.TaiKhoans.Any(x => x.TenDangNhap == model.TenDangNhap))
        {
            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
            return View(model);
        }

        var taiKhoan = new TaiKhoan
        {
            TenDangNhap = model.TenDangNhap,
            MatKhau = model.MatKhau,
            LoaiTaiKhoan = model.LoaiTaiKhoan,
            TrangThai = "Hoạt động",
            NgayTao = DateTime.Now
        };
        _context.TaiKhoans.Add(taiKhoan);
        _context.SaveChanges();

        if (model.LoaiTaiKhoan == "ChuNha")
        {
            var cn = new ChuNha
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                HoTen = model.HoTen,
                CCCD = model.CCCD,
                SoDienThoai = model.SoDienThoai,
                Email = model.Email,
                DiaChi = model.DiaChi
            };
            _context.ChuNhas.Add(cn);
        }
        else if (model.LoaiTaiKhoan == "NguoiThue")
        {
            var nt = new NguoiThue
            {
                MaTaiKhoan = taiKhoan.MaTaiKhoan,
                HoTen = model.HoTen,
                CCCD = model.CCCD,
                SoDienThoai = model.SoDienThoai,
                Email = model.Email,
                DiaChi = model.DiaChi
            };
            _context.NguoiThues.Add(nt);
        }

        _context.SaveChanges();

        TempData["Message"] = "Đăng ký thành công!";
        return RedirectToAction("Login");
    }

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        var tk = _context.TaiKhoans
            .FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap && x.MatKhau == model.MatKhau);

        if (tk == null || tk.TrangThai == "Bị khóa")
        {
            ModelState.AddModelError("", "Đăng nhập thất bại hoặc tài khoản bị khóa");
            return View(model);
        }

        TempData["Message"] = $"Đăng nhập thành công: {tk.LoaiTaiKhoan}";

        if (tk.LoaiTaiKhoan == "Admin")
            return RedirectToAction("Index", "Admin");
        else if (tk.LoaiTaiKhoan == "ChuNha")
            return RedirectToAction("ChuNhaDashboard", "Home");
        else
            //    return RedirectToAction("NguoiThueDashboard", "Home");
            return RedirectToAction("Dashboard", "NguoiThue");
    }
}

