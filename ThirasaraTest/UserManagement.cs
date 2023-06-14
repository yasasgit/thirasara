using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using ThirasaraTest;

public class UserManagement
{
    private static UserManagement instance;

    private readonly string connectionString =
        ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

    private UserManagement()
    {
    }

    public string UserNic { get; set; }

    public static UserManagement Instance
    {
        get
        {
            if (instance == null) instance = new UserManagement();
            return instance;
        }
    }

    public string Login(string email, string password)
    {
        var hashing = new Hashing();
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT nic, password_hashed, account_type FROM user_data WHERE email = @email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var nic = (string)reader["nic"];
                            var storedHash = (byte[])reader["password_hashed"];
                            var passwordHash = hashing.CalculateSHA1Hash(password);
                            if (hashing.CompareByteArrays(passwordHash, storedHash))
                            {
                                var userType = (string)reader["account_type"];
                                Instance.UserNic = nic;
                                return userType;
                            }

                            throw new Exception("Invalid email or password. Please try again.");
                        }

                        throw new Exception("Read Failed");
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