using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Hashing hashing = new Hashing();
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO user_data (nic, email, first_name, last_name, phone_number, password_hashed, account_type) " +
                                              "VALUES (@Nic, @Email, @FirstName, @LastName, @PhoneNumber, @Password, @Account_type)";

                        command.Parameters.AddWithValue("@Nic", txtNic.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        command.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                        command.Parameters.AddWithValue("@Password", hashing.CalculateSHA1Hash(txtPassword.Text));
                        command.Parameters.AddWithValue("@Account_type", "cultivator");
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Account Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while inserting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
