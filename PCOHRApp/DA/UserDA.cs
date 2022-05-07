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
    public class UserDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public int InsertOrUpdateUser(UserVM _obj)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_registration", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@userId", _obj.userId);
                cmd.Parameters.AddWithValue("@password", _obj.password);
                cmd.Parameters.AddWithValue("@userName", _obj.userName);
                cmd.Parameters.AddWithValue("@email", _obj.email);
                cmd.Parameters.AddWithValue("@phoneNumber", _obj.phoneNumber);
                cmd.Parameters.AddWithValue("@address", _obj.address);
                cmd.Parameters.AddWithValue("@isAdmin", _obj.isAdmin);
                cmd.Parameters.AddWithValue("@isCableUser", _obj.isCableUser);
                cmd.Parameters.AddWithValue("@isHouseRentUser", _obj.isHouseRentUser);
                cmd.Parameters.AddWithValue("@designationId", _obj.designationId);
                cmd.Parameters.AddWithValue("@userFullName", _obj.userFullName);
                cmd.Parameters.AddWithValue("@isActive", _obj.isActive);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);

                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }



        public List<UserVM> GetUserList()
        {
            DataTable dt = new DataTable();
            List<UserVM> userList = new List<UserVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
             userList = (from DataRow rdr in dt.Rows
                   select new UserVM()
                   {
                        userId = Convert.ToInt16(rdr["userId"]),
                        userName = rdr["userName"].ToString(),
                        email = rdr["email"].ToString(),
                        phoneNumber = rdr["phoneNumber"].ToString(),
                        address = rdr["address"].ToString(),
                        designationId =Convert.ToInt16( rdr["designationId"]),
                        designationName = rdr["designationName"].ToString(),
                        isCableUser = Convert.ToBoolean(rdr["isCableUser"]),
                        isAdmin = Convert.ToBoolean(rdr["isAdmin"]),
                        isHouseRentUser = Convert.ToBoolean(rdr["isHouseRentUser"]),
                        userFullName = rdr["userFullName"].ToString(),
                        isActive = Convert.ToBoolean(rdr["isActive"]),
                        isManagement = Convert.ToBoolean(rdr["isManagement"])
                   }).ToList();
            return userList;
        }

        public List<UserPageVM> GetPageListByUserId(int userId)
        {
            DataTable dt = new DataTable();
            List<UserPageVM> userPageList = new List<UserPageVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_menusByUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            userPageList = (from DataRow rdr in dt.Rows
                            select new UserPageVM()
                        {
                            pageId = Convert.ToInt16(rdr["pageId"]),
                            pageName = rdr["pageName"].ToString(),
                            pageUrl = rdr["pageUrl"].ToString(),
                            pageType = rdr["pageType"].ToString(),
                            isPermitted = Convert.ToBoolean(rdr["isPermitted"]),
                        }).ToList();
            return userPageList;
        }

        public List<UserPageVM> GetPageListByUserAndType(int userId, string userType)
        {
            DataTable dt = new DataTable();
            List<UserPageVM> userPageList = new List<UserPageVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getLoadMenuByUserAndType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@userType", userType);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            userPageList = (from DataRow rdr in dt.Rows
                            select new UserPageVM()
                            {
                                pageId = Convert.ToInt16(rdr["pageId"]),
                                pageName = rdr["pageName"].ToString(),
                                pageUrl = rdr["pageUrl"].ToString(),
                                pageType = rdr["pageType"].ToString(),
                                pageSubType = rdr["pageSubType"].ToString(),
                            }).ToList();
            return userPageList;
        }

        public UserVM GetUserByUserNameAndPass(string userName,string password,string userType)
        {
            UserVM _obj = new UserVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getUserByUserNameAndPass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@userType", userType);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    _obj.userId = Convert.ToInt16(rdr["userId"]);
                    _obj.userName = rdr["userName"].ToString();
                    _obj.email = rdr["email"].ToString();
                    _obj.phoneNumber = rdr["phoneNumber"].ToString();
                    _obj.address = rdr["address"].ToString();
                    _obj.designationId = Convert.ToInt16(rdr["designationId"]);
                    _obj.isCableUser = Convert.ToBoolean(rdr["isCableUser"]);
                    _obj.isAdmin = Convert.ToBoolean(rdr["isAdmin"]);
                    _obj.isHouseRentUser = Convert.ToBoolean(rdr["isHouseRentUser"]);
                    _obj.userFullName = rdr["userFullName"].ToString();
                    _obj.password = rdr["password"].ToString();
                    _obj.isActive = Convert.ToBoolean(rdr["isActive"]);
                    _obj.userType = rdr["userType"].ToString();

                }
                con.Close();
            }
            return _obj;
        }
        public UserVM GetUserById(int userId)
        {
            UserVM _obj = new UserVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    _obj.userId = Convert.ToInt16(rdr["userId"]);
                    _obj.userName = rdr["userName"].ToString();
                    _obj.email = rdr["email"].ToString();
                    _obj.phoneNumber = rdr["phoneNumber"].ToString();
                    _obj.address = rdr["address"].ToString();
                    _obj.designationId = Convert.ToInt16(rdr["designationId"]);
                    _obj.isCableUser = Convert.ToBoolean(rdr["isCableUser"]);
                    _obj.isAdmin = Convert.ToBoolean(rdr["isAdmin"]);
                    _obj.isHouseRentUser = Convert.ToBoolean(rdr["isHouseRentUser"]);
                    _obj.userFullName = rdr["userFullName"].ToString();
                    _obj.password = rdr["password"].ToString();
                    _obj.isActive = Convert.ToBoolean(rdr["isActive"]);

                }
                con.Close();
            }
            return _obj;
        }

        public int UserPageMap(List<int> ids,int userId,int createdBy)
        {
            int result = 0;
            DataTable tvp = new DataTable();
            tvp.Columns.Add(new DataColumn("id", typeof(int)));
            foreach (var id in ids)
            {
                tvp.Rows.Add(id);
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_usermenumapping", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@List", tvp);
                cmd.Parameters.AddWithValue("@createdBy",createdBy);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }

    }
}