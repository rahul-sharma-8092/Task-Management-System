using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TaskManagementSystem.Models;
using System.Runtime.Remoting.Messaging;
using System.Web.Hosting;

namespace TaskManagementSystem.DBHandler
{
    public class ProjectHandler
    {
        #region Globel Objects/Variable
        string _connectionString = string.Empty;
        DataTable table = new DataTable();
        Project project = new Project();
        #endregion

        public List<Project> GetProjectList()
        {
            List<Project> projectlist = new List<Project>();

            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand("Sp__AddUpdateProject", connection);
            try
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@query", 1);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                foreach (DataRow item in table.Rows)
                {
                    projectlist.Add(new Project()
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        Name = item["Name"].ToString(),
                        Description = item["Description"].ToString(),
                        StartDate = Convert.ToDateTime(item["StartDate"]),
                        EndDate = Convert.ToDateTime(item["EndDate"]),
                        IsClosed = Convert.ToBoolean(item["IsClosed"])
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

        public bool CreateProject(Project project)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("CreateProject", connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", project.Name);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@StartDate", project.StartDate);
                command.Parameters.AddWithValue("@EndDate", project.EndDate);
                command.Parameters.AddWithValue("@IsClosed", project.IsClosed);
                command.Parameters.AddWithValue("@query", 2);

                connection.Open();
                string res = command.ExecuteNonQuery().ToString();
                if (string.IsNullOrEmpty(res))
                {
                    return false;
                }
                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
            finally
            {
                connection.Dispose();
                command.Dispose();
            }
        }

        public DataTable GetProjectDetails(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("Sp__AddUpdateProject", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@query", 3);

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

        public bool EditProject(Project project)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("Sp__AddUpdateProject", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", project.Id);
                cmd.Parameters.AddWithValue("@name", project.Name);
                cmd.Parameters.AddWithValue("@description", project.Description);
                cmd.Parameters.AddWithValue("@startDate", project.StartDate);
                cmd.Parameters.AddWithValue("@endDate", project.EndDate);
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

        public bool DeleteProject(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("Sp__AddUpdateProject", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@query", 5);

                string res = cmd.ExecuteNonQuery().ToString();
                if (string.IsNullOrEmpty(res))
                {
                    return false;
                }
                return true;
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
    }
}