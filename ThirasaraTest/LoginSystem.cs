using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public class LoginSystem
{
    private const string CONNECTION_STRING = @"Data Source=DESKTOP-A3HT73H;Initial Catalog=thirasara_db;Integrated Security=True;";
    public bool Login(string email, string password)
    {
        byte[] passwordHash = CalculateSHA1Hash(password);

        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "SELECT password_hashed FROM user_data WHERE email = @email";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        byte[] storedHash = (byte[])reader["password_hashed"];
                        return CompareByteArrays(passwordHash, storedHash);
                    }
                    else
                    {
                        Console.WriteLine("Read Failed");
                    }

                }
            }
        }
        return false;
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
