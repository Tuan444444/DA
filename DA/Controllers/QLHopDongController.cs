using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DA.Data;
using DA.Models;

namespace DA.Controllers
{
    public class QLHopDongController : Controller
    {
        private readonly MyDbContext _context;

        public QLHopDongController(MyDbContext context)
        {
            _context = context;
        }

        // GET: HopDong
        public IActionResult Index(string search, string trangThai)
        {
            var list = _context.HopDongs
                .Include(h => h.NguoiThue)
                .Include(h => h.Phong)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(h => h.NguoiThue.HoTen.Contains(search));
            }

            if (!string.IsNullOrEmpty(trangThai))
            {
                list = list.Where(h => h.TrangThai == trangThai);
            }

            ViewBag.Search = search;
            ViewBag.TrangThai = trangThai;

            return View(list.ToList());
        }


        // GET: HopDong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .FirstOrDefaultAsync(m => m.MaHopDong == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // GET: HopDong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HopDong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHopDong,MaNguoiThue,MaPhong,NgayBatDau,NgayKetThuc,TienDatCoc,TrangThai")] HopDong hopDong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hopDong);
        }

        // GET: HopDong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }
            return View(hopDong);
        }

        // POST: HopDong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHopDong,MaNguoiThue,MaPhong,NgayBatDau,NgayKetThuc,TienDatCoc,TrangThai")] HopDong hopDong)
        {
            if (id != hopDong.MaHopDong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hopDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HopDongExists(hopDong.MaHopDong))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hopDong);
        }

        // GET: HopDong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .FirstOrDefaultAsync(m => m.MaHopDong == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // POST: HopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDongs.Remove(hopDong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HopDongExists(int id)
        {
            return _context.HopDongs.Any(e => e.MaHopDong == id);
        }
        public IActionResult Xulyvipham()
        {
            return View();
        }
        }
}
