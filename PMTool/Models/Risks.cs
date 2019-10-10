using System;
using System.Collections.Generic;

namespace PMTool.Models
{
    public partial class Risks
    {
        public int RiskId { get; set; }
        public int? ProjIdFk { get; set; }
        public byte RiskLevel { get; set; }
        public string RiskDescription { get; set; }

        public virtual Projects ProjIdFkNavigation { get; set; }
    }
}
