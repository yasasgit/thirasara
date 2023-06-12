using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class OfficerForm : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public OfficerForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            lblNic.Text = UserManagement.Instance.UserNic;
        }



        private void btnLogout_Click(object sender, System.EventArgs e)
        {
            UserManagement.Instance.UserNic = null;
            LoginForm.Instance.Show();
            this.Close();
        }

        private void OfficerForm_Load(object sender, System.EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT dbo.GetTotalLandUsage() AS TotalLandUsage", connection))
                {
                    decimal totalLandUsage = (decimal)command.ExecuteScalar();
                    lblLandUsage.Text = totalLandUsage.ToString();
                }
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT dbo.GetTotalPredictedYield() AS TotalPredictedYield", connection))
                {
                    decimal predictedYield = (decimal)command.ExecuteScalar();
                    lblPredictedYield.Text = predictedYield.ToString();
                }
                connection.Close();
            }
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("ResetPredictedYield", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Predictions have been reset");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
