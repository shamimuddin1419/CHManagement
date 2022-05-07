using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class BillGenerateVM
    {
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string zoneName { get; set; }
        public string customerAddress { get; set; }
        public string isClosedString { get; set; }
        public int billDetailId { get; set; }
        public int? cid { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public bool isBatch { get; set; }
        public string month { get; set; }
        public string yearName { get; set; }
        public decimal billAmount { get; set; }
        public string customerSerial { get; set; }
        public int year { get; set; }
        public string remarks { get; set; }
        public int createdBy { get; set; }


    }
}