using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class LoginForm : Form
    {
        public string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        private static LoginForm loginFormInstance;

        private LoginForm()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        public static LoginForm Instance
        {
            get
            {
                if (loginFormInstance == null)
                {
                    loginFormInstance = new LoginForm();
                }
                return loginFormInstance;
            }
        }

        private void passwordCheck_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !passwordCheck.Checked;
        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Password Cannot be null");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        private void txt_email_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtEmail.Text, emailPattern) == false)
            {
                errorProvider1.SetError(txtEmail, "please Enter Valid Email Address");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                string userType = UserManagement.Instance.Login(email, password);

                switch (userType)
                {
                    case "officer":
                        MessageBox.Show("Welcome Officer!");
                        LoginForm.Instance.Hide();
                        OfficerForm adminForm = new OfficerForm();
                        adminForm.Show();
                        break;

                    case "cultivator":
                        MessageBox.Show("Welcome Cultivator!");
                        LoginForm.Instance.Hide();
                        FarmerForm userForm = new FarmerForm();
                        userForm.Show();
                        break;

                    default:
                        throw new Exception("Invalid user type. Please contact the administrator.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registrationForm = new RegisterForm();
            registrationForm.ShowDialog();
        }
    }
}
