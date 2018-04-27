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
    public class TeacherInSubjectsController : Controller
    {
        private readonly SundayTimeTableContext _context;

        public TeacherInSubjectsController(SundayTimeTableContext context)
        {
            _context = context;
        }

        // GET: TeacherInSubjects
        public async Task<IActionResult> Index()
        {
            var sundayTimeTableContext = _context.TeacherInSubject.Include(t => t.Ssubject).Include(t => t.Teacher);
            return View(await sundayTimeTableContext.ToListAsync());
        }

        // GET: TeacherInSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherInSubject = await _context.TeacherInSubject
                .Include(t => t.Ssubject)
                .Include(t => t.Teacher)
                .SingleOrDefaultAsync(m => m.TeacherInSubjectId == id);
            if (teacherInSubject == null)
            {
                return NotFound();
            }

            return View(teacherInSubject);
        }

        // GET: TeacherInSubjects/Create
        public IActionResult Create()
        {
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName");
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName");
            return View();
        }

        // POST: TeacherInSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherInSubjectId,TeacherId,SsubjectId")] TeacherInSubject teacherInSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherInSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName", teacherInSubject.SsubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName", teacherInSubject.TeacherId);
            return View(teacherInSubject);
        }

        // GET: TeacherInSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherInSubject = await _context.TeacherInSubject.SingleOrDefaultAsync(m => m.TeacherInSubjectId == id);
            if (teacherInSubject == null)
            {
                return NotFound();
            }
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName", teacherInSubject.SsubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName", teacherInSubject.TeacherId);
            return View(teacherInSubject);
        }

        // POST: TeacherInSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherInSubjectId,TeacherId,SsubjectId")] TeacherInSubject teacherInSubject)
        {
            if (id != teacherInSubject.TeacherInSubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherInSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherInSubjectExists(teacherInSubject.TeacherInSubjectId))
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
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName", teacherInSubject.SsubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName", teacherInSubject.TeacherId);
            return View(teacherInSubject);
        }

        // GET: TeacherInSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherInSubject = await _context.TeacherInSubject
                .Include(t => t.Ssubject)
                .Include(t => t.Teacher)
                .SingleOrDefaultAsync(m => m.TeacherInSubjectId == id);
            if (teacherInSubject == null)
            {
                return NotFound();
            }

            return View(teacherInSubject);
        }

        // POST: TeacherInSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherInSubject = await _context.TeacherInSubject.SingleOrDefaultAsync(m => m.TeacherInSubjectId == id);
            _context.TeacherInSubject.Remove(teacherInSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherInSubjectExists(int id)
        {
            return _context.TeacherInSubject.Any(e => e.TeacherInSubjectId == id);
        }
    }
}
