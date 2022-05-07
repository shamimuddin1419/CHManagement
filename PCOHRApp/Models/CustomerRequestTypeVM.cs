using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class CustomerRequestTypeVM
    {
        public int requestTypeId { get; set; }
        public string requestName { get; set; }
        public string requestTypeGroup { get; set; }
    }
}