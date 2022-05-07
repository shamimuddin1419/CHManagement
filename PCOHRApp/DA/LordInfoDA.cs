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
    public class LordInfoDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<LordInfoVM> GetLordInfoList()
        {
            DataTable dt = new DataTable();
            List<LordInfoVM> lordList = new List<LordInfoVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getLordInfoes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            lordList = (from DataRow rdr in dt.Rows
                        select new LordInfoVM()
                        {
                            rptCompanyName = rdr["rptCompanyName"].ToString(),
                            rptCompanyAddress = rdr["rptCompanyAddress"].ToString(),
                            lordId = Convert.ToInt16(rdr["lordId"]),
                            ownerName = rdr["ownerName"].ToString(),
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            phoneNo = rdr["phoneNo"].ToString(),
                            email = rdr["email"].ToString(),
                            nid = rdr["nid"].ToString(),
                            createdBy = Convert.ToInt16(rdr["createdBy"]),
                            createdDate = Convert.ToDateTime(rdr["createdDate"])
                        }).ToList();
            return lordList;
        }
        public int InsertOrUpdateLordInfo(LordInfoVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_lordInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lordId", _obj.lordId);
                cmd.Parameters.AddWithValue("@ownerName", _obj.ownerName);
                cmd.Parameters.AddWithValue("@companyName", _obj.companyName);
                cmd.Parameters.AddWithValue("@companyAddress", _obj.companyAddress);
                cmd.Parameters.AddWithValue("@phoneNo", _obj.phoneNo);
                cmd.Parameters.AddWithValue("@email", _obj.email);
                cmd.Parameters.AddWithValue("@nid", _obj.nid);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
        public LordInfoVM GetLordInfoById(int lordId)
        {
            LordInfoVM _obj = new LordInfoVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getLordInfoById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lordId", lordId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _obj.rptCompanyName = rdr["rptCompanyName"].ToString();
                    _obj.rptCompanyAddress = rdr["rptCompanyAddress"].ToString();
                    _obj.lordId = Convert.ToInt16(rdr["lordId"]);
                    _obj.ownerName = rdr["ownerName"].ToString();
                    _obj.companyName = rdr["companyName"].ToString();
                    _obj.companyAddress = rdr["companyAddress"].ToString();
                    _obj.phoneNo = rdr["phoneNo"].ToString();
                    _obj.email = rdr["email"].ToString();
                    _obj.nid = rdr["nid"].ToString();
                    _obj.createdBy = Convert.ToInt16(rdr["createdBy"]);
                    _obj.createdDate = Convert.ToDateTime(rdr["createdDate"]);
                }
                con.Close();
            }
            return _obj;
        }
    }
}