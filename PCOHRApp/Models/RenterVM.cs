using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class RenterVM
    {
        public int renterId { get; set; }
        public string renterFullInfo { get; set; }
        public string renterName { get; set; }
        public string renterPhone { get; set; }
        public string renterNID { get; set; }
        public string renterEmail { get; set; }
        public string renterEmergencyContactName { get; set; }
        public string renterEmergencyContactPhone { get; set; }
        public string renterReferenceInfo { get; set; }
        public string retnterFilePath { get; set; }
        public bool isActive { get; set; }
    }
}