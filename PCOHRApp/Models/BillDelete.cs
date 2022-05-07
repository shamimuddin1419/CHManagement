using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class BillDelete
    {
        public int ID { set; get; }
        public string Month { set; get; }
        public string YearName { set; get; }
        public string CustomerSerial { set; get; }
        public string CustomerName { set; get; }
        public string CustomerPhone { set; get; }
        public string CustomerAddress { set; get; }
        public decimal BillAmount { set; get; }
        public string  DeletedBy { get; set; }
        public string DeletedDate { get; set; }

    }
}