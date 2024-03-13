using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.DBHandler
{
    public class Handler
    {
        string connString = ConfigurationManager.ConnectionStrings[""].ConnectionString;

        public User GetUserDetails(string email)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetUserDetails", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();

                User user = new User();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    user.Id = Convert.ToInt32(dt.Rows[0]["Name"]);
                    user.Name = Convert.ToString(dt.Rows[0]["Name"]);
                    user.Email = Convert.ToString(dt.Rows[0]["Name"]);
                    user.Password = Convert.ToString(dt.Rows[0]["Name"]);
                    user.Mobile = Convert.ToString(dt.Rows[0]["Name"]);
                    user.Role = Convert.ToString(dt.Rows[0]["Name"]);
                }
                return user;
            }
            catch (Exception Ex)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public string[] GetUsersRole(string email)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetUsersRole__ByEmail", conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> roleList = new List<string>();
                string[] roles = new string[] { };

                if (dt.Rows.Count > 0)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        roleList.Add(Convert.ToString(row["Role"]));
                    }
                    roles = roleList.ToArray();
                }
                return roles;
            }
            catch (Exception Ex)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }
    }
}