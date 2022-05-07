using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class HostVM
    {
        public int hostId { get; set; }
        public string hostName { get; set; }
        public string hostAddress { get; set; }
        public string hostPhone { get; set; }
        public bool isActive { get; set; }
        public string isActiveString { get; set; }
        public int createdBy { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
    }
}