using DA.Data;
using DA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PhongController : Controller
{
    private readonly MyDbContext _context;

    public PhongController(MyDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string search, string trangThai)
    {
        var query = _context.Phongs.Include(p => p.Phong_DichVus).ThenInclude(pd => pd.DichVu).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.TenPhong.Contains(search));
        }

        if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
        {
            query = query.Where(p => p.TrangThai == trangThai);
        }

        ViewBag.Search = search;
        ViewBag.TrangThai = trangThai;

        return View(query.ToList());
    }

    public IActionResult Create()
    {
        ViewBag.DichVus = _context.DichVus.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Phong phong, int[] selectedDichVus)
    {
        if (ModelState.IsValid)
        {
            _context.Phongs.Add(phong);
            _context.SaveChanges();

            foreach (var maDichVu in selectedDichVus)
            {
                var phongDichVu = new Phong_DichVu
                {
                    MaPhong = phong.MaPhong,
                    MaDichVu = maDichVu,
                    NgayApDung = DateTime.Now
                };
                _context.Phong_DichVus.Add(phongDichVu);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.DichVus = _context.DichVus.ToList();
        return View(phong);
    }

   
    // SỬA PHÒNG - GET
    public IActionResult Edit(int id)
    {
        var phong = _context.Phongs
                            .Include(p => p.Phong_DichVus)
                            .FirstOrDefault(p => p.MaPhong == id);
        if (phong == null)
        {
            return NotFound();
        }

        ViewBag.DichVus = _context.DichVus.ToList();
        ViewBag.SelectedDichVus = phong.Phong_DichVus.Select(x => x.MaDichVu).ToList();

        return View(phong);
    }

    // SỬA PHÒNG - POST
    [HttpPost]
    public IActionResult Edit(Phong phong, int[] selectedDichVus)
    {
        if (ModelState.IsValid)
        {
            _context.Phongs.Update(phong);
            _context.SaveChanges();

            // Xóa dịch vụ cũ
            var old = _context.Phong_DichVus.Where(x => x.MaPhong == phong.MaPhong);
            _context.Phong_DichVus.RemoveRange(old);
            _context.SaveChanges();

            // Thêm mới dịch vụ đã chọn
            foreach (var maDichVu in selectedDichVus)
            {
                var pdv = new Phong_DichVu
                {
                    MaPhong = phong.MaPhong,
                    MaDichVu = maDichVu,
                    NgayApDung = DateTime.Now
                };
                _context.Phong_DichVus.Add(pdv);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        return View(phong);
    }

    // XÓA PHÒNG
    public IActionResult Delete(int id)
    {
        var phong = _context.Phongs.Find(id);
        if (phong != null)
        {
            _context.Phongs.Remove(phong);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // CẬP NHẬT TRẠNG THÁI
    public IActionResult UpdateStatus(int id, string newStatus)
    {
        var phong = _context.Phongs.Find(id);
        if (phong != null)
        {
            phong.TrangThai = newStatus;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

}
