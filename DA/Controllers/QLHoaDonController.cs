using DA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DA.Models;
using System;
using Xceed.Words.NET;
using DA.Data;
using Xceed.Document.NET;

namespace DA.Controllers
{
    public class QLHoaDonController : Controller
    {
        private readonly MyDbContext _context;

        public QLHoaDonController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ds = await _context.HoaDons.Include(h => h.HopDong).ToListAsync();
            return View(ds);
        }

        public IActionResult Create()
        {
            ViewBag.HopDongs = _context.HopDongs.Where(h => h.TrangThai == "Còn hiệu lực").ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int maHopDong)
        {
            var hopDong = await _context.HopDongs
                .Include(h => h.Phong)
                .FirstOrDefaultAsync(h => h.MaHopDong == maHopDong);

            var dichVus = await _context.Phong_DichVus
                .Where(pd => pd.MaPhong == hopDong.MaPhong)
                .Include(pd => pd.DichVu)
                .ToListAsync();

            var hoaDon = new HoaDon
            {
                MaHopDong = maHopDong,
                NgayLap = DateTime.Now,
                TrangThaiThanhToan = "Chưa thanh toán"
            };

            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            decimal tongTien = 0;

            foreach (var dv in dichVus)
            {
                var chiTiet = new ChiTietHoaDon
                {
                    MaHoaDon = hoaDon.MaHoaDon,
                    MaDichVu = dv.MaDichVu,
                    SoLuong = 1,
                    DonGia = dv.DichVu.DonGia,
                    ThanhTien = dv.DichVu.DonGia
                };
                tongTien += chiTiet.ThanhTien;
                _context.ChiTietHoaDons.Add(chiTiet);
            }

            hoaDon.TongTien = tongTien;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.ChiTietHoaDons).ThenInclude(ct => ct.DichVu)
                .Include(h => h.HopDong).ThenInclude(h => h.NguoiThue)
                .FirstOrDefaultAsync(h => h.MaHoaDon == id);

            if (hoaDon == null) return NotFound();

            return View(hoaDon);
        }

        public async Task<IActionResult> ExportToWord(int id)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.ChiTietHoaDons).ThenInclude(ct => ct.DichVu)
                .Include(h => h.HopDong).ThenInclude(h => h.NguoiThue)
                .FirstOrDefaultAsync(h => h.MaHoaDon == id);

            if (hoaDon == null) return NotFound();

            var fileName = $"HoaDon_{hoaDon.MaHoaDon}.docx";

            using (var ms = new MemoryStream())
            {
                using (var doc = DocX.Create(ms))
                {
                    doc.InsertParagraph($"HÓA ĐƠN #{hoaDon.MaHoaDon}");
                    doc.InsertParagraph($"Ngày lập: {hoaDon.NgayLap:dd/MM/yyyy}");
                    doc.InsertParagraph($"Khách hàng: {hoaDon.HopDong.NguoiThue.HoTen}");

                    doc.InsertParagraph("\nChi tiết:");
                    var table = doc.AddTable(hoaDon.ChiTietHoaDons.Count + 1, 4);
                    table.Design = TableDesign.ColorfulList;

                    table.Rows[0].Cells[0].Paragraphs[0].Append("Dịch vụ");
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Số lượng");
                    table.Rows[0].Cells[2].Paragraphs[0].Append("Đơn giá");
                    table.Rows[0].Cells[3].Paragraphs[0].Append("Thành tiền");

                    int i = 1;
                    foreach (var ct in hoaDon.ChiTietHoaDons)
                    {
                        table.Rows[i].Cells[0].Paragraphs[0].Append(ct.DichVu.TenDichVu);
                        table.Rows[i].Cells[1].Paragraphs[0].Append(ct.SoLuong.ToString());
                        table.Rows[i].Cells[2].Paragraphs[0].Append(ct.DonGia.ToString("N0"));
                        table.Rows[i].Cells[3].Paragraphs[0].Append(ct.ThanhTien.ToString("N0"));
                        i++;
                    }

                    doc.InsertTable(table);
                    doc.InsertParagraph($"\nTổng tiền: {hoaDon.TongTien:N0} VNĐ");
                    doc.Save();
                }
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
            }
        }
    }
}
