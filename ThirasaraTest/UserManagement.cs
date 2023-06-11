using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirasaraTest;

public class UserManagement
{
    string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    private static UserManagement instance;
    public string UserNic { get; set; }

    private UserManagement()
    {
    }

    public static UserManagement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UserManagement();
            }
            return instance;
        }
    }

    public string Login(string email, string password)
    {
        Hashing hashing = new Hashing();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nic, password_hashed, account_type FROM user_data WHERE email = @email";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nic = (string)reader["nic"];
                            byte[] storedHash = (byte[])reader["password_hashed"];
                            byte[] passwordHash = hashing.CalculateSHA1Hash(password);
                            if (hashing.CompareByteArrays(passwordHash, storedHash))
                            {
                                string userType = (string)reader["account_type"];
                                UserManagement.Instance.UserNic = nic;
                                return userType;
                            }
                            else
                            {
                                throw new Exception("Invalid email or password. Please try again.");
                            }
                        }
                        else
                        {
                            throw new Exception("Read Failed");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return "fail";
    }
}
