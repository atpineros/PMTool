using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMTool.Models.ViewModels
{
    public class ProjectNewViewModel
    {
        public IEnumerable<SelectListItem> SelectedUserID { get; set; }
        public IEnumerable<SelectListItem> SelectedRoleID { get; set; }
        public IEnumerable<SelectListItem> SelectedMemeberID { get; set; }
        public Projects Project { get; set; }
        public List<User> ListOfUsers { get; set; }
        public List<Risks> Risks { get; set; }
        public User User { get; set; }
        public Risks Risk { get; set; }
        public SelectList Users { get; set; }
        public SelectList Roles { get; set; }
    }
}
