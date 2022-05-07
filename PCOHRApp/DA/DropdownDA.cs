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
    public class DropdownDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<DropdownVM> GetYearList()
        {
            DataTable dt = new DataTable();
            List<DropdownVM> yearList = new List<DropdownVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getYears", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            yearList = (from DataRow rdr in dt.Rows
                        select new DropdownVM()
                        {
                            id = Convert.ToInt16(rdr["id"]),
                            text = rdr["text"].ToString(),
                        }).ToList();
            return yearList;
        }

        public List<DropdownVM> GetCustomerSerialList()
        {
            DataTable dt = new DataTable();
            List<DropdownVM> yearList = new List<DropdownVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getCustomerSerials", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            yearList = (from DataRow rdr in dt.Rows
                        select new DropdownVM()
                        {
                            id = Convert.ToInt16(rdr["id"]),
                            text = rdr["text"].ToString(),
                        }).ToList();
            return yearList;
        }
        public List<DropdownVM> GetCustomerRequestTypeList(string requestTypeGroup)
        {
            DataTable dt = new DataTable();
            List<DropdownVM> requestList = new List<DropdownVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getCustomerRequestTypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@requestTypeGroup", requestTypeGroup);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            requestList = (from DataRow rdr in dt.Rows
                           select new DropdownVM()
                           {
                               id = Convert.ToInt16(rdr["requestTypeId"]),
                               text = rdr["requestName"].ToString(),
                           }).ToList();
            return requestList;
        }
    }
}