using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class ProjectVM
    {
        public List<CareTakerVM> careTakers;
        public int projectId {get;set;}
        public string rptCompanyName { get; set; }
        public string rptCompanyAddress { get; set; }
	    public string projectName{get;set;}
	    public string projectAddress {get;set;}
	    public string projectDescription{get;set;}
	    public string projectType{get;set;}
	    public string apartmentBuildingType{get;set;}
        public DateTime createdDate { get; set; }
        public int createdBy { get; set; }
        public List<int> careTakerIds { get; set; }
    }
}