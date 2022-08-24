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
    public class InternetCustomerRequestDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public int InsertCustomerRequest(CustomerRequestVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_InternetCustomerRequest", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cid", _obj.cid);
                cmd.Parameters.AddWithValue("@requestTypeId", _obj.requestTypeId);
                cmd.Parameters.AddWithValue("@requestCharge", _obj.requestCharge);
                cmd.Parameters.AddWithValue("@updatedMontlyBill", _obj.updatedMontlyBill);
                cmd.Parameters.AddWithValue("@requiredNet", _obj.requiredNet);
                cmd.Parameters.AddWithValue("@hostId", _obj.hostId);
                cmd.Parameters.AddWithValue("@zoneId", _obj.zoneId);
                cmd.Parameters.AddWithValue("@customerAddress", _obj.customerAddress);
                cmd.Parameters.AddWithValue("@assignedUserId", _obj.assignedUserId);
                cmd.Parameters.AddWithValue("@remarks", _obj.remarks);
                cmd.Parameters.AddWithValue("@isOnuReturned", _obj.@isOnuReturned);
                cmd.Parameters.AddWithValue("@newOnu", _obj.newOnu);
                cmd.Parameters.AddWithValue("@requestMonth", _obj.requestMonth);
                cmd.Parameters.AddWithValue("@requestYear", _obj.requestYear);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                result = 1;

                con.Close();
            }
            return result;
        }

        public List<CustomerRequestVM> GetCustomerRequestList()
        {
            DataTable dt = new DataTable();
            List<CustomerRequestVM> requestList = new List<CustomerRequestVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetCustomerRequests", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            requestList = (from DataRow rdr in dt.Rows
                           select new CustomerRequestVM()
                        {
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            requestId = Convert.ToInt32(rdr["requestId"]),
                            requestCharge = Convert.ToDecimal(rdr["requestCharge"]),
                            requestMonth = rdr["requestMonth"].ToString(),
                            requestTypeId = Convert.ToInt32(rdr["requestTypeId"]),
                            requestName = rdr["requestName"].ToString(),
                            requestYear = Convert.ToInt32(rdr["requestYear"]),
                            requestYearName = rdr["requestYearName"].ToString(),
                            requiredNet = rdr["requiredNet"].ToString(),
                            remarks = rdr["remarks"].ToString(),
                            updatedMontlyBill = Convert.ToDecimal(rdr["updatedMontlyBill"]),
                        }).ToList();
            return requestList;
        }
        public int DeleteCustomerRequest(int requestId,int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteInternetCustomerRequest", con);
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
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}