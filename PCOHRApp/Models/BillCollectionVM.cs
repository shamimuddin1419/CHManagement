using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class BillCollectionVM
    {
        public int collectionId{get;set;}
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string voucherNo { get; set; }
        public int cid { get; set; }
        public string customerSerial { get; set; }
        public decimal connFee{get;set;}
        public decimal reConnFee {get;set;}
        public decimal othersAmount{get;set;}
        public decimal monthlyFee{get;set;}
        public string fromMonth{get;set;}
        public string toMonth{get;set;}
        public decimal shiftingCharge{get;set;}
        public string description{get;set;}
        public decimal netAmount{get;set;}
        public decimal rcvAmount{get;set;}
        public decimal adjustAdvance{get;set;}
        public decimal collectionAmount { get; set; }
        public decimal discount { get; set; }
        public decimal totalDue { get; set; }
        public string customerSL{get;set;}
        public string pageNo{get;set;}
        public int? collectedBy{get;set;}
        public string fromMonthYear { get; set; }
        public string toMonthYear { get; set; }
        public int? receivedBy{get;set;}
        public int createdBy{get;set;}
        public DateTime collectionDate { get; set; }
        public string collectedDateString { get; set; }
        public string createdDateString { get; set; }
        public string collectedByString { get; set; }
        public string receivedByString { get; set; }
        public DateTime createdDate { get; set; }
        public string stringCreatedDate { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string zoneName { get; set; }
        public string receivedDateString { get; set; }
        public string Password { get; set; }
        public List<int> billDetailsIds { get; set; }
        public string  createdByString { get; set; }
        public string yearName { set; get; }
        public string customerAddress { set; get; }
       

    }
}