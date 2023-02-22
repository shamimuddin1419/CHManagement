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
    public class HouseRenterRequestDA
    {
        private readonly string connectionString;
        public HouseRenterRequestDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public int InsertRequest(HouseRenterRequestReqVM _obj,int createdBy)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_HouseRenterRequest", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@renterHouseId", _obj.renterHouseId);
                cmd.Parameters.AddWithValue("@remarks", _obj.remarks);
                cmd.Parameters.AddWithValue("@requestCharge", _obj.requestCharge);
                cmd.Parameters.AddWithValue("@requestTypeId", _obj.requestTypeId);
                cmd.Parameters.AddWithValue("@requestYear", _obj.requestYear);
                cmd.Parameters.AddWithValue("@requestMonth", _obj.requestMonth);
                cmd.Parameters.AddWithValue("@updatedMonthlyRent", _obj.updatedMonthlyRent);
               
                cmd.Parameters.AddWithValue("@createdBy", createdBy);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                result = 1;

                con.Close();
            }
            return result;
        }
        public List<HouseRenterRequestInfoVM> GetRequestList()
        {
            DataTable dt = new DataTable();
            List<HouseRenterRequestInfoVM> requestList = new List<HouseRenterRequestInfoVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getHouseRenterRequests", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            requestList = (from DataRow rdr in dt.Rows
                           select new HouseRenterRequestInfoVM()
                           {
                               houseName = rdr["houseName"].ToString(),
                               renterHouseId = Convert.ToInt32( rdr["renterHouseId"]),
                               renterName = rdr["renterName"].ToString(),
                               renterPhone = rdr["renterPhone"].ToString(),
                               requestId = Convert.ToInt32(rdr["requestId"]),
                               requestCharge = Convert.ToDecimal(rdr["requestCharge"]),
                               requestMonth = rdr["requestMonth"].ToString(),
                               requestTypeId = Convert.ToInt32(rdr["requestTypeId"]),
                               requestName = rdr["requestName"].ToString(),
                               requestYear = Convert.ToInt32(rdr["requestYear"]),
                               requestYearName = rdr["requestYearName"].ToString(),
                               remarks = rdr["remarks"].ToString(),
                               updatedMonthlyRent = Convert.ToDecimal(rdr["updatedMonthlyRent"]),
                               renterNID = rdr["renterNID"].ToString()
                           }).ToList();
            return requestList;
        }
        public int DeleteRequest(int requestId, int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteHouseRenterRequest", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@requestId", requestId);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    result = 1;

                    con.Close();

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}