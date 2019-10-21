using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models.ViewModels
{
    public class ProjectViewViewModel
    {
        public int SelectedProjectID { get; set; }
        public IEnumerable<Projects> projects { get; set; }
        public Teams team { get; set; }
        public Risks risk { get; set; }
    }
}
