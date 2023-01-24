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
    public class HouseBillGenerateDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int InsertBillGenerate(HouseBillGenerateVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_HouseCustomerBillGenerate", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@renterHouseId", _obj.renterHouseId);
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

        public List<HouseBillGenerateVM> GetBillList(int month, int year)
        {
            DataTable dt = new DataTable();
            List<HouseBillGenerateVM> billList = new List<HouseBillGenerateVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getHouseBills", con);
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
                        select new HouseBillGenerateVM()
                        {
                            companyAddress = rdr["companyAddress"].ToString(),
                            companyName = rdr["companyName"].ToString(),
                            billDetailId = Convert.ToInt32(rdr["billDetailId"]),
                            monthName = rdr["monthName"].ToString(),
                            yearName = rdr["yearName"].ToString(),
                            isClosedString = rdr["isClosedString"].ToString(),
                            renterName = rdr["renterName"].ToString(),
                            renterPhone = rdr["renterPhone"].ToString(),
                            renterHouseId = Convert.ToInt32(rdr["renterHouseId"]),
                            houseName = rdr["houseName"].ToString(),
                            houseType = rdr["houseType"].ToString(),
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
                    SqlCommand cmd = new SqlCommand("dsp_deleteHouseBill", con);
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