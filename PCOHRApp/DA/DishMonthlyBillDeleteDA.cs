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
    public class DishMonthlyBillDeleteDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        internal string DishMonthlyBillDelete(BillCollectionVM _obj)
        {
            string result = "";
            DataTable tvp = new DataTable();
            tvp.Columns.Add(new DataColumn("id", typeof(int)));
            foreach (var id in _obj.billDetailsIds)
            {
                tvp.Rows.Add(id);
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("dsp_DishMonthlyBillDelete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", _obj.cid); 
                cmd.Parameters.AddWithValue("@billCollectionDetailIds", tvp);                             
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

        internal int CheckPassword(BillCollectionVM _obj, string PageName)
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
                    result =int.Parse(rdr["Status"].ToString());
                }

                con.Close();
            }
            return result;
        }
    }
}