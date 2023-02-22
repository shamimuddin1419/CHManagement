using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class HouseRenterBillCollectionVM
    {
        public string companyAddress { get; set; }
        public string companyName { get; set; }
        public int collectionId { get; set; }
        public int billDetailId { get; set; }
        public int renterHouseId { get; set; }
        public int month { get; set; }
        public int yearId { get; set; }
        public string billMonth { get; set; }
        public decimal payableAmount { get; set; }
        public decimal rcvAmount { get; set; }
        public decimal adjustAdvance { get; set; }
        public decimal discount { get; set; }
        public string description { get; set; }
        public DateTime collectionDate { get; set; }
        public string paymentRef { get; set; }
        public string collectedDateString { get; set; }
        public string renterName { get; set; }
        public string houseName { get; set; }
        public string renterNID { get; set; }
        public string renterPhone { get; set; }
        public string houseType { get; set; }
        public decimal advanceAmount { get; set; }
        public decimal rentAmount { get; set; }
        public decimal gasCharge { get; set; }
        public decimal electricityCharge { get; set; }
        public decimal waterCharge { get; set; }
        public decimal serviceCharge { get; set; }
        public decimal otherCharge { get; set; }
        public string meterNo { get; set; }
        public string renterEmail { get; set; }

    }
}