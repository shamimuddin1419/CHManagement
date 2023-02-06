using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class HouseVM
    {
        public string rptCompanyName { get; set; }
        public string rptCompanyAddress { get; set; }
        public int houseId { get; set; }
        public string houseName { get; set; }
        public string meterNo { get; set; }
        public int projectId { get; set; }
        public string houseType { get; set; }
        public decimal monthlyRent { get; set; }
        public string description { get; set; }
        public int createdBy { get; set; }
        public DateTime MyProperty { get; set; }
        public string projectName { get; set; }
    }
}