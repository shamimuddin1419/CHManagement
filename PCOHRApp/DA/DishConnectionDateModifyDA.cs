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
    public class DishConnectionDateModifyDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        internal int CheckPassword(CustomerVM _obj, string PageName)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_GetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PageName", PageName);
                cmd.Parameters.AddWithValue("@Password", _obj.Password);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = int.Parse(rdr["Status"].ToString());
                }

                con.Close();
            }
            return result;
        }

        internal int ConnectionDateUpdate(CustomerVM _obj)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteDishPrimaryBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cid", _obj.customerId);
                    cmd.Parameters.AddWithValue("@connFee", _obj.connFee);
                    cmd.Parameters.AddWithValue("@monthlyBill", _obj.monthBill);
                    cmd.Parameters.AddWithValue("@connMonth", _obj.connMonth);
                    cmd.Parameters.AddWithValue("@connYear", _obj.connYear);
                    cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
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