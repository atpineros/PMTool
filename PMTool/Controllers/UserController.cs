using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMTool.Controllers.Api;
using PMTool.Helper;
using PMTool.Models;

namespace PMTool.Controllers
{
    public class UserController : Controller
    {
        private readonly DataApi _api = new DataApi();
        public IActionResult Index()
        {
            return View();
        }
    }
}