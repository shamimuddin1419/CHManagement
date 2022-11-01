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
    }
}