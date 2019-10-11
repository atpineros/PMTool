using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMTool.Models;

namespace PMTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly prj6633t2Context _prj6633T2Context;

        public HomeController(prj6633t2Context prj6633T2Context,ILogger<HomeController> logger)
        {
            _logger = logger;
            _prj6633T2Context = prj6633T2Context;
        }

        public IActionResult Index()
        {          
          return View(_prj6633T2Context.Projects.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
