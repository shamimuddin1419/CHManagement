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
    public class CareTakerDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<CareTakerVM> GetCareTakerList()
        {
            DataTable dt = new DataTable();
            List<CareTakerVM> careTakerList = new List<CareTakerVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getCareTakers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            careTakerList = (from DataRow rdr in dt.Rows
                        select new CareTakerVM()
                        {
                            rptCompanyName = rdr["rptCompanyName"].ToString(),
                            rptCompanyAddress = rdr["rptCompanyAddress"].ToString(),
                            careTakerId = Convert.ToInt16(rdr["careTakerId"]),
                            careTakerName = rdr["careTakerName"].ToString(),
                            presentAddress = rdr["presentAddress"].ToString(),
                            permanentAddress = rdr["permanentAddress"].ToString(),
                            phoneNo = rdr["phoneNo"].ToString(),
                            email = rdr["email"].ToString(),
                            nid = rdr["nid"].ToString(),
                            joiningDate = rdr["joiningDate"] == System.DBNull.Value ? (DateTime?) null: Convert.ToDateTime(rdr["joiningDate"]),
                            joiningDateString = rdr["joiningDateString"].ToString(),
                            salary = rdr["salary"] == System.DBNull.Value ? (decimal?) null : Convert.ToDecimal(rdr["salary"]) ,
                            isActive = Convert.ToBoolean(rdr["isActive"]),
                            isActiveString = rdr["isActiveString"].ToString(),
                            createdBy = Convert.ToInt16(rdr["createdBy"]),
                            createdDate = Convert.ToDateTime(rdr["createdDate"])
                        }).ToList();
            return careTakerList;
        }
        public int InsertOrUpdateCareTaker(CareTakerVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_careTaker", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@careTakerId", _obj.careTakerId);
                cmd.Parameters.AddWithValue("@careTakerName", _obj.careTakerName);
                cmd.Parameters.AddWithValue("@presentAddress", _obj.presentAddress);
                cmd.Parameters.AddWithValue("@permanentAddress", _obj.permanentAddress);
                cmd.Parameters.AddWithValue("@phoneNo", _obj.phoneNo);
                cmd.Parameters.AddWithValue("@email", _obj.email);
                cmd.Parameters.AddWithValue("@nid", _obj.nid);
                cmd.Parameters.AddWithValue("@joiningDate", _obj.joiningDate);
                cmd.Parameters.AddWithValue("@salary", _obj.salary);
                cmd.Parameters.AddWithValue("@isActive", _obj.isActive);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
        public CareTakerVM GetCareTakerById(int careTakerId)
        {
            CareTakerVM _obj = new CareTakerVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getCareTakers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@careTakerId", careTakerId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   _obj.rptCompanyName = rdr["rptCompanyName"].ToString();
                   _obj.rptCompanyAddress = rdr["rptCompanyAddress"].ToString();
                   _obj.careTakerId = Convert.ToInt16(rdr["careTakerId"]);
                   _obj.careTakerName = rdr["careTakerName"].ToString();
                   _obj.presentAddress = rdr["presentAddress"].ToString();
                   _obj.permanentAddress = rdr["permanentAddress"].ToString();
                   _obj.phoneNo = rdr["phoneNo"].ToString();
                   _obj.email = rdr["email"].ToString();
                   _obj.nid = rdr["nid"].ToString();
                   _obj.joiningDate = rdr["joiningDate"] == System.DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["joiningDate"]);
                   _obj.joiningDateString = rdr["joiningDateString"].ToString();
                   _obj.salary = Convert.ToDecimal(rdr["salary"]);
                   _obj.isActiveString = rdr["isActiveString"].ToString();
                   _obj.isActive = Convert.ToBoolean(rdr["isActive"]);
                   _obj.createdBy = Convert.ToInt16(rdr["createdBy"]);
                   _obj.createdDate = Convert.ToDateTime(rdr["createdDate"]);
                }
                con.Close();
            }
            return _obj;
        }
    }
}