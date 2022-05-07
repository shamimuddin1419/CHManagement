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
    public class DesignationDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<DesignationVM> GetDesignationList()
        {
            List<DesignationVM> designationList = new List<DesignationVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getDesignations", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DesignationVM _obj = new DesignationVM();
                    _obj.designationId =Convert.ToInt16(rdr["designationId"]);
                    _obj.designationName = rdr["designationName"].ToString();
                    _obj.isActive = Convert.ToBoolean(rdr["isActive"]);
                    designationList.Add(_obj);
                }
                con.Close();
            }
            return designationList;
        }

    }
}