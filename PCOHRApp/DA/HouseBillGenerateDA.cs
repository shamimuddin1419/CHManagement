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
    public class HouseBillGenerateDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int InsertBillGenerate(HouseBillGenerateVM _obj)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("isp_HouseCustomerBillGenerate", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@renterHouseId", _obj.renterHouseId);
                    cmd.Parameters.AddWithValue("@isBatch", _obj.isBatch);
                    cmd.Parameters.AddWithValue("@month", _obj.month);
                    cmd.Parameters.AddWithValue("@year", _obj.year);
                    cmd.Parameters.AddWithValue("@remarks", _obj.remarks);
                    cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                    cmd.CommandTimeout = 0;
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

        public List<HouseBillGenerateVM> GetBillList(int month, int year)
        {
            List<HouseBillGenerateVM> billList = new List<HouseBillGenerateVM>();

            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getHouseBills", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }
                billList = (from DataRow rdr in dt.Rows
                            select new HouseBillGenerateVM()
                            {
                                companyAddress = rdr["companyAddress"].ToString(),
                                companyName = rdr["companyName"].ToString(),
                                billDetailId = Convert.ToInt32(rdr["billDetailId"]),
                                monthName = rdr["monthName"].ToString(),
                                yearName = rdr["yearName"].ToString(),
                                isClosedString = rdr["isClosedString"].ToString(),
                                renterName = rdr["renterName"].ToString(),
                                renterPhone = rdr["renterPhone"].ToString(),
                                renterHouseId = Convert.ToInt32(rdr["renterHouseId"]),
                                houseName = rdr["houseName"].ToString(),
                                houseType = rdr["houseType"].ToString(),
                                billAmount = Convert.ToDecimal(rdr["billAmount"]),
                            }).ToList();
                return billList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteBill(int billDetailId, int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteHouseBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@billDetailId", billDetailId);
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

        public List<UnUpdatedHouseBillVM> GetUnUpdatedHouseBills(int houseId)
        {
            List<UnUpdatedHouseBillVM> result = new List<UnUpdatedHouseBillVM>();
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getUnUpdatedHouseBills", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@houseId", houseId);
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }
                result = (from DataRow rdr in dt.Rows
                          select new UnUpdatedHouseBillVM()
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

        public HouseBillInformationVM GetHouseBillInformation(int billDetailId,bool isUtilityUpdated = false)
        {
            HouseBillInformationVM result = new HouseBillInformationVM();

            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getHouseBillInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@billDetailId", billDetailId);
                    cmd.Parameters.AddWithValue("@isUtilityUpdated", isUtilityUpdated);
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }

                result = new HouseBillInformationVM
                {
                    billAmount = Convert.ToDecimal(dt.Rows[0]["billAmount"]),
                    electricityCharge = Convert.ToDecimal(dt.Rows[0]["electricityCharge"]),
                    gasCharge = Convert.ToDecimal(dt.Rows[0]["gasCharge"]),
                    serviceCharge = Convert.ToDecimal(dt.Rows[0]["serviceCharge"]),
                    waterCharge = Convert.ToDecimal(dt.Rows[0]["waterCharge"]),
                    otherCharge = Convert.ToDecimal(dt.Rows[0]["otherCharge"]),
                    houseName = dt.Rows[0]["houseName"].ToString(),
                    billDetailId = Convert.ToInt32(dt.Rows[0]["billDetailId"]),
                    houseType = dt.Rows[0]["houseType"].ToString(),
                    meterNo = dt.Rows[0]["meterNo"].ToString(),
                    rentEndTo = dt.Rows[0]["rentEndTo"].ToString(),
                    renterEmail = dt.Rows[0]["renterEmail"].ToString(),
                    renterEmergencyContactName = dt.Rows[0]["renterEmergencyContactName"].ToString(),
                    renterEmergencyContactPhone = dt.Rows[0]["renterEmergencyContactPhone"].ToString(),
                    renterName = dt.Rows[0]["renterName"].ToString(),
                    renterNID = dt.Rows[0]["renterNID"].ToString(),
                    renterPhone = dt.Rows[0]["renterPhone"].ToString(),
                    rentStartFrom = dt.Rows[0]["rentStartFrom"].ToString(),
                    unPaidMonth = dt.Rows[0]["unPaidMonth"].ToString(),
                    companyAddress = dt.Rows[0]["companyAddress"].ToString(),
                    companyName = dt.Rows[0]["companyName"].ToString()
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public int UpdateGeneratedBill(HouseBillInformationVM input, int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("usp_UpdateHouseUtilityBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@billDetailId", input.billDetailId);
                    cmd.Parameters.AddWithValue("@gasCharge", input.gasCharge); 
                    cmd.Parameters.AddWithValue("@waterCharge", input.waterCharge);
                    cmd.Parameters.AddWithValue("@electricityCharge", input.electricityCharge);
                    cmd.Parameters.AddWithValue("@serviceCharge", input.serviceCharge);
                    cmd.Parameters.AddWithValue("@otherCharge", input.otherCharge);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    cmd.CommandTimeout = 0;
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

        public List<HouseBillInformationVM> GetUpdatedHouseBillInformation(int year, int month)
        {
            List<HouseBillInformationVM> billList = new List<HouseBillInformationVM>();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("gsp_getUpdatedHouseBillInformation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    con.Open();
                    var da = new SqlDataAdapter(cmd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    con.Close();
                }
                billList = (from DataRow rdr in dt.Rows
                            select new HouseBillInformationVM()
                            {
                                billAmount = Convert.ToDecimal(rdr["billAmount"]),
                                electricityCharge = Convert.ToDecimal(rdr["electricityCharge"]),
                                gasCharge = Convert.ToDecimal(rdr["gasCharge"]),
                                serviceCharge = Convert.ToDecimal(rdr["serviceCharge"]),
                                waterCharge = Convert.ToDecimal(rdr["waterCharge"]),
                                otherCharge = Convert.ToDecimal(rdr["otherCharge"]),
                                houseName = rdr["houseName"].ToString(),
                                billDetailId = Convert.ToInt32(rdr["billDetailId"]),
                                houseType = rdr["houseType"].ToString(),
                                meterNo = rdr["meterNo"].ToString(),
                                rentEndTo = rdr["rentEndTo"].ToString(),
                                renterEmail = rdr["renterEmail"].ToString(),
                                renterEmergencyContactName = rdr["renterEmergencyContactName"].ToString(),
                                renterEmergencyContactPhone = rdr["renterEmergencyContactPhone"].ToString(),
                                renterName = rdr["renterName"].ToString(),
                                renterNID = rdr["renterNID"].ToString(),
                                renterPhone = rdr["renterPhone"].ToString(),
                                rentStartFrom = rdr["rentStartFrom"].ToString(),
                                unPaidMonth = rdr["unPaidMonth"].ToString(),
                                companyAddress = rdr["companyAddress"].ToString(),
                                companyName = rdr["companyName"].ToString()
                            }).ToList();
                return billList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteUpdatedHouseBill(int billDetailId, int createdBy)
        {
            try
            {
                int result = 0;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dsp_deleteUpdatedHouseBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@billDetailId", billDetailId);
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
    }
}