using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMTool.Models;
using ProcedureTest.Models;

namespace PMTool.Controllers
{
    public class ReportingController : Controller
    {
        private readonly prj6633t2Context _context;

        public ReportingController(prj6633t2Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var empRecord = _context.spReportFinal.FromSqlInterpolated($"spReportFinal").ToList();
            return View(empRecord);
        }

        // GET: spReportFinals
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.spReportFinal.ToListAsync());
        //}

        // GET: spReportFinals/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spReportFinal = await _context.spReportFinal
                .FirstOrDefaultAsync(m => m.PrjName == id);
            if (spReportFinal == null)
            {
                return NotFound();
            }

            return View(spReportFinal);
        }

        // GET: spReportFinals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: spReportFinals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrjName,ReqID,ReqDescription,TaskName,EffortHrs,RoleName")] spReportFinal spReportFinal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spReportFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spReportFinal);
        }

        // GET: spReportFinals/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spReportFinal = await _context.spReportFinal.FindAsync(id);
            if (spReportFinal == null)
            {
                return NotFound();
            }
            return View(spReportFinal);
        }

        // POST: spReportFinals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PrjName,ReqID,ReqDescription,TaskName,EffortHrs,RoleName")] spReportFinal spReportFinal)
        {
            if (id != spReportFinal.PrjName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spReportFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!spReportFinalExists(spReportFinal.PrjName))
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
            return View(spReportFinal);
        }

        // GET: spReportFinals/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spReportFinal = await _context.spReportFinal
                .FirstOrDefaultAsync(m => m.PrjName == id);
            if (spReportFinal == null)
            {
                return NotFound();
            }

            return View(spReportFinal);
        }

        // POST: spReportFinals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var spReportFinal = await _context.spReportFinal.FindAsync(id);
            _context.spReportFinal.Remove(spReportFinal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool spReportFinalExists(string id)
        {
            return _context.spReportFinal.Any(e => e.PrjName == id);
        }
    }
}
