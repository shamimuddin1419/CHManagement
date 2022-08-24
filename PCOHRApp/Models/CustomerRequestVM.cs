using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class CustomerRequestVM
    {
        public int requestId { get; set; }
        public int cid { get; set; }
        public string customerName { get; set; }
        public int requestTypeId { get; set; }
        public string requestName { get; set; }
        public string customerPhone { get; set; }
        public decimal requestCharge { get; set; }
        public decimal updatedMontlyBill { get; set; }
        public string customerSerial { get; set; }
        public string requiredNet { get; set; }
        public string customerAddress { get; set; }
        public int hostId { get; set; }
        public int zoneId { get; set; }
        public int assignedUserId { get; set; }	
        public string remarks { get; set; }
        public bool? isOnuReturned { get; set; }
        public string newOnu { get; set; }
        public string requestMonth { get; set; }
        public int requestYear { get; set; }
        public string requestYearName { get; set; }
        public int createdBy { get; set; }
    }
}