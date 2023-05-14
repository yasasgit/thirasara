using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class MainForm : Form
    {
        private const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\thirasaradb.mdf;Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Get the email and password entered by the user
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Create a new SQL connection using the connection string
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create a new SQL command to retrieve the user with the given email and password
                    string query = "SELECT * FROM User_Data WHERE email=@Email AND password=@Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters for the email and password
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        // Execute the command and get the result
                        SqlDataReader reader = command.ExecuteReader();

                        // If a user was found, display a message and close the form
                        if (reader.Read())
                        {
                            MessageBox.Show("Login successful!");
                            this.Close();
                        }
                        // If no user was found, display an error message
                        else
                        {
                            MessageBox.Show("Invalid email or password. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
