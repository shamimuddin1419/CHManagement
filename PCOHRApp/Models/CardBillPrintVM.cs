using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class CardBillPrintVM
    {
        public int customerId { get; set; }
        public string customerLocality { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string customerAddress { get; set; }
        public string ownerName { set; get; }
        public string ownerPhone { set; get; }
        public string customerSerial { set; get; }

    }
}