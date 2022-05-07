using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class UserVM
    {
        public int userId { get; set; }
        public string password { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public bool isAdmin { get; set; }
        public bool isCableUser { get; set; }
        public bool isHouseRentUser { get; set; }
        public int designationId { get; set; }
        public string userFullName { get; set; }
        public string designationName { get; set; }
        public bool isActive { get; set; }
        public string userType { get; set; }
        public int createdBy { get; set; }
        public bool isManagement { get; set; }

    }
}