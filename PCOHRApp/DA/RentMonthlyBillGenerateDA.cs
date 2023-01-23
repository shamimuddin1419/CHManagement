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
    public class RentMonthlyBillGenerateDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int InsertBillGenerate(RentMonthlyBillGenerateReqVM _obj)
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


    }
}