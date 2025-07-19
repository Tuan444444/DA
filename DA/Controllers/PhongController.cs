using Microsoft.AspNetCore.Mvc;
using DA.Data;
using DA.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DA.Controllers
{
    public class PhongController : Controller
    {
        private readonly MyDbContext _context;

        public PhongController(MyDbContext context)
        {
            _context = context;
        }

        // Danh sách phòng
        public IActionResult Index(string search, string trangThai)
        {
            var dsPhong = _context.Phongs.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                dsPhong = dsPhong.Where(p => p.TenPhong.Contains(search));
            }

            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
            {
                dsPhong = dsPhong.Where(p => p.TrangThai == trangThai);
            }

            ViewBag.Search = search;
            ViewBag.TrangThai = trangThai;

            return View(dsPhong.ToList());
        }


        // Tạo mới
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Phong phong)
        {
            if (ModelState.IsValid)
            {
                _context.Phongs.Add(phong);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phong);
        }

        // Sửa
        public IActionResult Edit(int id)
        {
            var phong = _context.Phongs.Find(id);
            return View(phong);
        }

        [HttpPost]
        public IActionResult Edit(Phong phong)
        {
            if (ModelState.IsValid)
            {
                _context.Phongs.Update(phong);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phong);
        }

        // Xóa
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

        // Quản lý lịch sử lưu trú
        public IActionResult LichSu(int maPhong)
        {
            var ls = _context.LichSuLuuTrus
                             .Where(x => x.MaPhong == maPhong)
                             .Include(x => x.NguoiThue)
                             .ToList();
            return View(ls);
        }

    }
}
