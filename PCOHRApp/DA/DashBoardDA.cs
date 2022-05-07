using PCOHRApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PCOHRApp.DA
{
    public class DashBoardDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DashBoardDataVM GetDishDashboarddata()
        {
            DashBoardDataVM _obj = new DashBoardDataVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getDishDashboarddata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _obj.totalActiveUsers = Convert.ToInt32(rdr["totalActiveUsers"]);
                    _obj.totalInactiveUser = Convert.ToInt32(rdr["totalInactiveUser"]);
                    _obj.discontinuedUserThisMonth = Convert.ToDecimal(rdr["discontinuedUserThisMonth"]);
                    _obj.generatedBillThisMonth = Convert.ToDecimal(rdr["generatedBillThisMonth"]);
                    _obj.collectedThisMonth = Convert.ToDecimal(rdr["collectedThisMonth"]);
                    _obj.todaysCollectedAmount = Convert.ToDecimal(rdr["todaysCollectedAmount"]);
                }
                con.Close();
            }
            return _obj;
        }

        public DashBoardDataVM GetInternetDashboarddata()
        {
            DashBoardDataVM _obj = new DashBoardDataVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetDashboarddata", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _obj.totalActiveUsers = Convert.ToInt32(rdr["totalActiveUsers"]);
                    _obj.totalInactiveUser = Convert.ToInt32(rdr["totalInactiveUser"]);
                    _obj.discontinuedUserThisMonth = Convert.ToDecimal(rdr["discontinuedUserThisMonth"]);
                    _obj.generatedBillThisMonth = Convert.ToDecimal(rdr["generatedBillThisMonth"]);
                    _obj.collectedThisMonth = Convert.ToDecimal(rdr["collectedThisMonth"]);
                    _obj.todaysCollectedAmount = Convert.ToDecimal(rdr["todaysCollectedAmount"]);
                }
                con.Close();
            }
            return _obj;
        }
    }
}