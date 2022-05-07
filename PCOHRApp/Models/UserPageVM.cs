using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class UserPageVM
    {
        public int pageId { get; set; }
        public string pageName { get; set; }
        public string pageUrl { get; set; }
        public bool isPermitted { get; set; }
        public string pageType { get; set; }
        public string pageSubType { get; set; }
    }
}