using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Teams
    {
        public int TeamId { get; set; }
        public int? RoleIdFk { get; set; }
        public int? PriIdFk { get; set; }
        public int? UserIdFk { get; set; }

        public virtual Projects PriIdFkNavigation { get; set; }
        public virtual Role RoleIdFkNavigation { get; set; }
        public virtual User UserIdFkNavigation { get; set; }
    }
}
