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
    public class RequirementController : Controller
    {
        private readonly DataApi _api = new DataApi();
        private readonly prj6633t2Context _context;

        public RequirementController(prj6633t2Context context)
        {
            _context = context;
        }

        // GET: Assignment
        public IActionResult Index()
        {
            spTaskInfo AssignModel = new spTaskInfo
            {
                Projects = GetAllProjects().Result.Projects,
                //Requirements = GetAllRequirements().Result.Requirements
            };
            return View(AssignModel);
        }

        //public IActionResult Index(string id)
        //{
        //    var empRecord = dbContext.RetrieveEmployeeRecord.FromSql($ "RetrieveEmployeeRecord {id}").ToList();
        //    return View(empRecord);
        //}
        [HttpPost]
        public IActionResult Index(spTaskInfo SpTaskInfo)
        {

            var selectedProjID = SpTaskInfo.SelectedProjectID;
            var selectedReqID = SpTaskInfo.SelectedRequirementID;

            var empRecord = _context.Requirement.FromSqlInterpolated($"spTaskInfo {selectedProjID}, {selectedReqID}").ToList();
            SpTaskInfo.Requirement = empRecord;
            var allInfo = GetAllProjects();
            SpTaskInfo.Projects = allInfo.Result.Projects;
            SpTaskInfo.projectModels = allInfo.Result.projectModels;
            SpTaskInfo.Requirements = GetAllRequirements(SpTaskInfo).Result.Requirements;
            //spAssignTasks stuff = new spAssignTasks { Assignments = empRecord };

            return View(SpTaskInfo);
        }

        public async Task<spTaskInfo> GetAllProjects()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/projectsapi");
            var spTaskInfo = new spTaskInfo();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                var projectModels = JsonConvert.DeserializeObject<IEnumerable<Projects>>(result).Select(a => a).ToList();
                var projectSelectList = JsonConvert.DeserializeObject<IEnumerable<Projects>>(result).Select(a => new SelectListItem
                {
                    Text = a.PrjName,
                    Value = a.PrjName
                });
                spTaskInfo.Projects = new SelectList(projectSelectList, "Value", "Text");
                spTaskInfo.projectModels = projectModels;
            }
            return spTaskInfo;
        }

        public async Task<spTaskInfo> GetAllRequirements(spTaskInfo SpTaskInfo)
        {
            var selectedProjID = SpTaskInfo.SelectedProjectID;
            int projID = SpTaskInfo.projectModels.FirstOrDefault(x => x.PrjName == selectedProjID).ProjId;
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/requirementapi/projId/{projID}");
            var spTaskInfo = new spTaskInfo();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                var requirementSelectList = JsonConvert.DeserializeObject<IEnumerable<Requirements>>(result).Select(a => new SelectListItem
                {
                    Text = a.ReqDescription,
                    Value = a.ReqDescription
                });
                spTaskInfo.Requirements = new SelectList(requirementSelectList, "Value", "Text");
            }
            return spTaskInfo;
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
