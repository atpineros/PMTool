using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models.ViewModels
{
    public class ProjectViewViewModel
    {
        public string SelectedProjectID { get; set; }
        public SelectList Projects { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Risks> Risks { get; set; }
        public Projects Project { get; set; }

    }
}
