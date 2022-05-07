using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class CareTakerVM
    {
        public string rptCompanyName {get;set;}
		public string rptCompanyAddress {get;set;}
		public int careTakerId {get;set;}
		public string careTakerName {get;set;}
		public string presentAddress {get;set;}
		public string permanentAddress {get;set;}
		public string phoneNo {get;set;}
		public string email {get;set;}
		public string nid {get;set;}
		public DateTime? joiningDate {get;set;}
        public string joiningDateString { get; set; }
		public decimal? salary {get;set;}
		public bool isActive {get;set;}
        public string isActiveString { get; set; }
		public int createdBy {get;set;}
        public DateTime createdDate { get; set; }
    }
}