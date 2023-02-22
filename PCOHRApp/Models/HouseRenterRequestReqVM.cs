using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class HouseRenterRequestReqVM
    {
        public int renterHouseId { get;  set; }
        public int requestTypeId { get;  set; }
        public decimal requestCharge { get;  set; }
        public int requestMonth { get;  set; }
        public int requestYear { get;  set; }
        public decimal updatedMonthlyRent { get;  set; }
        public string remarks { get;  set; }
    }
}