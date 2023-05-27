using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class LoginForm : Form
    {
        public string emailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public LoginForm()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
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
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            LoginSystem loginSystem = new LoginSystem();
            string userType = loginSystem.Login(email, password);

            switch (userType)
            {
                case "officer":
                    MessageBox.Show("Login successful as Officer!");
                    this.Hide();
                    AdminForm adminForm = new AdminForm();
                    adminForm.Show();
                    break;

                case "cultivator":
                    MessageBox.Show("Login successful as Cultivator!");
                    this.Hide();
                    UserForm userForm = new UserForm();
                    userForm.Show();
                    break;

                default:
                    MessageBox.Show("Invalid email or password. Please try again.");
                    break;
            }
        }
    }
}
