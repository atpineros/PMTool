using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PMTool.Helper;
using PMTool.Models;
using PMTool.Models.ViewModels;

namespace PMTool.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly DataApi _api = new DataApi();
        
        public async Task<IActionResult> Index()
        {
            //List<Projects> project = new List<Projects>();
            ProjectViewViewModel projectViewViewModel = new ProjectViewViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/projectsapi");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                var projectSelectList = JsonConvert.DeserializeObject<IEnumerable<Projects>>(result).Select(a => new SelectListItem
                {
                    Text = a.PrjName,
                    Value = a.ProjId.ToString()
                });
                projectViewViewModel.Projects = new SelectList(projectSelectList, "Value", "Text");
            }

            return View(projectViewViewModel);
        }
        [HttpPost]
        public IActionResult Index(ProjectViewViewModel projectViewViewModel)
        {

            var item = projectViewViewModel.SelectedProjectID;
            return RedirectToAction("Index");
        }
    }
}