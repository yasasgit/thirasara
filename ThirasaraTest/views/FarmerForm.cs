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
            string storedProcedure = "GetFieldData";
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nic", UserManagement.Instance.UserNic);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }
            fieldDataGridView.DataSource = dataTable;
        }

        private void fieldDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            if (fieldDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = fieldDataGridView.SelectedRows[0];
                int fieldId = Convert.ToInt32(selectedRow.Cells["field_id"].Value);
                dataTable.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string storedProcedure = "GetCropCycleData";
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fieldId", fieldId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                cropCyleDataGridView.DataSource = dataTable;
            }
        }

        private void cropCyleDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            DataTable dataTable = new DataTable();
            if (cropCyleDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = cropCyleDataGridView.SelectedRows[0];
                int cropCycleId = Convert.ToInt32(selectedRow.Cells["crop_cycle_id"].Value);
                dataTable.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string storedProcedure = "GetEnvironmentData";
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
                environmentDataGridView.DataSource = dataTable;
                connection.Close();
                DataTable dataTableF = new DataTable();
                dataTableF.Clear();
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
                            adapter.Fill(dataTableF);
                        }
                    }
                }
                fertilizerDataGridView.DataSource = dataTableF;
            }
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

        private void btnAddField_Click(object sender, EventArgs e)
        {
            var afForm = new AddFieldForm();
            afForm.ShowDialog();
        }
    }
}
