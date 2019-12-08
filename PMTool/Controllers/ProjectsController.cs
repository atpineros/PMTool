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

        public IActionResult Index()
        {

            ProjectViewViewModel projectViewViewModel = new ProjectViewViewModel
            {
                Projects = GetAllProjects().Result.Projects
            };
            return View(projectViewViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ProjectViewViewModel projectViewViewModel)
        {
            
            var selectedProjID = int.Parse(projectViewViewModel.SelectedProjectID);
            projectViewViewModel.Projects = GetAllProjects().Result.Projects;
            projectViewViewModel.Project = await GetProjectInfoByProjectID(selectedProjID);
            projectViewViewModel.Risks = await GetRisksByProjectID(selectedProjID);
            Task<ProjectViewViewModel> result = GetTeamsByProjectID(selectedProjID);
            projectViewViewModel.Users = result.Result.Users;
            projectViewViewModel.Roles = result.Result.Roles;          
            return View("Index", projectViewViewModel);
        }


        //should combine these methods later in one large query
        public async Task<ProjectViewViewModel> GetAllProjects()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/projectsapi");
            var projectViewViewModel = new ProjectViewViewModel();
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
            return projectViewViewModel;
        }

        public async Task<Projects> GetProjectInfoByProjectID(int projectID)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/projectsapi/{projectID}");
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

        public async Task<ProjectViewViewModel> GetTeamsByProjectID(int projectId)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/teamsapi/GetTeamByProjectID/{projectId}");
            ProjectViewViewModel result = new ProjectViewViewModel();
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<ProjectViewViewModel>(data);             
            }
            return result;
        }





        public IActionResult NewView()
        {
            var projectNewViewModel = new ProjectNewViewModel
            {
                Users = GetAllUsers().Result.Users,
                Roles = GetAllRoles().Result.Roles
            };
            return View(projectNewViewModel);
        }
        [HttpPost]
        public IActionResult NewView(ProjectNewViewModel projectNewViewModel, string projectTitle, string projectDescription, DateTime dueDate, string estimatedHours
                                        
            )
        {

            return View(projectNewViewModel);
        }

        public async Task<ProjectNewViewModel> GetAllUsers()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/users");
            var projectNewViewModel = new ProjectNewViewModel();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                var userSelectList = JsonConvert.DeserializeObject<IEnumerable<User>>(result).Select(a => new SelectListItem
                {
                    Text = a.Fname + " " + a.Lname,
                    Value = a.UserId.ToString()
                });
                projectNewViewModel.Users = new SelectList(userSelectList, "Value", "Text");
            }
            return projectNewViewModel;
        }
        public async Task<ProjectNewViewModel> GetAllRoles()
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/rolesApi");
            var projectNewViewModel = new ProjectNewViewModel();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                var roleSelectList = JsonConvert.DeserializeObject<IEnumerable<Role>>(result).Select(a => new SelectListItem
                {
                    Text = a.RoleName,
                    Value = a.RoleId.ToString()
                });
                projectNewViewModel.Roles = new SelectList(roleSelectList, "Value", "Text");
            }
            return projectNewViewModel;
        }
    }
}