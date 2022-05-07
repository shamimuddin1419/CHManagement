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
    public class InternetCustomerDeleteDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        internal int CheckPassword(Models.BillCollectionVM _obj, string PageName)
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

        internal string InternetCustomerDelete(BillCollectionVM _obj)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_InternetCustomerDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", _obj.cid);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = rdr["Result"].ToString();
                }

                con.Close();
            }
            return result;
        }

    }
}