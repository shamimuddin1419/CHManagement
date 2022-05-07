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
    public class ProjectDA
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public int InsertOrUpdateProject(ProjectVM _obj)
        {
            int result = 0;
            DataTable tvp = new DataTable();
            tvp.Columns.Add(new DataColumn("id", typeof(int)));
            foreach (var id in _obj.careTakerIds ?? new List<int>())
            {
                tvp.Rows.Add(id);
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("isp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@projectId", _obj.projectId);
                cmd.Parameters.AddWithValue("@projectName", _obj.projectName);
                cmd.Parameters.AddWithValue("@projectType", _obj.projectType);
                cmd.Parameters.AddWithValue("@projectAddress", _obj.projectAddress);
                cmd.Parameters.AddWithValue("@projectDescription", _obj.projectDescription);
                cmd.Parameters.AddWithValue("@createdBy", _obj.createdBy);
                cmd.Parameters.AddWithValue("@List", tvp);
                result = 1;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return result;
        }
        public List<ProjectVM> GetProjectList()
        {
            DataTable dt = new DataTable();
            List<ProjectVM> projectList = new List<ProjectVM>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getProjects", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }
            projectList = (from DataRow rdr in dt.Rows
                             select new ProjectVM()
                             {
                                 rptCompanyName = rdr["rptCompanyName"].ToString(),
                                 rptCompanyAddress = rdr["rptCompanyAddress"].ToString(),
                                 projectId = Convert.ToInt16(rdr["projectId"]),
                                 projectName = rdr["projectName"].ToString(),
                                 projectAddress = rdr["projectAddress"].ToString(),
                                 projectDescription = rdr["projectDescription"].ToString(),
                                 projectType = rdr["projectType"].ToString(),
                                 
                             }).ToList();
            return projectList;
        }

        public ProjectVM GetProjectById(int projectId)
        {
            DataTable dt = new DataTable();
            ProjectVM project = new ProjectVM();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("gsp_getProjectById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@projectId", projectId);
                con.Open();
                var da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                con.Close();
            }

            DataRow dr = dt.Rows[0];
            project.projectId = Convert.ToInt32(dr["projectId"]);
            project.projectName = dr["projectName"].ToString();
            project.projectAddress = dr["projectAddress"].ToString();
            project.projectDescription = dr["projectDescription"].ToString();
            project.projectType = dr["projectType"].ToString();
            project.careTakers = new List<CareTakerVM>();
            if (dr["careTakerId"] != DBNull.Value)
            {
                project.careTakers = (from DataRow rdr in dt.Rows
                                      select new CareTakerVM()
                                       {
                                           careTakerId = Convert.ToInt32(rdr["careTakerId"]),
                                           careTakerName = rdr["careTakerName"].ToString(),
                                           permanentAddress = rdr["careTakerAddress"].ToString(),
                                           phoneNo = rdr["careTakerMobile"].ToString(),
                                       }).ToList();
            }
            return project;
        }

    }
}