using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class PreviousBillInfoVM
    {
        public int? billDetailId { get; set; }
        public int cid { get; set; }
        public string customerName { get; set; }
        public string customerSerial { get; set; }
        public string requestName { get; set; }
        public decimal transactionAmount { get; set; }
        public int customerRequestId { get; set; }
        public decimal pendingAmount { get; set; }
        public decimal advanceAmount { get; set; }
        public string collectionType { get; set; }
        public string transactionMonth { get; set; }
        public string yearName { get; set; }
    }
}