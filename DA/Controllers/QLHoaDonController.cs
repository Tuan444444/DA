using DA.Data;
using DA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class QLHoaDonController : Controller
{
    private readonly MyDbContext _context;

    public QLHoaDonController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var ds = _context.HoaDons
            .Include(h => h.HopDong)
            .ToList();
        return View(ds);
    }

    public IActionResult Generate()
    {
        var hopDongs = _context.HopDongs
            .Include(h => h.Phong)
            .Include(h => h.Phong.Phong_DichVus)
            .ThenInclude(pd => pd.DichVu)
            .Where(h => h.TrangThai == "Còn hiệu lực")
            .ToList();

        foreach (var hd in hopDongs)
        {
            decimal tongTien = hd.Phong.GiaPhong;

            var hoaDon = new HoaDon
            {
                MaHopDong = hd.MaHopDong,
                NgayLap = DateTime.Now,
                TrangThaiThanhToan = "Chưa thanh toán"
            };

            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();

            foreach (var pd in hd.Phong.Phong_DichVus)
            {
                int soLuong = 50; // Demo: Giả định

                var ct = new ChiTietHoaDon
                {
                    MaHoaDon = hoaDon.MaHoaDon,
                    MaDichVu = pd.MaDichVu,
                    SoLuong = soLuong,
                    DonGia = pd.DichVu.DonGia,
                    ThanhTien = soLuong * pd.DichVu.DonGia
                };

                tongTien += ct.ThanhTien;

                _context.ChiTietHoaDons.Add(ct);
            }

            hoaDon.TongTien = tongTien;
            _context.SaveChanges();
        }

        TempData["Message"] = "Xuất hoá đơn thành công!";
        return RedirectToAction("Index");
    }
}
