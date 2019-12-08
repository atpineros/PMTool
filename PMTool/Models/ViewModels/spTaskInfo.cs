using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PMTool.Models.ViewModels
{
    public class spTaskInfo
    {

        [Key]
        public string SelectedProjectID { get; set; }

        public int SelectedProjectid { get; set; }

        public string SelectedRequirementID { get; set; }
        public SelectList Projects { get; set; }

        public SelectList Requirements { get; set; }

        public Projects Project { get; set; }

        public Requirements Requirementx { get; set; }

        public IEnumerable<Requirement> Requirement { get; set; }
        public List<Projects> projectModels { get; set; }



    }
}