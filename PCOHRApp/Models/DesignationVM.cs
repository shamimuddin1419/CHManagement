using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class DesignationVM
    {
        public int designationId { get; set; }
        public string designationName { get; set; }
        public bool isActive { get; set; }
    }
}