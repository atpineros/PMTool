using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            Effort = new HashSet<Effort>();
        }

        public int TaskId { get; set; }
        public int? ReqIdFk { get; set; }
        public int? UserIdFk { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Test { get; set; }

        public virtual Requirements ReqIdFkNavigation { get; set; }
        public virtual User UserIdFkNavigation { get; set; }
        public virtual ICollection<Effort> Effort { get; set; }
    }
}
