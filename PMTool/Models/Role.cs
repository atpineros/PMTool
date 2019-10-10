using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Role
    {
        public Role()
        {
            Teams = new HashSet<Teams>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Teams> Teams { get; set; }
    }
}
