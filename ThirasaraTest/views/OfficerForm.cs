using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class OfficerForm : Form
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public OfficerForm()
        {
            InitializeComponent();
            lblNic.Text = UserManagement.Instance.UserNic;
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserManagement.Instance.UserNic = null;
            LoginForm.Instance.Show();
            Close();
        }

        private void OfficerForm_Load(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var dataTable = new DataTable();
                connection.Open();
                using (var command = new SqlCommand("SELECT dbo.GetTotalLandUsage() AS TotalLandUsage", connection))
                {
                    var totalLandUsage = (decimal)command.ExecuteScalar();
                    lblLandUsage.Text = totalLandUsage.ToString();
                }

                using (var command = new SqlCommand("SELECT dbo.GetTotalPredictedYield() AS TotalPredictedYield",
                           connection))
                {
                    var predictedYield = (decimal)command.ExecuteScalar();
                    lblPredictedYield.Text = predictedYield.ToString();
                }

                using (var command = new SqlCommand(
                           "SELECT TOP 3 ud.first_name, ud.last_name, SUM(ccd.yield_kg_ha) AS total_yield, SUM(fd.size_ha) AS total_size, ud.phone_number FROM user_data AS ud JOIN field_data AS fd ON ud.nic = fd.cultivator JOIN crop_cycle_data AS ccd ON fd.field_id = ccd.field GROUP BY ud.first_name, ud.last_name, ud.phone_number ORDER BY total_yield DESC",
                           connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                topFarmerDataGridView.DataSource = dataTable;
                connection.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("ResetPredictedYield", connection);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Predictions have been reset");
                    Form currentForm = new OfficerForm();
                    Close();
                    currentForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}