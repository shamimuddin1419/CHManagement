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
    public class RenterDA
    {
        private string connectionString;
        public RenterDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public List<RenterVM> GetRenterList()
        {
            DataTable dt = new DataTable();
            List<RenterVM> userList = new List<RenterVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getRenters", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            userList = (from DataRow rdr in dt.Rows
                        select new RenterVM()
                        {
                            renterId = Convert.ToInt32(rdr["renterId"]),
                            renterName = rdr["renterName"].ToString(),
                            renterFullInfo = rdr["renterFullInfo"].ToString(),
                            renterPhone = rdr["renterPhone"].ToString(),
                            renterEmail = rdr["renterEmail"].ToString(),
                            renterEmergencyContactName = rdr["renterEmergencyContactName"].ToString(),
                            renterEmergencyContactPhone = rdr["renterEmergencyContactPhone"].ToString(),
                            renterNID = rdr["renterNID"].ToString(),
                            renterReferenceInfo = rdr["renterReferenceInfo"].ToString(),
                            retnterFilePath = rdr["retnterFilePath"].ToString(),
                            isActive = Convert.ToBoolean(rdr["isActive"])
                        }).ToList();
            return userList;
        }

        public CurrnetHouseRenterVM GetCurrentHouseRenter(int houseId,int effectiveMonth, int effectiveYear)
        {
            DataTable dt = new DataTable();
            CurrnetHouseRenterVM result = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getCurrentRenterByHouse", con);
                cmd.Parameters.AddWithValue("@houseId", houseId);
                cmd.Parameters.AddWithValue("@effectiveMonth", effectiveMonth);
                cmd.Parameters.AddWithValue("@effectiveYear", effectiveYear);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                result = new CurrnetHouseRenterVM
                {
                    renterHouseId = Convert.ToInt32(row["renterHouseId"]),
                    caretakerName = row["renterName"].ToString(),
                    connectionMonth = row["connectionMonth"].ToString(),
                    currentRentAmount = Convert.ToDecimal(row["currentRentAmount"]),
                    houseType = row["houseType"].ToString(),
                    renterName = row["renterName"].ToString(),
                    renterNID = row["renterNID"].ToString(),
                    renterPhone = row["renterPhone"].ToString(),
                };
            }
            return result;
        }
    }
}