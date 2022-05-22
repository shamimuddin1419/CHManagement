using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCOHRApp.Models
{
    public class CustomerCardInfoVM
    {
        public int customerId { get; set; }
        public string customerSerial { get; set; }
        public string customerName { get; set; }
        public string customerLocality { get; set; }
        public string customerPhone { get; set; }
        public string customerAddress { get; set; }
        public string ownerName { get; set; }
        public string ownerPhone { get; set; }
        public int createdBy { get; set; }
        public int insertFlag { get; set; }
    }
}
