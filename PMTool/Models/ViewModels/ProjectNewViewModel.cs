using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models.ViewModels
{
    public class ProjectNewViewModel
    {
        public string SelectedUserID { get; set; }
        public string SelectedRoleID { get; set; }
        public string SelectedMemeberID { get; set; }
        public Projects Project { get; set; }
        public List<User> ListOfUsers { get; set; }
        public List<Risks> Risks { get; set; }
        public User User { get; set; }
        public Risks Risk { get; set; }
        public SelectList Users { get; set; }
        public SelectList Roles { get; set; }
    }
}
