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
    public class LessonsController : Controller
    {
        private readonly SundayTimeTableContext _context;

        public LessonsController(SundayTimeTableContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var sundayTimeTableContext = _context.Lesson.Include(l => l.Sgroup).Include(l => l.Ssubject).Include(l => l.Teacher);
            return View(await sundayTimeTableContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .Include(l => l.Sgroup)
                .Include(l => l.Ssubject)
                .Include(l => l.Teacher)
                .SingleOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["SgroupId"] = new SelectList(_context.Sgroup, "SgroupId", "SgroupName");
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName");
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonId,LessonDatetime,Cabinet,SsubjectId,TeacherId,SgroupId")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SgroupId"] = new SelectList(_context.Sgroup, "SgroupId", "SgroupName", lesson.SgroupId);
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName", lesson.SsubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName", lesson.TeacherId);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson.SingleOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["SgroupId"] = new SelectList(_context.Sgroup, "SgroupId", "SgroupName", lesson.SgroupId);
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName", lesson.SsubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName", lesson.TeacherId);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LessonId,LessonDatetime,Cabinet,SsubjectId,TeacherId,SgroupId")] Lesson lesson)
        {
            if (id != lesson.LessonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.LessonId))
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
            ViewData["SgroupId"] = new SelectList(_context.Sgroup, "SgroupId", "SgroupName", lesson.SgroupId);
            ViewData["SsubjectId"] = new SelectList(_context.Ssubject, "SsubjectId", "SsubjectName", lesson.SsubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "TeacherId", "FirstName", lesson.TeacherId);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .Include(l => l.Sgroup)
                .Include(l => l.Ssubject)
                .Include(l => l.Teacher)
                .SingleOrDefaultAsync(m => m.LessonId == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lesson.SingleOrDefaultAsync(m => m.LessonId == id);
            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lesson.Any(e => e.LessonId == id);
        }
    }
}
