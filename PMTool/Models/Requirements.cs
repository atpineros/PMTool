using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Requirements
    {
        public Requirements()
        {
            Tasks = new HashSet<Tasks>();
        }

        public int ReqId { get; set; }
        public int? PrjIdFk { get; set; }
        public string ReqDescription { get; set; }
        public bool Functional { get; set; }
        public int EstEffort { get; set; }
        public DateTime? CompleteDate { get; set; }

        public virtual Projects PrjIdFkNavigation { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
