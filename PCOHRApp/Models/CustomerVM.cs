using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class CustomerVM
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public int insertFlag { get; set; }
        public string customerId { get; set; }
        public int customerSerialId{get;set;}
        public string customerSerial { get; set; }
		public string customerName { get; set; }	
		public string customerPhone { get; set; }
		public string customerAddress { get; set; }	
		public string requiredNet { get; set; }	
		public string ipAddress { get; set; }	
		public int hostId { get; set; }	
		public int zoneId { get; set; }
        public string hostName { get; set; }
        public string hostAddress { get; set; }
        public string hostPhone { get; set; }
        public string zoneName { get; set; }
        public string assignedUserName { get; set; }
		public int assignedUserId { get; set; }	
		public decimal connFee { get; set; }	
		public decimal monthBill { get; set; }	
		public decimal othersAmount { get; set; }	
		public string description { get; set; }
		public string connMonth { get; set; }	
		public int connYear { get; set; }
        public string connYearName { get; set; }
		public bool isActive { get; set; }
        public string isActiveString { get; set; }
        public string isDisconnectedString { get; set; }
		public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string Password { get; set; }
        public string EntryDateString { set; get; }
        public DateTime? EntryDate { set; get; }
        public string onuMCId { set; get; }
        public string nid { get; set; }
        public string disconnectedDateString { get; set; }
    }
}