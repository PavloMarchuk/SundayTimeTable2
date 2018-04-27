using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbLayer;
using EFCoreGenericRepository.Typed;
using EFCoreGenericRepository.Common;

namespace WUI.Controllers
{
    public class SsubjectsController : Controller
    {
        //private readonly SundayTimeTableContext _context;
		private readonly IGenericRepository<Ssubject> _rep;

		public SsubjectsController(/*SundayTimeTableContext context, */IGenericRepository<Ssubject> subjectRepository_)
        {
            //_context = context;
			_rep = subjectRepository_;
		}

        // GET: Ssubjects
        public async Task<IActionResult> Index()
        {
			return View(await _rep.GetAllAsyn());				
        }

        // GET: Ssubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			Ssubject ssubject = await _rep.GetAsync(id);
            if (ssubject == null)
            {
                return NotFound();
            }

            return View(ssubject);
        }

        // GET: Ssubjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ssubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SsubjectId,SsubjectName")] Ssubject ssubject)
        {
            if (ModelState.IsValid)
            {
				await _rep.AddAsyn(ssubject);              
                return RedirectToAction(nameof(Index));
            }
            return View(ssubject);
        }

        // GET: Ssubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ssubject = await _rep.GetAsync(id);
			
            if (ssubject == null)
            {
                return NotFound();
            }
            return View(ssubject);
        }

        // POST: Ssubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SsubjectId,SsubjectName")] Ssubject ssubject)
        {
            if (id != ssubject.SsubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					await _rep.UpdateAsyn(ssubject, id);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SsubjectExists(ssubject.SsubjectId))
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
            return View(ssubject);
        }

        // GET: Ssubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			Ssubject ssubject = await _rep.GetAsync(id);
				
            if (ssubject == null)
            {
                return NotFound();
            }

            return View(ssubject);
        }

        // POST: Ssubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {			
			await _rep.DeleteAsyn(await _rep.GetAsync(id));			
            return RedirectToAction(nameof(Index));
        }

        private bool SsubjectExists(int id)
        {
			return _rep.FindBy(s => s.SsubjectId == id).Any();				
        }
    }
}
