using System.Data.SqlClient;
using System;
using System.Windows.Forms;
using System.Configuration;

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
            this.Close();

        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNic_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
