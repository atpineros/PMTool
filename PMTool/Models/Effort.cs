using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Effort
    {
        public int EffortId { get; set; }
        public int? TaskIdFk { get; set; }
        public int EffortHrs { get; set; }
        public DateTime CompleteDate { get; set; }

        public virtual Tasks TaskIdFkNavigation { get; set; }
    }
}
