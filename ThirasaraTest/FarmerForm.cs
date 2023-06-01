using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class FarmerForm : Form
    {
        private SqlConnection connection;
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        
        public FarmerForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            lblNic.Text = UserManagement.Instance.UserNic;
        }

        private void UserForm_Load(object sender, System.EventArgs e)
        {
            DataTable dataTable = new DataTable();
            connection = new SqlConnection(connectionString);
            string storedProcedure = "GetCropCycleData";
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nic", UserManagement.Instance.UserNic);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            cropCycleDataGridView.DataSource = dataTable;
        }

        private void btnPnD_Click(object sender, System.EventArgs e)
        {
            var pdForm = new PestDiseaseForm();
            pdForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, System.EventArgs e)
        {
            UserManagement.Instance.UserNic = null;
            LoginForm.Instance.Show();
            this.Close();
        }

        private void CropCycleDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            if (cropCycleDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = cropCycleDataGridView.SelectedRows[0];
                int cropCycleId = Convert.ToInt32(selectedRow.Cells["crop_cycle_id"].Value);
                dataTable.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string storedProcedure = "RequiredFertilizer";
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cropCycleId", cropCycleId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                fertilizerDataGridView.DataSource = dataTable;
            }
        }

    }
}
