using DA.Data;
using DA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

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
            int? maTK = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTK == null) return RedirectToAction("Login", "Account");

            var nguoiThue = _context.NguoiThues.FirstOrDefault(x => x.MaTaiKhoan == maTK);
            if (nguoiThue == null) return NotFound();

            var phongData = (from hd in _context.HopDongs
                             join p in _context.Phongs on hd.MaPhong equals p.MaPhong
                             join cn in _context.ChuNhas on p.MaChuNha equals cn.MaChuNha
                             where hd.MaNguoiThue == nguoiThue.MaNguoiThue && hd.TrangThai == "Còn hiệu lực"
                             select new Phong
                             {
                                 TenPhong = p.TenPhong,
                                 LoaiPhong = p.LoaiPhong,
                                 DienTich = p.DienTich,
                                 GiaPhong = p.GiaPhong,
                                 TrangThai = p.TrangThai,
                                 
                             }).FirstOrDefault();

            if (phongData == null)
                ViewBag.Message = "Bạn hiện chưa có phòng đang thuê.";

            return View(phongData);
        }

        public IActionResult ThongTinHopDong()
        {
            int? maTaiKhoan = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTaiKhoan == null)
                return RedirectToAction("Login", "Account");

            var nguoiThue = _context.NguoiThues.FirstOrDefault(x => x.MaTaiKhoan == maTaiKhoan);
            if (nguoiThue == null)
                return NotFound();

            var hopDong = _context.HopDongs
                .Include(h => h.Phong) // lấy luôn tên phòng
                .FirstOrDefault(h => h.MaNguoiThue == nguoiThue.MaNguoiThue && h.TrangThai == "Còn hiệu lực");

            if (hopDong == null)
            {
                TempData["Message"] = "Không tìm thấy hợp đồng còn hiệu lực.";
                return View(new HopDong()); // Tránh null
            }

            return View(hopDong);
        }

        // GET: Hóa đơn
        public IActionResult HoaDon()
        {
            int? maTK = HttpContext.Session.GetInt32("MaTaiKhoan");
            if (maTK == null)
                return RedirectToAction("Login", "Account");

            // Tìm mã người thuê tương ứng
            var nguoiThue = _context.NguoiThues.FirstOrDefault(x => x.MaTaiKhoan == maTK);
            if (nguoiThue == null)
                return NotFound();

            // Lấy danh sách hóa đơn theo hợp đồng của người thuê
            var hoaDons = _context.HoaDons
                .Include(h => h.HopDong)
                    .ThenInclude(hd => hd.Phong) // 🛠️ Thêm dòng này để tránh lỗi null!
                .Where(h => h.HopDong.MaNguoiThue == nguoiThue.MaNguoiThue)
                .OrderByDescending(h => h.NgayLap)
                .ToList();

            return View(hoaDons);
        }

        public IActionResult ChiTietHoaDon(int id)
        {
            var hoaDon = _context.HoaDons
      .Include(h => h.HopDong)
          .ThenInclude(hd => hd.Phong) // <- BẮT BUỘC CÓ!
      .Include(h => h.ChiTietHoaDons)
          .ThenInclude(ct => ct.DichVu)
      .FirstOrDefault(h => h.MaHoaDon == id);

            if (hoaDon == null)
                return NotFound();

            return View(hoaDon);
        }

        public IActionResult XuatHoaDonPdf(int id)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var hoaDon = _context.HoaDons
                .Include(h => h.HopDong).ThenInclude(hd => hd.Phong)
                .Include(h => h.HopDong).ThenInclude(hd => hd.NguoiThue)
                .Include(h => h.ChiTietHoaDons).ThenInclude(ct => ct.DichVu)
                .FirstOrDefault(h => h.MaHoaDon == id);

            if (hoaDon == null)
                return NotFound();

            var stream = new MemoryStream();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("HỆ THỐNG NHÀ TRỌ ABC").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                            col.Item().Text("Địa chỉ: 123 Trọ Xanh, TP.HCM").FontSize(10);
                            col.Item().Text("Hotline: 0989 000 999").FontSize(10);
                        });

                        row.ConstantItem(100).Height(60).Image(Placeholders.Image(100, 60)); // Placeholder logo
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Spacing(10);

                        col.Item().Text($"HÓA ĐƠN THANH TOÁN").FontSize(20).Bold().FontColor(Colors.Black).AlignCenter();
                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Thông tin chung
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text($"Mã hóa đơn: {hoaDon.MaHoaDon}").SemiBold();
                                c.Item().Text($"Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy}");
                                c.Item().Text($"Trạng thái: {hoaDon.TrangThaiThanhToan}");
                            });

                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text($"Phòng: {hoaDon.HopDong?.Phong?.TenPhong ?? "N/A"}").SemiBold();
                                c.Item().Text($"Người thuê: {hoaDon.HopDong?.NguoiThue?.HoTen ?? "N/A"}");
                                c.Item().Text($"SĐT: {hoaDon.HopDong?.NguoiThue?.SoDienThoai ?? "N/A"}");
                            });
                        });

                        col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        // Bảng dịch vụ
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.ConstantColumn(40);
                                columns.ConstantColumn(80);
                                columns.ConstantColumn(80);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Dịch vụ").Bold();
                                header.Cell().Element(CellStyle).AlignRight().Text("SL").Bold();
                                header.Cell().Element(CellStyle).AlignRight().Text("Đơn giá").Bold();
                                header.Cell().Element(CellStyle).AlignRight().Text("Thành tiền").Bold();

                                static IContainer CellStyle(IContainer container) =>
                                    container.DefaultTextStyle(x => x.SemiBold()).Padding(5).Background(Colors.Grey.Lighten3).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });

                            foreach (var ct in hoaDon.ChiTietHoaDons)
                            {
                                table.Cell().PaddingVertical(5).Text(ct.DichVu?.TenDichVu ?? "N/A");
                                table.Cell().AlignRight().Text($"{ct.SoLuong}");
                                table.Cell().AlignRight().Text($"{ct.DonGia:N0} đ");
                                table.Cell().AlignRight().Text($"{ct.ThanhTien:N0} đ");
                            }
                        });

                        col.Item().AlignRight().PaddingTop(10).Text($"Tổng cộng: {hoaDon.TongTien:N0} đ")
                            .FontSize(14).Bold().FontColor(Colors.Red.Medium);
                    });

                    page.Footer().AlignCenter().Text("Cảm ơn quý khách đã sử dụng dịch vụ!").Italic().FontSize(10).FontColor(Colors.Grey.Darken1);
                });
            });

            document.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", $"HoaDon_{hoaDon.MaHoaDon}.pdf");
        }


        // GET: Phản hồi
        public IActionResult PhanHoi()
        {
            return View();
        }
    }
}
