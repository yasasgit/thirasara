using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThirasaraTest;

namespace Hospital_Management_System
{
    public partial class Login_Form : Form
    {
        public string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        private const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\thirasaradb.mdf;Integrated Security=True";


        //email and password validation

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_pass.Text.Trim()))
            {
                txt_pass.Focus();
                errorProvider1.SetError(txt_pass, "Password Cannot be null");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        //validate the email pattern

        private void txt_email_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txt_email.Text, emailPattern) == false)
            {
                errorProvider1.SetError(txt_email, "please Enter Valid Email Address");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            // Get the email and password entered by the user
            string email = txt_email.Text.Trim();
            string password = txt_pass.Text;

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
                            // Close the login form
                            this.Hide();

                            // Open the main form
                            Main mainForm = new Main();
                            mainForm.Show();
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

        public Login_Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
