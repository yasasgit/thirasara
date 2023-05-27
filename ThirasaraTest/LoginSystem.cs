using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public class LoginSystem
{
    string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

    public string Login(string email, string password)
    {
        byte[] passwordHash = CalculateSHA1Hash(password);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT password_hashed, account_type FROM user_data WHERE email = @email";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        byte[] storedHash = (byte[])reader["password_hashed"];
                        if (CompareByteArrays(passwordHash, storedHash))
                        {
                            string userType = (string)reader["account_type"];
                            return userType;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Read Failed");
                    }

                }
            }
        }
        return "fail";
    }

    private byte[] CalculateSHA1Hash(string input)
    {
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            return sha1.ComputeHash(inputBytes);
        }
    }

    private bool CompareByteArrays(byte[] array1, byte[] array2)
    {
        if (array1.Length != array2.Length)
            return false;

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }
        return true;
    }
}
