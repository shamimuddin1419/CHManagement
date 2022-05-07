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
    public class HostDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<HostVM> GetHostList()
        {
            DataTable dt = new DataTable();
            List<HostVM> hostList = new List<HostVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getHosts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            hostList = (from DataRow rdr in dt.Rows
                        select new HostVM()
                        {
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            hostId = Convert.ToInt32(rdr["hostId"]),
                            hostName = rdr["hostName"].ToString(),
                            hostAddress = rdr["hostAddress"].ToString(),
                            hostPhone = rdr["hostPhone"].ToString(),
                            isActive = Convert.ToBoolean(rdr["isActive"]),
                            isActiveString = rdr["isActiveString"].ToString()
                        }).ToList();
            return hostList;
        }
        public int InsertOrUpdateHost(HostVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_host", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hostId", _obj.hostId);
                cmd.Parameters.AddWithValue("@hostName", _obj.hostName);
                cmd.Parameters.AddWithValue("@hostAddress", _obj.hostAddress);
                cmd.Parameters.AddWithValue("@hostPhone", _obj.hostPhone);
                cmd.Parameters.AddWithValue("@isActive", _obj.isActive);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
        public HostVM GetHostById(int hostId)
        {
            HostVM _obj = new HostVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getHostById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@hostId", hostId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    _obj.hostId = Convert.ToInt32(rdr["hostId"]);
                    _obj.hostName = rdr["hostName"].ToString();
                    _obj.hostPhone = rdr["hostPhone"].ToString();
                    _obj.hostAddress = rdr["hostAddress"].ToString();
                    _obj.isActive = Convert.ToBoolean(rdr["isActive"]);

                }
                con.Close();
            }
            return _obj;
        }
    }
}