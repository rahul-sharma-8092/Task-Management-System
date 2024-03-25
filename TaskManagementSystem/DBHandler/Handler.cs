using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using TaskManagementSystem.Models;
using TaskManagementSystem.Common;

namespace TaskManagementSystem.DBHandler
{
    public class Handler
    {
        string connString = ConfigurationManager.ConnectionStrings["dbRahulConn"].ConnectionString;

        public bool CreateUser(User user)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS_AddUpdateUser", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fullName", user.FullName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", PasswordHash.CreateHashBCrypt(user.Password));
                cmd.Parameters.AddWithValue("@doj", user.DateOfJoining);
                cmd.Parameters.AddWithValue("@RoleId", Convert.ToInt32(user.Role));
                cmd.Parameters.AddWithValue("@mobile", user.Mobile);
                cmd.Parameters.AddWithValue("@image", Service.ImageSave(user.Image));
                cmd.Parameters.AddWithValue("@query", 1);

                int res = cmd.ExecuteNonQuery();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public bool UpdateUser(User user)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS_AddUpdateUser", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@userId", user.UserId);
                cmd.Parameters.AddWithValue("@fullName", user.FullName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@doj", user.DateOfJoining);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@mobile", user.Mobile);
                cmd.Parameters.AddWithValue("@image", user.Image == null ? user.ImagePath : Service.ImageSave(user.Image));
                cmd.Parameters.AddWithValue("@query", 2);

                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected > 0;
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

        public bool DeleteUser(int userId)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS_AddUpdateUser", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@query", 3);

                int rowAffected = cmd.ExecuteNonQuery();

                return rowAffected > 0;
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

        public List<User> GetUserList(int pageIndex, int pageSize)
        {
            List<User> list = new List<User>();
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS_GetUsersList", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    list.Add(new User
                    {
                        UserId = Convert.ToInt32(item["UserId"]),
                        FullName = Convert.ToString(item["FullName"]),
                        Email = Convert.ToString(item["Email"]),
                        Password = Convert.ToString(item["Password"]),
                        Mobile = Convert.ToString(item["Mobile"]),
                        DateOfJoining = Convert.ToDateTime(item["DateOfJoining"]),
                        Role = Convert.ToString(item["Role"]),
                        RoleId = Convert.ToInt32(item["RoleId"]),
                        ImagePath = Convert.ToString(item["Image"]),
                        IsDeleted = Convert.ToBoolean(item["IsDeleted"]),
                        TotalUsers = Convert.ToInt32(item["TotalRows"]),
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public bool IsEmailExists(string email)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS_CheckEmailExists", conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@email", email);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public User GetUserDetails(string email = "", int id = 0)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS__GetUserDetailByEmail", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@userId", id);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();

                User user = new User();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    user.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                    user.FullName = Convert.ToString(dt.Rows[0]["FullName"]);
                    user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    user.Password = Convert.ToString(dt.Rows[0]["Password"]);
                    user.Mobile = Convert.ToString(dt.Rows[0]["Mobile"]);
                    user.DateOfJoining = Convert.ToDateTime(dt.Rows[0]["DateOfJoining"]);
                    user.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                    user.Role = Convert.ToString(dt.Rows[0]["Role"]);
                    user.ImagePath = Convert.ToString(dt.Rows[0]["Image"]);
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
            SqlCommand cmd = new SqlCommand("RahulTMS__GetUsersRoleByEmail", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                List<string> roleList = new List<string>();
                string[] roles = new string[] { };

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
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

        public bool SaveForgotPassToken(string email, int userId, string token)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS__AddUpdateForgotPasswordToken", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@token", token);
                cmd.Parameters.AddWithValue("@query", 1);

                int res = cmd.ExecuteNonQuery();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public DataTable GetForgotPassToken(string email, int userId)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS__AddUpdateForgotPasswordToken", conn);
            try
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@query", 2);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;

                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public bool PasswordChanged(string email, int userId, string password)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("RahulTMS_AddUpdateUser", conn);
            try
            {
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@password", PasswordHash.CreateHashBCrypt(password));
                cmd.Parameters.AddWithValue("@query", 4);

                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception)
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