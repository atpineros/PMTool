using System;
using System.ComponentModel.DataAnnotations;

namespace PMTool.Models
{
    public class Assignments
    {
        [Key]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public int Effort { get; set; }

    }
}