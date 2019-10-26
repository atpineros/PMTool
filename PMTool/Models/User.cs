using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class User
    {
        public User()
        {
            Tasks = new HashSet<Tasks>();
            Teams = new HashSet<Teams>();
        }

        public int UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public int? TeamId { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
    }
}
