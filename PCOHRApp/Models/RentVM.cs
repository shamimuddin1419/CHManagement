using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class RentVM
    {
        public int? renterHouseId { get; set; }
        public int houseId { get; set; }
        public int? renterId { get; set; }
        public int? rentToMonth { get; set; }
        public int? rentToYear { get; set; }
        public int? rentFromMonth { get; set; }
        public int? rentFromYear { get; set; }
        public string houseName { get; set; }
        public string houseType { get; set; }
        public int projectId { get; set; }
        public string currentAvailability { get; set; }
       // public string rentStartDate { get; set; }
        public string rentEndDate { get; set; }
        public decimal currentRentAmount { get; set; }
        public decimal advanceAmount { get; set; }
        public decimal monthlyRent { get; set; }
        public int createdBy { get; set; }
    }
}