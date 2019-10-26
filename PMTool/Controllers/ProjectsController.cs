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
        public async Task<IActionResult> Index(ProjectViewViewModel projectViewViewModel)
        {
            var selectedProjID = int.Parse(projectViewViewModel.SelectedProjectID);
            projectViewViewModel.Project = await GetProjectByProjectID(selectedProjID);
            projectViewViewModel.Risks = await GetRisksByProjectID(selectedProjID);
            projectViewViewModel.Users = await GetTeamsByProjectID(selectedProjID);
            return RedirectToAction("Index", projectViewViewModel);
        }

        public async Task<Projects> GetProjectByProjectID(int projectID)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/projectapi/{projectID}");
            var results = new Projects();
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject<Projects>(data);

            }
            return results;
        }

        public async Task<IEnumerable<Risks>> GetRisksByProjectID(int projectId)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/risksapi/risks/{projectId}");
            IEnumerable<Risks> results = new List<Risks>();
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject<IEnumerable<Risks>>(data);
                
            }
            return results;
        }

        public async Task<IEnumerable<User>> GetTeamsByProjectID(int projectId)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/teamsapi/users/{projectId}");
            IEnumerable<User> result = new List<User>();
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<IEnumerable<User>>(data).ToList();             
            }
            return result;
        }

    }
}