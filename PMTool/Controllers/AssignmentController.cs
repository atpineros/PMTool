using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PMTool.Helper;
using PMTool.Models;
using PMTool.Models.ViewModels;

namespace PMTool.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly DataApi _api = new DataApi();
        private readonly prj6633t2Context _context;

        public AssignmentController(prj6633t2Context context)
        {
            _context = context;
        }

        // GET: Assignment
        public IActionResult Index()
        {
            spAssignTasks AssignModel = new spAssignTasks
            {
                Projects = GetAllProjects().Result.Projects
            };
            return View(AssignModel);
        }

        //public IActionResult Index(string id)
        //{
        //    var empRecord = dbContext.RetrieveEmployeeRecord.FromSql($ "RetrieveEmployeeRecord {id}").ToList();
        //    return View(empRecord);
        //}
        [HttpPost]
        public IActionResult Index(spAssignTasks SpAssignTasks)
        {

            var selectedProjID = SpAssignTasks.SelectedProjectID;

            var empRecord = _context.Assignments.FromSqlInterpolated($"spAssignTasks {selectedProjID}").ToList();
            SpAssignTasks.Assignments = empRecord;
            SpAssignTasks.Projects = GetAllProjects().Result.Projects;
            //spAssignTasks stuff = new spAssignTasks { Assignments = empRecord };

            return View(SpAssignTasks);
        }

        public async Task<spAssignTasks> GetAllProjects()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/projectsapi");
            var spAssignTasks = new spAssignTasks();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                var projectSelectList = JsonConvert.DeserializeObject<IEnumerable<Projects>>(result).Select(a => new SelectListItem
                {
                    Text = a.PrjName,
                    Value = a.PrjName
                });
                spAssignTasks.Projects = new SelectList(projectSelectList, "Value", "Text");
            }
            return spAssignTasks;
        }

        // GET: Assignment/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var spAssignTasks = await _context.spAssignTasks
        //        .FirstOrDefaultAsync(m => m.Title == id);
        //    if (spAssignTasks == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(spAssignTasks);
        //}

        // GET: Assignment/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Assignment/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SelectedProjectID,Title,Description,Assignee,Effort")] spAssignTasks spAssignTasks)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(spAssignTasks);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(spAssignTasks);
        //}

        //// GET: Assignment/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var spAssignTasks = await _context.spAssignTasks.FindAsync(id);
        //    if (spAssignTasks == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(spAssignTasks);
        //}

        // POST: Assignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("SelectedProjectID,Title,Description,Assignee,Effort")] spAssignTasks spAssignTasks)
        //{
        //    if (id != spAssignTasks.Title)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(spAssignTasks);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!spAssignTasksExists(spAssignTasks.Title))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(spAssignTasks);
        //}

        //// GET: Assignment/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var spAssignTasks = await _context.spAssignTasks
        //        .FirstOrDefaultAsync(m => m.Title == id);
        //    if (spAssignTasks == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(spAssignTasks);
        //}

        // POST: Assignment/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var spAssignTasks = await _context.spAssignTasks.FindAsync(id);
        //    _context.spAssignTasks.Remove(spAssignTasks);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool spAssignTasksExists(string id)
        //{
        //    return _context.spAssignTasks.Any(e => e.Title == id);
        //}
    }
}
