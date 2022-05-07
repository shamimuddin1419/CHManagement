using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCOHRApp.Models
{
    public class DashBoardDataVM
    {
            public int totalActiveUsers ;
			public int totalInactiveUser ;
			public decimal discontinuedUserThisMonth ;
			public decimal generatedBillThisMonth ;
			public decimal collectedThisMonth ;
			public decimal todaysCollectedAmount ;
    }
}