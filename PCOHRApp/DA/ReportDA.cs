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
    public class ReportDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<CustomerVM> GetDishCustomerList(bool? isActive, int zoneId, int cid,int custSerialPrefixId, int assignedUserId, int hostId)
        {
            DataTable dt = new DataTable();
            List<CustomerVM> userList = new List<CustomerVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@isActive", isActive);
                cmd.Parameters.AddWithValue("@zoneId", zoneId);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@custSerialPrefixId", custSerialPrefixId);
                cmd.Parameters.AddWithValue("@assignedUserId", assignedUserId);
                cmd.Parameters.AddWithValue("@hostId", hostId);
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
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            customerId = rdr["customerId"].ToString(),
                            customerSerialId = Convert.ToInt32(rdr["customerSerialId"]),
                            customerName = rdr["customerName"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerAddress = rdr["customerAddress"].ToString(),
                            hostId = Convert.ToInt32(rdr["hostId"]),
                            hostName = rdr["hostName"].ToString(),
                            hostPhone = rdr["hostPhone"].ToString(),
                            hostAddress = rdr["hostAddress"].ToString(),
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
                            isDisconnectedString = rdr["isDisconnectedString"].ToString()
                        }).ToList();
            return userList;
        }
        public List<CustomerVM> GetInternetCustomerList(bool? isActive, int zoneId, int cid,int custSerialPrefixId, int assignedUserId, int hostId)
        {
            DataTable dt = new DataTable();
            List<CustomerVM> userList = new List<CustomerVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetCustomers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@isActive", isActive);
                cmd.Parameters.AddWithValue("@zoneId", zoneId);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@custSerialPrefixId", custSerialPrefixId);
                cmd.Parameters.AddWithValue("@assignedUserId", assignedUserId);
                cmd.Parameters.AddWithValue("@hostId", hostId);
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
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            customerId = rdr["customerId"].ToString(),
                            customerSerialId = Convert.ToInt32(rdr["customerSerialId"]),
                            customerName = rdr["customerName"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerAddress = rdr["customerAddress"].ToString(),
                            hostId = Convert.ToInt32(rdr["hostId"]),
                            hostName = rdr["hostName"].ToString(),
                            hostAddress = rdr["hostAddress"].ToString(),
                            hostPhone = rdr["hostPhone"].ToString(),
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
                            isDisconnectedString = rdr["isDisconnectedString"].ToString()
                        }).ToList();
            return userList;
        }
        public List<BillCollectionVM> GetDishBillCollections(DateTime fromDate, DateTime toDate,int cid, int receivedBy, int collectedBy)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishBillCollections", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                cmd.Parameters.AddWithValue("@collectedBy", collectedBy);

                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            netAmount = Convert.ToDecimal(rdr["netAmount"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            discount = Convert.ToDecimal(rdr["discount"]),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDate = Convert.ToDateTime(rdr["createdDate"])
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetInternetBillCollections(DateTime fromDate, DateTime toDate, int cid, int receivedBy, int collectedBy)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetBillCollections", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                cmd.Parameters.AddWithValue("@collectedBy", collectedBy);

                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            netAmount = Convert.ToDecimal(rdr["netAmount"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDate = Convert.ToDateTime(rdr["createdDate"])
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetDishBillCollectionByType(DateTime fromDate, DateTime toDate, int cid, int receivedBy, int collectedBy, string ctype)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishBillCollectionByType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                cmd.Parameters.AddWithValue("@collectedBy", collectedBy);
                cmd.Parameters.AddWithValue("@ctype", ctype);

                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            collectionAmount = Convert.ToDecimal(rdr["collectionAmount"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            pageNo = rdr["pageNo"].ToString(),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDate = Convert.ToDateTime(rdr["createdDate"])
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetInternetBillCollectionByType(DateTime fromDate, DateTime toDate, int cid, int receivedBy, int collectedBy, string ctype)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetBillCollectionByType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@cid", cid);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                cmd.Parameters.AddWithValue("@collectedBy", collectedBy);
                cmd.Parameters.AddWithValue("@ctype", ctype);

                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            collectionAmount = Convert.ToDecimal(rdr["collectionAmount"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            pageNo = rdr["pageNo"].ToString(),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDate = Convert.ToDateTime(rdr["createdDate"])
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetInternetCustomerDue(int zoneId, int custSerialPrefixId, int assignedUserId, int hostId, int activeStatus, int dueReportStatus)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> dueList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetCustomerDue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@zoneId", zoneId);
                cmd.Parameters.AddWithValue("@custSerialPrefixId", custSerialPrefixId);
                cmd.Parameters.AddWithValue("@assignedUserId", assignedUserId);
                cmd.Parameters.AddWithValue("@hostId", hostId);
                cmd.Parameters.AddWithValue("@ActiveStatus", activeStatus);
                cmd.Parameters.AddWithValue("@DiscoutinueStatus", dueReportStatus);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            dueList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            cid = Convert.ToInt32(rdr["cid"]),
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            zoneName = rdr["zoneName"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            totalDue = Convert.ToDecimal(rdr["totalDue"]),
                            customerAddress = rdr["customerAddress"].ToString(),
                        }).ToList();
            return dueList;
        }
        public List<BillCollectionVM> GetDishCustomerDue(int zoneId, int custSerialPrefixId, int assignedUserId, int hostId, int activeStatus,int dueReportStatus)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> dueList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishCustomerDue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@zoneId", zoneId);
                cmd.Parameters.AddWithValue("@custSerialPrefixId", custSerialPrefixId);
                cmd.Parameters.AddWithValue("@assignedUserId", assignedUserId);
                cmd.Parameters.AddWithValue("@hostId", hostId);
                cmd.Parameters.AddWithValue("@ActiveStatus", activeStatus);
                cmd.Parameters.AddWithValue("@DiscoutinueStatus", dueReportStatus);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            dueList = (from DataRow rdr in dt.Rows
                       select new BillCollectionVM()
                       {
                           cid = Convert.ToInt32(rdr["cid"]),
                           companyName = rdr["companyName"].ToString(),
                           companyAddress = rdr["companyAddress"].ToString(),
                           zoneName = rdr["zoneName"].ToString(),
                           customerName = rdr["customerName"].ToString(),
                           customerSerial = rdr["customerSerial"].ToString(),
                           customerPhone = rdr["customerPhone"].ToString(),
                           monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                           reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                           shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                           othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                           connFee = Convert.ToDecimal(rdr["connFee"]),
                           totalDue = Convert.ToDecimal(rdr["totalDue"]),
                           customerAddress = rdr["customerAddress"].ToString(),
                       }).ToList();
            return dueList;
        }
        public List<BillCollectionVM> GetDishPaymentHistory(int cid)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishBillPaymentHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", cid);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            netAmount = Convert.ToDecimal(rdr["netAmount"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDate = Convert.ToDateTime(rdr["createdDate"]),
                            discount = Convert.ToDecimal(rdr["discount"]),

                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetInternetPaymentHistory(int cid)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetBillPaymentHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cid", cid);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            companyName = rdr["companyName"].ToString(),
                            companyAddress = rdr["companyAddress"].ToString(),
                            adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            netAmount = Convert.ToDecimal(rdr["netAmount"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDate = Convert.ToDateTime(rdr["createdDate"]),
                            discount = Convert.ToDecimal(rdr["discount"])
                        }).ToList();
            return billList;
        }

        public List<BillCollectionVM> GetDishCollectionPageWise(int yearId, int pageNo, int receivedBy)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishCollectionPageWise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.Parameters.AddWithValue("@pageNo", pageNo);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDateString = rdr["createdDateString"].ToString(),
                            receivedDateString = rdr["createdDateString"].ToString()
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetDishCollectionPageBetween(int yearId, int pageNo, int receivedBy, int topageNo)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishCollectionPageBetween", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.Parameters.AddWithValue("@frompageNo", pageNo);
                cmd.Parameters.AddWithValue("@topageNo", topageNo);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDateString = rdr["createdDateString"].ToString(),
                            receivedDateString = rdr["createdDateString"].ToString()
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetDishCollectionPageGroup(int yearId, int pageNo, int receivedBy, int topageNo)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getDishCollectionPageGroup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.Parameters.AddWithValue("@frompageNo", pageNo);
                cmd.Parameters.AddWithValue("@topageNo", topageNo);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {                          
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            yearName = rdr["yearName"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetInternetCollectionPageWise(int yearId, int pageNo, int receivedBy)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetCollectionPageWise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.Parameters.AddWithValue("@pageNo", pageNo);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDateString = rdr["createdDateString"].ToString(),
                            receivedDateString = rdr["createdDateString"].ToString()
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetInternetCollectionPageBetween(int yearId, int pageNo, int receivedBy, int topageNo)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetCollectionPageBetween", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.Parameters.AddWithValue("@frompageNo", pageNo);
                cmd.Parameters.AddWithValue("@topageNo", topageNo);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            cid = Convert.ToInt32(rdr["cid"]),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            customerName = rdr["customerName"].ToString(),
                            collectedByString = rdr["collectedByString"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                            createdDateString = rdr["createdDateString"].ToString(),
                            receivedDateString = rdr["createdDateString"].ToString()
                        }).ToList();
            return billList;
        }
        public List<BillCollectionVM> GetInternetCollectionPageGroup(int yearId, int pageNo, int receivedBy, int topageNo)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("rsp_getInternetCollectionPageGroup", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.Parameters.AddWithValue("@frompageNo", pageNo);
                cmd.Parameters.AddWithValue("@topageNo", topageNo);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            yearName = rdr["yearName"].ToString(),
                            receivedByString = rdr["receivedByString"].ToString(),
                        }).ToList();
            return billList;
        }

        public List<CardBillPrintVM> GetDishCustomerCardPrintInfo(int cid)
        {
            DataTable dt = new DataTable();
            List<CardBillPrintVM> billList = new List<CardBillPrintVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_DishCustomerCardPrint", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", cid);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new CardBillPrintVM()
                        {                            
                            customerLocality = rdr["customerLocality"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerAddress = rdr["customerAddress"].ToString(),
                            ownerName = rdr["ownerName"].ToString(),
                            ownerPhone = rdr["ownerPhone"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString()                          

                        }).ToList();
            return billList;
        }
        public List<CardBillPrintVM> GetInternetCustomerCardPrintInfo(int cid)
        {
            DataTable dt = new DataTable();
            List<CardBillPrintVM> billList = new List<CardBillPrintVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_InternetCustomerCardPrint", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerId", cid);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new CardBillPrintVM()
                        {
                            customerLocality = rdr["customerLocality"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString(),
                            customerAddress = rdr["customerAddress"].ToString(),
                            ownerName = rdr["ownerName"].ToString(),
                            ownerPhone = rdr["ownerPhone"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString()

                        }).ToList();
            return billList;
        }
    }
}