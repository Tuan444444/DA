using DA.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BaoCaoController : Controller
{
    private readonly MyDbContext _context;

    public BaoCaoController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Doanh thu: Tổng tiền
        decimal tongDoanhThu = _context.HoaDons.Sum(h => h.TongTien);

        // Thống kê tình trạng phòng
        var trangThaiPhong = _context.Phongs
            .GroupBy(p => p.TrangThai)
            .Select(g => new { TrangThai = g.Key, SoLuong = g.Count() })    
            .ToList();

        // Thống kê người thuê
        int tongNguoiThue = _context.NguoiThues.Count();

        // Gửi qua ViewBag
        ViewBag.TongDoanhThu = tongDoanhThu;
        ViewBag.TrangThaiPhong = trangThaiPhong;
        ViewBag.TongNguoiThue = tongNguoiThue;

        return View();
    }
}
