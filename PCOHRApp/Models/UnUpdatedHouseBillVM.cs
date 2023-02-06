using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class UnUpdatedHouseBillVM
    {
        public int billDetailId { get; set; }
        public string unPaidMonth { get; set; }
        public decimal billAmount { get; set; }
        public decimal gasCharge { get; set; }
        public decimal electricityCharge { get; set; }
        public decimal waterCharge { get; set; }
        public decimal otherCharge { get; set; }
        public decimal serviceCharge { get; set; }
    }
}