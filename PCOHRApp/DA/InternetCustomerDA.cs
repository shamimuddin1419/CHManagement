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
    public class InternetCustomerDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public string InsertOrUpdateCustomer(CustomerVM _obj)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_InternetCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@insertFlag", _obj.insertFlag);
                cmd.Parameters.AddWithValue("@customerId", _obj.customerId);
                cmd.Parameters.AddWithValue("@customerSerialId", _obj.customerSerialId);
                cmd.Parameters.AddWithValue("@customerName", _obj.customerName);
                cmd.Parameters.AddWithValue("@customerPhone", _obj.customerPhone);
                cmd.Parameters.AddWithValue("@customerAddress", _obj.customerAddress);
                cmd.Parameters.AddWithValue("@requiredNet", _obj.requiredNet);
                cmd.Parameters.AddWithValue("@ipAddress", _obj.ipAddress);
                cmd.Parameters.AddWithValue("@hostId", _obj.hostId);
                cmd.Parameters.AddWithValue("@zoneId", _obj.zoneId);
                cmd.Parameters.AddWithValue("@assignedUserId", _obj.assignedUserId);
                cmd.Parameters.AddWithValue("@connFee", _obj.connFee);
                cmd.Parameters.AddWithValue("@monthBill", _obj.monthBill);
                cmd.Parameters.AddWithValue("@othersAmount", _obj.othersAmount);
                cmd.Parameters.AddWithValue("@description", _obj.description);
                cmd.Parameters.AddWithValue("@connMonth", _obj.connMonth);
                cmd.Parameters.AddWithValue("@connYear", _obj.connYear);
                cmd.Parameters.AddWithValue("@isActive", _obj.isActive);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                cmd.Parameters.AddWithValue("@EntryDate", _obj.EntryDate);
                cmd.Parameters.AddWithValue("@OnuMC", _obj.onuMCId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = rdr["customerSerial"].ToString();
                }
                
                con.Close();
            }
            return result;
        }



        public List<CustomerVM> GetCustomerList()
        {
            DataTable dt = new DataTable();
            List<CustomerVM> userList = new List<CustomerVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            userList = (from DataRow rdr in dt.Rows
                        select new CustomerVM()
                        {
                            id = Convert.ToInt32(rdr["id"]),
                            customerId = rdr["customerId"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            customerSerialId = Convert.ToInt32(rdr["customerSerialId"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerAddress = rdr["customerAddress"].ToString(),
                            requiredNet = rdr["requiredNet"].ToString(),
                            ipAddress = rdr["ipAddress"].ToString(),
                            hostId = Convert.ToInt32(rdr["hostId"]),
                            hostName = rdr["hostName"].ToString(),
                            zoneId = Convert.ToInt32(rdr["zoneId"]),
                            zoneName = rdr["zoneName"].ToString(),
                            assignedUserId = Convert.ToInt32(rdr["assignedUserId"]),
                            assignedUserName = rdr["assignedUserName"].ToString(),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            monthBill = Convert.ToDecimal(rdr["monthBill"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"].ToString()),
                            description = rdr["description"].ToString(),
                            connMonth = rdr["connMonth"].ToString(),
                            connYear = Convert.ToInt32(rdr["connYear"]),
                            connYearName = rdr["connYearName"].ToString(),
                            isActive = Convert.ToBoolean(rdr["isActive"]),
                            isActiveString = rdr["isActiveString"].ToString(),
                        }).ToList();
            return userList;
        }

        public CustomerVM GetCustomerById(int id)
        {
            CustomerVM _obj = new CustomerVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetCustomerById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _obj.id = Convert.ToInt32(rdr["id"]);
                    _obj.customerId = rdr["customerId"].ToString();
                   _obj.customerName = rdr["customerName"].ToString();
                   _obj.customerSerialId = Convert.ToInt16(rdr["customerSerialId"]);
                   _obj.customerSerial = rdr["customerSerial"].ToString();
                   _obj.customerPhone = rdr["customerPhone"].ToString();
                   _obj.customerAddress = rdr["customerAddress"].ToString();
                   _obj.requiredNet = rdr["requiredNet"].ToString();
                   _obj.ipAddress = rdr["ipAddress"].ToString();
                   _obj.hostId = Convert.ToInt32(rdr["hostId"]);
                   _obj.hostName = rdr["hostName"].ToString();
                   _obj.hostPhone = rdr["hostPhone"].ToString();
                   _obj.zoneId = Convert.ToInt32(rdr["zoneId"]);
                   _obj.zoneName = rdr["zoneName"].ToString();
                   _obj.assignedUserName = rdr["assignedUserName"].ToString();
                   _obj.assignedUserId = Convert.ToInt32(rdr["assignedUserId"]);
                   _obj.connFee = Convert.ToDecimal(rdr["connFee"]);
                   _obj.monthBill = Convert.ToDecimal(rdr["monthBill"]);
                   _obj.othersAmount = Convert.ToDecimal(rdr["othersAmount"].ToString());
                   _obj.description = rdr["description"].ToString();
                   _obj.connMonth = rdr["connMonth"].ToString();
                   _obj.connYear = Convert.ToInt32(rdr["connYear"]);
                   _obj.connYearName = rdr["connYearName"].ToString();
                   _obj.isActive = Convert.ToBoolean(rdr["isActive"]);
                   _obj.EntryDateString = rdr["EntryDateString"].ToString();
                   _obj.onuMCId = rdr["OnuMC"].ToString();
                }
                con.Close();
            }
            return _obj;
        }
        public CustomerVM GetCustomerByCustomeridAndSerialprefix(string customerId, int serialPrefixId)
        {
            CustomerVM _obj = new CustomerVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetCustomerByCustomeridAndSerialprefix", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@serialPrefixId", serialPrefixId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _obj.id = Convert.ToInt32(rdr["id"]);
                    _obj.customerId = rdr["customerId"].ToString();
                    _obj.customerName = rdr["customerName"].ToString();
                    _obj.customerSerialId = Convert.ToInt16(rdr["customerSerialId"]);
                    _obj.customerSerial = rdr["customerSerial"].ToString();
                    _obj.customerPhone = rdr["customerPhone"].ToString();
                    _obj.customerAddress = rdr["customerAddress"].ToString();
                    _obj.requiredNet = rdr["requiredNet"].ToString();
                    _obj.ipAddress = rdr["ipAddress"].ToString();
                    _obj.hostId = Convert.ToInt32(rdr["hostId"]);
                    _obj.zoneId = Convert.ToInt32(rdr["zoneId"]);
                    _obj.assignedUserId = Convert.ToInt32(rdr["assignedUserId"]);
                    _obj.connFee = Convert.ToDecimal(rdr["connFee"]);
                    _obj.monthBill = Convert.ToDecimal(rdr["monthBill"]);
                    _obj.othersAmount = Convert.ToDecimal(rdr["othersAmount"].ToString());
                    _obj.description = rdr["description"].ToString();
                    _obj.connMonth = rdr["connMonth"].ToString();
                    _obj.connYear = Convert.ToInt32(rdr["connYear"]);
                    _obj.isActive = Convert.ToBoolean(rdr["isActive"]);

                }
                con.Close();
            }
            return _obj;
        }
    }
}