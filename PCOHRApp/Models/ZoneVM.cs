using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class ZoneVM
    {
       public int zoneId { get; set; } 
       public string zoneName { get; set; }
       public bool isActive { get; set; } 
       public int createdBy  { get; set; } 

    }
}