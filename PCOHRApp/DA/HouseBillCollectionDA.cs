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
    public class HouseBillCollectionDA
    {
        private string connectionString;
        public HouseBillCollectionDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<UnPaidHouseBillVM> GetUnPaidHouseBills(int houseId)
        {
            List<UnPaidHouseBillVM> result = new List<UnPaidHouseBillVM>();
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getUnPaidHouseBills", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@houseId", houseId);
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }
                result = (from DataRow rdr in dt.Rows
                          select new UnPaidHouseBillVM()
                          {
                              electricityCharge = Convert.ToDecimal(rdr["electricityCharge"]),
                              otherCharge = Convert.ToDecimal(rdr["otherCharge"]),
                              serviceCharge = Convert.ToDecimal(rdr["serviceCharge"]),
                              waterCharge = Convert.ToDecimal(rdr["waterCharge"]),
                              unPaidMonth = rdr["unPaidMonth"].ToString(),
                              gasCharge = Convert.ToDecimal(rdr["gasCharge"]),
                              billDetailId = Convert.ToInt32(rdr["billDetailId"]),
                              billAmount = Convert.ToDecimal(rdr["billAmount"]),
                          }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string InsertBillCollection(HouseRenterBillCollectionVM _obj,int createdBy)
        {
            try
            {
                string result = String.Empty;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("isp_HouseRenterBillCollection", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@billDetailId", _obj.billDetailId);
                    cmd.Parameters.AddWithValue("@rcvAmount", _obj.rcvAmount);
                    cmd.Parameters.AddWithValue("@adjustAdvance", _obj.adjustAdvance);
                    cmd.Parameters.AddWithValue("@discount", _obj.discount);
                    
                    cmd.Parameters.AddWithValue("@description", _obj.description);
                    
                    cmd.Parameters.AddWithValue("@collectionDate", _obj.collectionDate);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    cmd.CommandTimeout = 0;
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
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HouseRenterBillCollectionVM> GetCollectionList()
        {
            List<HouseRenterBillCollectionVM> billList = new List<HouseRenterBillCollectionVM>();

            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getHouseRenterBillCollections", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }
                billList = (from DataRow rdr in dt.Rows
                            select new HouseRenterBillCollectionVM()
                            {
                                companyAddress = rdr["companyAddress"].ToString(),
                                companyName = rdr["companyName"].ToString(),
                                collectionId = Convert.ToInt32(rdr["collectionId"]),
                                renterHouseId = Convert.ToInt32(rdr["renterHouseId"]),
                                month = Convert.ToInt32(rdr["month"]),
                                yearId = Convert.ToInt32(rdr["yearId"]),
                                billMonth = rdr["billMonth"].ToString(),
                                rcvAmount = Convert.ToDecimal(rdr["rcvAmount"]),
                                payableAmount = Convert.ToDecimal(rdr["payableAmount"]),
                                adjustAdvance = Convert.ToDecimal(rdr["adjustAdvance"]),
                                discount = Convert.ToDecimal(rdr["discount"]),
                                description = rdr["description"].ToString(),
                                collectionDate = Convert.ToDateTime(rdr["collectionDate"]),
                                paymentRef = rdr["paymentRef"].ToString(),
                                collectedDateString = rdr["collectedDateString"].ToString(),
                                houseName = rdr["houseName"].ToString(),
                                renterName = rdr["renterName"].ToString(),
                                billDetailId = Convert.ToInt32(rdr["billDetailId"])
                            }).ToList();
                return billList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteCollection(int collectionId, int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteHouseRenterBillCollection", con);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HouseRenterBillCollectionVM GetHouseBillByBillId(int billDetailId)
        {
            HouseRenterBillCollectionVM result = null;
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getHouseBillByBillId", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@billDetailId", billDetailId);
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    result = new HouseRenterBillCollectionVM
                    {
                        companyAddress = row["companyAddress"].ToString(),
                        companyName = row["companyName"].ToString(),
                        renterHouseId = Convert.ToInt32(row["renterHouseId"]),
                        billDetailId = Convert.ToInt32(row["billDetailId"]),
                        billMonth = row["billMonth"].ToString(),

                        renterName = row["renterName"].ToString(),
                        renterNID = row["renterNID"].ToString(),
                        renterPhone = row["renterPhone"].ToString(),
                        houseName = row["houseName"].ToString(),
                        houseType = row["houseType"].ToString(),
                        advanceAmount =Convert.ToDecimal( row["advanceAmount"]),
                        rentAmount = Convert.ToDecimal(row["rentAmount"]),
                        gasCharge = Convert.ToDecimal(row["gasCharge"]),
                        electricityCharge = Convert.ToDecimal(row["electricityCharge"]),
                        serviceCharge = Convert.ToDecimal(row["serviceCharge"]),
                        otherCharge = Convert.ToDecimal(row["otherCharge"]),
                        payableAmount = Convert.ToDecimal(row["payableAmount"])
                    };
                }
                    
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public HouseRenterBillCollectionVM GetHouseRenterBillReceipt(int billDetailId)
        {
            HouseRenterBillCollectionVM result = null;
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getHouseRenterBillReceipt", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@billDetailId", billDetailId);
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    result = new HouseRenterBillCollectionVM
                    {
                        companyAddress = row["companyAddress"].ToString(),
                        companyName = row["companyName"].ToString(),
                        billMonth = row["billMonth"].ToString(),
                        payableAmount = Convert.ToDecimal(row["payableAmount"]),
                        discount = Convert.ToDecimal(row["discount"]),
                        description = row["description"].ToString(),
                        collectionDate = Convert.ToDateTime(row["collectionDate"]),
                        paymentRef = row["paymentRef"].ToString(),
                        collectedDateString = row["collectedDateString"].ToString(),
                        renterName = row["renterName"].ToString(),
                        houseName = row["houseName"].ToString(),
                        meterNo = row["meterNo"].ToString(),
                        renterNID = row["renterNID"].ToString(),
                        renterPhone = row["renterPhone"].ToString(),
                        renterEmail = row["renterEmail"].ToString(),

                        rentAmount = Convert.ToDecimal(row["rentAmount"]),
                        gasCharge = Convert.ToDecimal(row["gasCharge"]),
                        electricityCharge = Convert.ToDecimal(row["electricityCharge"]),
                        serviceCharge = Convert.ToDecimal(row["serviceCharge"]),
                        otherCharge = Convert.ToDecimal(row["otherCharge"]),
                        rcvAmount = Convert.ToDecimal(row["rcvAmount"]),
                        adjustAdvance = Convert.ToDecimal(row["adjustAdvance"]),
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}