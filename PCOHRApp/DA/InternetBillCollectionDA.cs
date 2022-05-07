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
    public class InternetBillCollectionDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<PreviousBillInfoVM> GetPreviousInfoList(int cid)
        {
            DataTable dt = new DataTable();
            List<PreviousBillInfoVM> billList = new List<PreviousBillInfoVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetRemaingBillByCid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", cid);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new PreviousBillInfoVM()
                        {
                            billDetailId = Convert.ToInt32(rdr["billDetailId"]),
                            advanceAmount = Convert.ToDecimal(rdr["advanceAmount"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            collectionType = rdr["collectionType"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerRequestId = Convert.ToInt32(rdr["customerRequestId"]),
                            pendingAmount = Convert.ToDecimal(rdr["pendingAmount"]),
                            requestName = rdr["requestName"].ToString(),
                            transactionAmount = Convert.ToDecimal(rdr["transactionAmount"]),
                            transactionMonth = rdr["transactionMonth"].ToString(),
                            yearName = rdr["yearName"].ToString(),
                        }).ToList();
            return billList;
        }
        public PayedBillVM GetLastPaidBill(int cid)
        {
            DataTable dt = new DataTable();
            PayedBillVM bill = new PayedBillVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getLastPayedBillForInternet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", cid);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            bill = (from DataRow rdr in dt.Rows
                    select new PayedBillVM()
                    {
                        billAmount = Convert.ToDecimal(rdr["billAmount"]),
                        customerSL = rdr["customerSL"].ToString(),
                        pageNo = rdr["pageNo"].ToString(),
                        receivedByString = rdr["receivedByString"].ToString(),
                        receivedDateString = rdr["receivedDateString"].ToString(),
                        yearName = Convert.ToDecimal(rdr["yearName"]),
                        month = rdr["month"].ToString()
                    }).FirstOrDefault();
            return bill;
        }
        public string InsertBillCollection(BillCollectionVM _obj)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_InternetCustomerBillCollection", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cid", _obj.cid);
                cmd.Parameters.AddWithValue("@connFee", _obj.connFee);
                cmd.Parameters.AddWithValue("@reConnFee", _obj.reConnFee);
                cmd.Parameters.AddWithValue("@othersAmount", _obj.othersAmount);
                cmd.Parameters.AddWithValue("@monthlyFee", _obj.monthlyFee);
                cmd.Parameters.AddWithValue("@fromMonth", _obj.fromMonth);
                cmd.Parameters.AddWithValue("@toMonth", _obj.toMonth);
                cmd.Parameters.AddWithValue("@shiftingCharge", _obj.shiftingCharge);
                cmd.Parameters.AddWithValue("@description", _obj.description);
                cmd.Parameters.AddWithValue("@netAmount", _obj.netAmount);
                cmd.Parameters.AddWithValue("@rcvAmount", _obj.rcvAmount);
                cmd.Parameters.AddWithValue("@discount", _obj.discount);
                cmd.Parameters.AddWithValue("@adjustAdvance", _obj.adjustAdvance);
                cmd.Parameters.AddWithValue("@customerSL", _obj.customerSL);
                cmd.Parameters.AddWithValue("@pageNo", _obj.pageNo);
                cmd.Parameters.AddWithValue("@collectionDate", _obj.collectionDate);
                cmd.Parameters.AddWithValue("@collectedBy", _obj.collectedBy);
                cmd.Parameters.AddWithValue("@receivedBy", _obj.receivedBy);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = rdr["collectionId"].ToString();
                }

                con.Close();
            }
            return result;
        }
        public List<BillCollectionVM> GetBillCollectionList()
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetBillCollections", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            voucherNo = rdr["voucherNo"].ToString(),
                            adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            collectionId = Convert.ToInt32(rdr["collectionId"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            description = rdr["description"].ToString(),
                            fromMonth = rdr["fromMonth"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            netAmount = Convert.ToDecimal(rdr["netAmount"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            toMonth = rdr["toMonth"].ToString(),
                            fromMonthYear = rdr["fromMonthYear"].ToString(),
                            toMonthYear = rdr["toMonthYear"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            collectedDateString = rdr["collectedDateString"].ToString(),
                        }).ToList();
            return billList;
        }

        public List<BillCollectionVM> GetBillUnCollectionList(DateTime fromDate,DateTime toDate,int receivedBy)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetUnCollections", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);
                cmd.Parameters.AddWithValue("@receivedBy", @receivedBy);

                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            voucherNo = rdr["voucherNo"].ToString(),
                            adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            collectedBy = Convert.ToInt32(rdr["collectedBy"]),
                            collectionId = Convert.ToInt32(rdr["collectionId"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            description = rdr["description"].ToString(),
                            fromMonth = rdr["fromMonth"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            netAmount = Convert.ToDecimal(rdr["netAmount"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            receivedBy = Convert.ToInt32(rdr["receivedBy"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            toMonth = rdr["toMonth"].ToString(),
                            fromMonthYear = rdr["fromMonthYear"].ToString(),
                            toMonthYear = rdr["toMonthYear"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            createdDate = Convert.ToDateTime(rdr["createdDate"]),
                            stringCreatedDate = rdr["createdDate"].ToString(),
                            customerPhone = rdr["customerPhone"].ToString()
                        }).ToList();
            return billList;
        }

        public int UpdateCollectionStatus(List<BillCollectionVM> _objs,int createdBy)
        {
            int result = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("collectionId", typeof(int)));
            dt.Columns.Add(new DataColumn("customerSL", typeof(string)));
            dt.Columns.Add(new DataColumn("pageNo", typeof(string)));
            
            
            foreach (var o in _objs)
            {
                DataRow row = dt.NewRow();
                row["collectionId"] = o.collectionId;
                row["customerSL"] = o.customerSL;
                row["pageNo"] = o.pageNo;
                dt.Rows.Add(row);
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_updateInternetCollectionStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@List", dt);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }

        public int DeleteCollection(int collectionId, int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteInternetBillCollection", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@collectionId", collectionId);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    result = 1;

                    con.Close();

                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal GetMonthlyBill(int cid, DateTime? fromMonthDate, DateTime? toMonthDate)
        {
            try
            {
                decimal result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getInternetMonthlyBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@fromMonthDate", fromMonthDate);
                    cmd.Parameters.AddWithValue("@toMonthDate", toMonthDate);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        result = Convert.ToDecimal(rdr["totalMonthlyBill"]);
                    }

                    con.Close();

                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal GetMonthlyBill(BillCollectionVM _obj)
        {
            try
            {
                DataTable tvp = new DataTable();
                tvp.Columns.Add(new DataColumn("id", typeof(int)));
                foreach (var id in _obj.billDetailsIds)
                {
                    tvp.Rows.Add(id);
                }
                decimal result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getInternetMonthlyBillByCollectionId", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@billCollectionDetailIds", tvp);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        result = Convert.ToDecimal(rdr["totalMonthlyBill"]);
                    }

                    con.Close();

                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal List<BillCollectionVM> GetPagesByYear(int yearId, int receivedBy)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> pageList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetPagesByYear", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.Parameters.AddWithValue("@receivedBy", receivedBy);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            pageList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            pageNo = rdr["pageNo"].ToString(),
                        }).ToList();
            return pageList;
        }
        internal List<BillCollectionVM> GetPagesByYear(int yearId)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> pageList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetPages", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);               
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            pageList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            pageNo = rdr["pageNo"].ToString(),
                        }).ToList();
            return pageList;
        }

        public List<BillDelete> GetMonthlyBillDeleteList()
        {
            DataTable dt = new DataTable();
            List<BillDelete> billList = new List<BillDelete>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetInternetMonthlyDeleteHis", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillDelete()
                        {
                            Month = rdr["Month"].ToString(),
                            YearName = rdr["yearName"].ToString(),
                            CustomerSerial = rdr["customerSerial"].ToString(),
                            CustomerName = rdr["customerName"].ToString(),
                            CustomerPhone = rdr["customerPhone"].ToString(),
                            CustomerAddress = rdr["customerAddress"].ToString(),
                            DeletedBy = rdr["DeletedBy"].ToString(),
                            DeletedDate = rdr["DeletedDate"].ToString(),
                            BillAmount = Convert.ToDecimal(rdr["BillAMount"]),
                            
                        }).ToList();
            return billList;
        }

        public List<BillCollectionVM> GetBillCollectionListBySerial(string pageNo, string serialNo, int yearID)
        {
            DataTable dt = new DataTable();
            List<BillCollectionVM> billList = new List<BillCollectionVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getInternetBillCollectionsBySeriaNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@YearId", yearID);
                cmd.Parameters.AddWithValue("@PageNo", pageNo);
                cmd.Parameters.AddWithValue("@SerialNo", serialNo);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            billList = (from DataRow rdr in dt.Rows
                        select new BillCollectionVM()
                        {
                            voucherNo = rdr["voucherNo"].ToString(),
                            adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                            cid = Convert.ToInt32(rdr["cid"]),
                            collectionId = Convert.ToInt32(rdr["collectionId"]),
                            connFee = Convert.ToDecimal(rdr["connFee"]),
                            customerSerial = rdr["customerSerial"].ToString(),
                            customerSL = rdr["customerSL"].ToString(),
                            description = rdr["description"].ToString(),
                            fromMonth = rdr["fromMonth"].ToString(),
                            monthlyFee = Convert.ToDecimal(rdr["monthlyFee"]),
                            netAmount = Convert.ToDecimal(rdr["netAmount"]),
                            othersAmount = Convert.ToDecimal(rdr["othersAmount"]),
                            pageNo = rdr["pageNo"].ToString(),
                            rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                            reConnFee = Convert.ToDecimal(rdr["reConnFee"]),
                            shiftingCharge = Convert.ToDecimal(rdr["shiftingCharge"]),
                            toMonth = rdr["toMonth"].ToString(),
                            fromMonthYear = rdr["fromMonthYear"].ToString(),
                            toMonthYear = rdr["toMonthYear"].ToString(),
                            customerName = rdr["customerName"].ToString(),
                            collectedDateString = rdr["collectedDateString"].ToString(),
                            collectedByString = rdr["collectedBy"].ToString(),
                            receivedByString = rdr["receivedBy"].ToString(),
                            createdByString = rdr["createdBy"].ToString(),
                        }).ToList();
            return billList;
        }
    }
}