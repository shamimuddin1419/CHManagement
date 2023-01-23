using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class CurrnetHouseRenterVM
    {
        public int renterHouseId { get; set; }
        public string connectionMonth { get; set; }
        public string renterName { get; set; }
        public string renterPhone { get; set; }
        public string renterNID { get; set; }
        public decimal currentRentAmount { get; set; }
        public string houseType { get; set; }
        public string caretakerName { get; set; }
    }
}