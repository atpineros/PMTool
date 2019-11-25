using System;
using System.ComponentModel.DataAnnotations;

namespace ProcedureTest.Models
{
    public class spReportFinal
    {
        [Key]
        public string ReqDescription { get; set; }
        public string PrjName { get; set; }
        public string RoleName { get; set; }
        public int TotalEffort { get; set; }

    }
}