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
    public class InternetBillGenerateDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public int InsertBillGenerate(BillGenerateVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_InternetCustomerBillGenerate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cid", _obj.cid);
                cmd.Parameters.AddWithValue("@isBatch", _obj.isBatch);
                cmd.Parameters.AddWithValue("@month", _obj.month);
                cmd.Parameters.AddWithValue("@year", _obj.year);
                cmd.Parameters.AddWithValue("@remarks", _obj.remarks);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                cmd.CommandTimeout = 0;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                result = 1;

                con.Close();
            }
            return result;
        }

        public List<BillGenerateVM> GetBillList(string month, int year )
        {
            DataTable dt = new DataTable();
            List<BillGenerateVM> billList = new List<BillGenerateVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetBills", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@year", year);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillGenerateVM()
                        {
                            companyAddress = rdr["companyAddress"].ToString(),
                            companyName = rdr["companyName"].ToString(),
                            billDetailId = Convert.ToInt32(rdr["billDetailId"]),
                            
                            month = rdr["month"].ToString(),
                            yearName = rdr["yearName"].ToString(),
                            zoneName = rdr["zoneName"].ToString(),
                            customerAddress = rdr["customerAddress"].ToString(),
                            isClosedString = rdr["isClosedString"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            billAmount = Convert.ToDecimal(rdr["billAmount"]),
                        }).ToList();
            return billList;
        }

        public int DeleteBill(int billDetailId, int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteInternetBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@billDetailId", billDetailId);
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