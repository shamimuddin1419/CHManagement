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
    public class RentDA
    {
        private string connectionString;

        public RentDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public List<RentVM> GetAvailableRent(int? projectId = null, int? houseId = null )
        {
            DataTable dt = new DataTable();
            List<RentVM> userList = new List<RentVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getAvailableHouse", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@projectId", projectId);
                cmd.Parameters.AddWithValue("@houseId", houseId);

                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            userList = (from DataRow rdr in dt.Rows
                        select new RentVM()
                        {
                            renterId = rdr["renterId"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["renterId"]),
                            currentAvailability = rdr["currentAvailability"].ToString(),
                            houseId = Convert.ToInt32( rdr["houseId"]),
                            houseName = rdr["houseName"].ToString(),
                            houseType = rdr["houseType"].ToString(),
                            projectId = Convert.ToInt32( rdr["projectId"]),
                            rentEndDate = rdr["rentEndDate"].ToString(),
                            renterHouseId = rdr["renterHouseId"].Equals( DBNull.Value)  ? 0 : Convert.ToInt32(rdr["renterHouseId"]),
                            rentFromMonth = rdr["rentFromMonth"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["rentFromMonth"]),
                            rentFromYear = rdr["rentFromYear"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["rentFromYear"]),
                            rentToMonth = rdr["rentToMonth"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["rentToMonth"]),
                            rentToYear = rdr["rentToYear"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["rentToYear"]),
                            monthlyRent = rdr["monthlyRent"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(rdr["monthlyRent"])
                        }).ToList();
            return userList;
        }

        public int InsertRentHouse(RentVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_RentHouse", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@renterId", _obj.renterId);
                cmd.Parameters.AddWithValue("@houseId", _obj.houseId);
                cmd.Parameters.AddWithValue("@rentFromMonth", _obj.rentFromMonth);
                cmd.Parameters.AddWithValue("@rentFromYear", _obj.rentFromYear);
                cmd.Parameters.AddWithValue("@currentRentAmount", _obj.currentRentAmount);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                cmd.CommandTimeout = 0;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                result = 1;
                con.Close();
            }
            return result;
        }
    }
}