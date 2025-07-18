using DA.Data;
using DA.Models;
using DA.ViewModels;

using Microsoft.AspNetCore.Mvc;
using System;

namespace DA.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyDbContext _context;

        public AccountController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.VaiTro == "ChuNha")
                {
                    var chuNha = new ChuNha
                    {
                        HoTen = model.HoTen,
                        SDT = model.SDT,
                        Email = model.Email,
                        DiaChi = model.DiaChi
                    };
                    _context.ChuNhas.Add(chuNha);
                    _context.SaveChanges();

                    var taiKhoan = new TaiKhoan
                    {
                        TenDangNhap = model.TenDangNhap,
                        MatKhau = model.MatKhau,
                        VaiTro = "ChuNha",
                        MaChuNha = chuNha.MaChuNha
                    };
                    _context.TaiKhoans.Add(taiKhoan);
                    _context.SaveChanges();
                }
                else if (model.VaiTro == "NguoiThue")
                {
                    var nguoiThue = new NguoiThue
                    {
                        HoTen = model.HoTen,
                        SDT = model.SDT,
                        Email = model.Email,
                        CCCD = model.CCCD
                    };
                    _context.NguoiThues.Add(nguoiThue);
                    _context.SaveChanges();

                    var taiKhoan = new TaiKhoan
                    {
                        TenDangNhap = model.TenDangNhap,
                        MatKhau = model.MatKhau,
                        VaiTro = "NguoiThue",
                        MaNguoiThue = nguoiThue.MaNguoiThue
                    };
                    _context.TaiKhoans.Add(taiKhoan);
                    _context.SaveChanges();
                }

                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login", "Account"); // <-- Phải có dòng này
            }

            // Nếu ModelState không hợp lệ
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.TaiKhoans
                    .FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap && u.MatKhau == model.MatKhau);

                if (user != null)
                {
                    HttpContext.Session.SetString("Username", user.TenDangNhap);
                    HttpContext.Session.SetString("Role", user.VaiTro);

                    // Chuyển đến giao diện phù hợp
                    if (user.VaiTro == "Admin")
                        return RedirectToAction("Index", "Admin");
                    else if (user.VaiTro == "ChuNha")
                        return RedirectToAction("Dashboard", "ChuNha");
                    else
                        return RedirectToAction("Dashboard", "NguoiThue");
                }

                TempData["Error"] = "Sai tên đăng nhập hoặc mật khẩu";
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }



}
