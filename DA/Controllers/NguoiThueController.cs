using Microsoft.AspNetCore.Mvc;
using DA.Data;
using DA.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DA.Controllers
{
    public class NguoiThueController : Controller
    {
        private readonly MyDbContext _context;

        public NguoiThueController(MyDbContext context)
        {
            _context = context;
        }

        // Dashboard chính
        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: Thông tin cá nhân
        public IActionResult ThongTinCaNhan()
        {
            int? maTK = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTK == null)
                return RedirectToAction("Login", "Account");

            var nguoiThue = _context.NguoiThues.FirstOrDefault(x => x.MaTaiKhoan == maTK);
            if (nguoiThue == null)
                return NotFound();

            return View(nguoiThue);
        }

        // POST: Cập nhật thông tin cá nhân
        [HttpPost]
        public IActionResult ThongTinCaNhan(NguoiThue model)
        {
            Console.WriteLine("🔍 Nhận từ form: MaNguoiThue = " + model.MaNguoiThue);
            if (!ModelState.IsValid)
                return View(model);

            var nguoiThue = _context.NguoiThues.FirstOrDefault(x => x.MaNguoiThue == model.MaNguoiThue);
            if (nguoiThue == null)
            {
                Console.WriteLine("❌ Không tìm thấy người thuê với MaNguoiThue = " + model.MaNguoiThue);
                return NotFound();
            }

            Console.WriteLine("Hiển thị thông tin cho MaNguoiThue = " + nguoiThue.MaNguoiThue);

            // Cập nhật thủ công để tránh mất liên kết hoặc ghi đè không mong muốn
            nguoiThue.HoTen = model.HoTen;
            nguoiThue.CCCD = model.CCCD;
            nguoiThue.SoDienThoai = model.SoDienThoai;
            nguoiThue.Email = model.Email;
            nguoiThue.DiaChi = model.DiaChi;

            _context.SaveChanges();
            Console.WriteLine("✅ Dữ liệu đã cập nhật");

            TempData["Message"] = "Cập nhật thành công!";
            return RedirectToAction("ThongTinCaNhan");
        }

        // GET: Thông tin tài khoản (nếu bạn muốn hiển thị)
        public IActionResult TaiKhoan()
        {
            int? maTK = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTK == null)
                return RedirectToAction("Login", "Account");

            var taiKhoan = _context.TaiKhoans.FirstOrDefault(x => x.MaTaiKhoan == maTK);
            if (taiKhoan == null)
                return NotFound();

            return View(taiKhoan);
        
        }
        [HttpPost]
       

        [HttpPost]
        public IActionResult DoiMatKhau(string MatKhauCu, string MatKhauMoi, string XacNhanMatKhau)
        {
            int? maTK = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTK == null)
                return RedirectToAction("Login", "Account");

            var taiKhoan = _context.TaiKhoans.FirstOrDefault(x => x.MaTaiKhoan == maTK);
            if (taiKhoan == null)
                return NotFound();

            if (taiKhoan.MatKhau != MatKhauCu)
            {
                TempData["Message"] = "❌ Mật khẩu cũ không đúng!";
                return RedirectToAction("ThongTinCaNhan");
            }

            if (MatKhauMoi != XacNhanMatKhau)
            {
                TempData["Message"] = "❌ Mật khẩu xác nhận không khớp!";
                return RedirectToAction("ThongTinCaNhan");
            }

            taiKhoan.MatKhau = MatKhauMoi;
            _context.SaveChanges();

            TempData["Message"] = "✅ Đổi mật khẩu thành công!";
            return RedirectToAction("ThongTinCaNhan");
        }


        // GET: Thông tin lưu trú → sub-menu
        public IActionResult ThongTinPhong()
        {
            // TODO: Lấy thông tin phòng theo mã người thuê
            return View();
        }

        public IActionResult ThongTinHopDong()
        {
            // TODO: Lấy hợp đồng theo mã người thuê
            return View();
        }

        // GET: Hóa đơn
        public IActionResult HoaDon()
        {
            // TODO: Lấy hóa đơn theo mã người thuê (qua hợp đồng)
            return View();
        }

        // GET: Phản hồi
        public IActionResult PhanHoi()
        {
            return View();
        }
    }
}
