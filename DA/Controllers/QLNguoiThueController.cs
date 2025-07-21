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
    public class QLNguoiThueController : Controller
    {
        private readonly MyDbContext _context;

        public QLNguoiThueController(MyDbContext context)
        {
            _context = context;
        }

        // GET: NguoiThue
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.NguoiThues.Include(n => n.TaiKhoan);
            return View(await myDbContext.ToListAsync());
        }

        // GET: NguoiThue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiThue = await _context.NguoiThues
                .Include(n => n.TaiKhoan)
                .FirstOrDefaultAsync(m => m.MaNguoiThue == id);
            if (nguoiThue == null)
            {
                return NotFound();
            }

            return View(nguoiThue);
        }

        // GET: NguoiThue/Create
        public IActionResult Create()
        {
            ViewData["MaTaiKhoan"] = new SelectList(_context.TaiKhoans, "MaTaiKhoan", "LoaiTaiKhoan");
            return View();
        }

        // POST: NguoiThue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNguoiThue,MaTaiKhoan,HoTen,CCCD,SoDienThoai,Email,DiaChi")] NguoiThue nguoiThue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nguoiThue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaTaiKhoan"] = new SelectList(_context.TaiKhoans, "MaTaiKhoan", "LoaiTaiKhoan", nguoiThue.MaTaiKhoan);
            return View(nguoiThue);
        }

        // GET: NguoiThue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiThue = await _context.NguoiThues.FindAsync(id);
            if (nguoiThue == null)
            {
                return NotFound();
            }
            ViewData["MaTaiKhoan"] = new SelectList(_context.TaiKhoans, "MaTaiKhoan", "LoaiTaiKhoan", nguoiThue.MaTaiKhoan);
            return View(nguoiThue);
        }

        // POST: NguoiThue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNguoiThue,MaTaiKhoan,HoTen,CCCD,SoDienThoai,Email,DiaChi")] NguoiThue nguoiThue)
        {
            if (id != nguoiThue.MaNguoiThue)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nguoiThue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiThueExists(nguoiThue.MaNguoiThue))
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
            ViewData["MaTaiKhoan"] = new SelectList(_context.TaiKhoans, "MaTaiKhoan", "LoaiTaiKhoan", nguoiThue.MaTaiKhoan);
            return View(nguoiThue);
        }

        // GET: NguoiThue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nguoiThue = await _context.NguoiThues
                .Include(n => n.TaiKhoan)
                .FirstOrDefaultAsync(m => m.MaNguoiThue == id);
            if (nguoiThue == null)
            {
                return NotFound();
            }

            return View(nguoiThue);
        }

        // POST: NguoiThue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nguoiThue = await _context.NguoiThues.FindAsync(id);
            if (nguoiThue != null)
            {
                _context.NguoiThues.Remove(nguoiThue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiThueExists(int id)
        {
            return _context.NguoiThues.Any(e => e.MaNguoiThue == id);
        }
    }
}
