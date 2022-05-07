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
    public class ZoneDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<ZoneVM> GetZoneList()
        {
            DataTable dt = new DataTable();
            List<ZoneVM> zoneList = new List<ZoneVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getZones", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            zoneList = (from DataRow rdr in dt.Rows
                        select new ZoneVM()
                        {
                            zoneId = Convert.ToInt32(rdr["zoneId"]),
                            zoneName = rdr["zoneName"].ToString(),
                            isActive = Convert.ToBoolean(rdr["isActive"]),
                        }).ToList();
            return zoneList;
        }
        public int InsertOrUpdateZone(ZoneVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_zone", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@zoneId", _obj.zoneId);
                cmd.Parameters.AddWithValue("@zoneName", _obj.zoneName);
                cmd.Parameters.AddWithValue("@isActive", _obj.isActive);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
        public ZoneVM GetZoneById(int zoneId)
        {
            ZoneVM _obj = new ZoneVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getZoneById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@zoneId", zoneId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    _obj.zoneId = Convert.ToInt32(rdr["zoneId"]);
                    _obj.zoneName = rdr["zoneName"].ToString();
                    _obj.isActive = Convert.ToBoolean(rdr["isActive"]);

                }
                con.Close();
            }
            return _obj;
        }
    }
}