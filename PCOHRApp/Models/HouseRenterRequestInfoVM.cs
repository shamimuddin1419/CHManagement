using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class HouseRenterRequestInfoVM
    {
        public int renterHouseId { get;  set; }
        public string houseName { get;  set; }
        public string renterName { get;  set; }
        public string renterPhone { get; set; }
        public int requestId { get;  set; }
        public decimal requestCharge { get;  set; }
        public string requestMonth { get;  set; }
        public int requestTypeId { get;  set; }
        public string requestName { get;  set; }
        public int requestYear { get;  set; }
        public string requestYearName { get;  set; }
        public string remarks { get; set; }
        public decimal updatedMonthlyRent { get; set; }
        public string renterNID { get; set; }
    }
}