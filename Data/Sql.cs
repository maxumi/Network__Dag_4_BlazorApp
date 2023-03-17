
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Network__Dag_4_BlazorApp.Data
{
    public class Sql
    {
        public static string connectionString = "Data Source=LAPTOP-U1QPB21T;Initial Database=Blazor_LoginDB;User ID=sa;Password=Passw0rd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<User> Read()
        {
            List<User> User_list = new List<User>();
            SqlConnection Conn = new SqlConnection(connectionString);
            SqlCommand cmd = new("SELECT * FROM User_Info", Conn);
            Conn.Open();
            SqlDataReader DataRead = cmd.ExecuteReader();
            while (DataRead.Read())
            {
                User AllUser = new User() {
                    User_ID = DataRead.GetInt32(0),
                    User_Name = DataRead.GetString(1),
                    User_Password = DataRead.GetString(2) };
                User_list.Add(AllUser);
            }
            return User_list;

        }
        public static void Create(User user)
        {
			SqlConnection Conn = new SqlConnection(connectionString);
			SqlCommand cmd = new("INSERT INTO User_Info VALUES (@Name, @Password)", Conn);
			cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.User_Name;
			cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.User_Password;
			Conn.Open();
			cmd.ExecuteNonQuery();
			Conn.Close();
		}

        public static bool CheckLogin(User user)
        {
            List<User> User_list = new List<User>();
            SqlConnection Conn = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("SELECT * FROM User_Info WHERE [name] = @Name AND [password] = @Password", Conn);
			cmd.Parameters.AddWithValue("@Name", user.User_Name);
			cmd.Parameters.AddWithValue("@Password", user.User_Password);
			Conn.Open();
            SqlDataReader DataRead = cmd.ExecuteReader();
            while (DataRead.Read())
            {
                User AllUser = new User()
                {
                    User_ID = DataRead.GetInt32(0),
                    User_Name = DataRead.GetString(1),
                    User_Password = DataRead.GetString(2)
                };
                User_list.Add(AllUser);
            }
            if (User_list.Count > 0)
            {
                if (User_list[0].User_Name == user.User_Name && User_list[0].User_Password == user.User_Password)
                {
					return true;
				}
				else
                {
					return false;
				}
            }
            else
            {
                return false;
            }
        } 
    }
}
