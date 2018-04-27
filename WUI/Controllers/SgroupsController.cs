using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbLayer;

namespace WUI.Controllers
{
    public class SgroupsController : Controller
    {
        private readonly SundayTimeTableContext _context;

        public SgroupsController(SundayTimeTableContext context)
        {
            _context = context;
        }

        // GET: Sgroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sgroup.ToListAsync());
        }

        // GET: Sgroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sgroup = await _context.Sgroup
                .SingleOrDefaultAsync(m => m.SgroupId == id);
            if (sgroup == null)
            {
                return NotFound();
            }

            return View(sgroup);
        }

        // GET: Sgroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sgroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SgroupId,SgroupName,CourseName,Specialization")] Sgroup sgroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sgroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sgroup);
        }

        // GET: Sgroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sgroup = await _context.Sgroup.SingleOrDefaultAsync(m => m.SgroupId == id);
            if (sgroup == null)
            {
                return NotFound();
            }
            return View(sgroup);
        }

        // POST: Sgroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SgroupId,SgroupName,CourseName,Specialization")] Sgroup sgroup)
        {
            if (id != sgroup.SgroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sgroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SgroupExists(sgroup.SgroupId))
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
            return View(sgroup);
        }

        // GET: Sgroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sgroup = await _context.Sgroup
                .SingleOrDefaultAsync(m => m.SgroupId == id);
            if (sgroup == null)
            {
                return NotFound();
            }

            return View(sgroup);
        }

        // POST: Sgroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sgroup = await _context.Sgroup.SingleOrDefaultAsync(m => m.SgroupId == id);
            _context.Sgroup.Remove(sgroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SgroupExists(int id)
        {
            return _context.Sgroup.Any(e => e.SgroupId == id);
        }
    }
}
