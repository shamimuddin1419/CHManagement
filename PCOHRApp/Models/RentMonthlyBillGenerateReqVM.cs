using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class RentMonthlyBillGenerateReqVM
    {
       
        public int? renterHouseId { get; set; }
        public bool isBatch { get; set; }
        public string month { get; set; }
        public int year { get; set; }
        public string remarks { get; set; }
        public int createdBy { get; set; }
    }

}