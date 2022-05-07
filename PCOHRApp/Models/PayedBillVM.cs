using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class PayedBillVM
    {
        public decimal billAmount{get;set;}
        public string customerSL { get; set; }
        public string pageNo { get; set; }
        public string receivedDateString { get; set; }
        public string receivedByString { get; set; }
        public string month { get; set; }
        public decimal yearName { get; set; }
    }
}