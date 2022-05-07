using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class LordInfoVM
    {
        public string rptCompanyName { get; set; }
        public string rptCompanyAddress { get; set; }
        public int lordId { get; set; }
        public string ownerName { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string phoneNo { get; set; }
        public string email { get; set; }
        public string nid { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }
}