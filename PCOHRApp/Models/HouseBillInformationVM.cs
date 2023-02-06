using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class HouseBillInformationVM
    {
        public int billDetailId { get; set; }
        public string unPaidMonth { get; set; }
        public decimal billAmount { get; set; }
        public decimal gasCharge { get; set; }
        public decimal electricityCharge { get; set; }
        public decimal waterCharge { get; set; }
        public decimal otherCharge { get; set; }
        public decimal serviceCharge { get; set; }
        public string renterName { get; set; }
        public string renterEmail { get; set; }
        public string renterPhone { get; set; }
        public string renterEmergencyContactName { get; set; }
        public string renterEmergencyContactPhone { get; set; }
        public string renterNID { get; set; }
        public string houseName { get; set; }
        public string houseType { get; set; }
        public string meterNo { get; set; }
        public string rentStartFrom { get; set; }
        public string rentEndTo { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
    }
}