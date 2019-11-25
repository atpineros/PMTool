using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PMTool.Models.ViewModels
{
    public class spAssignTasks
    {
        public string SelectedProjectID { get; set; }
        public SelectList Projects { get; set; }

        public Projects Project { get; set; }

        public IEnumerable<Assignments> Assignments { get; set; }



    }
}