using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Projects
    {
        public Projects()
        {
            Requirements = new HashSet<Requirements>();
            Risks = new HashSet<Risks>();
            Teams = new HashSet<Teams>();
        }

        public int ProjId { get; set; }
        public string PrjName { get; set; }
        public DateTime PrjDueDate { get; set; }
        public string Description { get; set; }
        public string PrjMgr { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PrjEstEffort { get; set; }

        public virtual ICollection<Requirements> Requirements { get; set; }
        public virtual ICollection<Risks> Risks { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
    }
}
