using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TaskManagementSystem.Models;
using System.Runtime.Remoting.Messaging;
using System.Web.Hosting;
using System.Configuration;

namespace TaskManagementSystem.DBHandler
{
    public class ProjectHandler
    {
        #region Globel Objects/Variable
        string _connectionString = ConfigurationManager.ConnectionStrings["dbRahulConn"].ConnectionString;
        DataTable table = new DataTable();
        Project project = new Project();
        #endregion

        #region GetProjectList
        public List<Project> GetProjectList()
        {
            List<Project> projectlist = new List<Project>();

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("RahulTMS_GetProjectList", connection);
            try
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                foreach (DataRow item in table.Rows)
                {
                    projectlist.Add(new Project()
                    {
                        Id = Convert.ToInt32(item["ProjectId"]),
                        Name = item["ProjectName"].ToString(),
                        Description = item["ProjectDescription"].ToString(),
                        StartDate = Convert.ToDateTime(item["StartDate"]),
                        EndDate = Convert.ToDateTime(item["EndDate"]),
                        IsClosed = Convert.ToBoolean(item["IsClosed"]),
                        Status = Convert.ToString(item["Status"]),
                        CreatedBy = Convert.ToString(item["CreatedBy"]),
                        UpdatedBy = Convert.ToString(item["ModifiedBy"])
                    });
                }
                return projectlist;
            }
            catch (Exception Ex)
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Dispose();
            }
        }
        #endregion

        #region Create Project
        public bool CreateProject(Project project, int userId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("RahulTMS_AddUpdateProject", connection);
            try
            {
               command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@name", project.Name);
                command.Parameters.AddWithValue("@description", project.Description);
                command.Parameters.AddWithValue("@startDate", project.StartDate);
                command.Parameters.AddWithValue("@endDate", project.EndDate);
                command.Parameters.AddWithValue("@status", project.Status);
                command.Parameters.AddWithValue("@query", 1);

                connection.Open();
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception Ex)
            {
                throw;
            }
            finally
            {
                connection.Dispose();
                command.Dispose();
            }
        }
        #endregion

        public DataTable GetProjectDetails(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("RahulTMS__GetProjectDetailById", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;

                adapter.Fill(table);
                return table;
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        public bool EditProject(Project project, int userId)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("RahulTMS_AddUpdateProject", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", project.Id);
                cmd.Parameters.AddWithValue("@id", project.Id);
                cmd.Parameters.AddWithValue("@name", project.Name);
                cmd.Parameters.AddWithValue("@description", project.Description);
                cmd.Parameters.AddWithValue("@startDate", project.StartDate);
                cmd.Parameters.AddWithValue("@endDate", project.EndDate);
                cmd.Parameters.AddWithValue("@status", project.Status);
                cmd.Parameters.AddWithValue("@isClosed", project.IsClosed);
                cmd.Parameters.AddWithValue("@query", 4);

                string res = cmd.ExecuteNonQuery().ToString();
                if (string.IsNullOrEmpty(res))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Delete Project
        public bool DeleteProject(int projectId, int userId)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("RahulTMS_AddUpdateProject", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", projectId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@query", 3);

                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected > 0;
            }
            catch (Exception Ex)
            {
                return false;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }
        #endregion
    }
}