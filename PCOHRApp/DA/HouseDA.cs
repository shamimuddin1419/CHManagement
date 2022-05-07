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
    public class HouseDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<HouseVM> GetHouseListByProjectIdAndId(int projectId, int houseId)
        {
            DataTable dt = new DataTable();
            List<HouseVM> careTakerList = new List<HouseVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getHouseByProjectIdAndId", con);
                cmd.Parameters.AddWithValue("@houseId", houseId);
                cmd.Parameters.AddWithValue("@projectId", projectId);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            careTakerList = (from DataRow rdr in dt.Rows
                             select new HouseVM()
                             {
                                 rptCompanyName = rdr["rptCompanyName"].ToString(),
                                 rptCompanyAddress = rdr["rptCompanyAddress"].ToString(),
                                 houseId = Convert.ToInt16(rdr["houseId"]),
                                 houseName = rdr["houseName"].ToString(),
                                 projectId = Convert.ToInt32(rdr["projectId"]),
                                 houseType = rdr["houseType"].ToString(),
                                 monthlyRent = Convert.ToDecimal(rdr["monthlyRent"]),
                                 description = rdr["description"].ToString(),
                                 projectName = rdr["projectName"].ToString(),
                                 createdBy = Convert.ToInt16(rdr["createdBy"]),
                             }).ToList();
            return careTakerList;
        }
        public int InsertOrUpdateHouse(HouseVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_house", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@houseId", _obj.houseId);
                cmd.Parameters.AddWithValue("@projectId", _obj.projectId);
                cmd.Parameters.AddWithValue("@houseName", _obj.houseName);
                cmd.Parameters.AddWithValue("@houseType", _obj.houseType);
                cmd.Parameters.AddWithValue("@description", _obj.description);
                cmd.Parameters.AddWithValue("@monthlyRent", _obj.monthlyRent);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
    
    }
}