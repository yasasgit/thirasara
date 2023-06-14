using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class FarmerForm : Form
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        private SqlConnection connection;

        public FarmerForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            lblNic.Text = UserManagement.Instance.UserNic;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            var dataTable = new DataTable();
            connection = new SqlConnection(connectionString);
            var storedProcedure = "GetFieldData";
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nic", UserManagement.Instance.UserNic);
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            fieldDataGridView.DataSource = dataTable;
        }

        private void fieldDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var dataTable = new DataTable();
            if (fieldDataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = fieldDataGridView.SelectedRows[0];
                var fieldId = Convert.ToInt32(selectedRow.Cells["field_id"].Value);
                dataTable.Clear();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var storedProcedure = "GetCropCycleData";
                    using (var command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fieldId", fieldId);
                        using (var adapter = new SqlDataAdapter(command))
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
            var dataTable = new DataTable();
            if (cropCyleDataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = cropCyleDataGridView.SelectedRows[0];
                var cropCycleId = Convert.ToInt32(selectedRow.Cells["crop_cycle_id"].Value);
                dataTable.Clear();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var storedProcedure = "GetEnvironmentData";
                    using (var command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cropCycleId", cropCycleId);
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                environmentDataGridView.DataSource = dataTable;
                connection.Close();
                var dataTableF = new DataTable();
                dataTableF.Clear();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var storedProcedure = "RequiredFertilizer";
                    using (var command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@cropCycleId", cropCycleId);
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTableF);
                        }
                    }
                }

                fertilizerDataGridView.DataSource = dataTableF;
            }
        }

        private void btnPnD_Click(object sender, EventArgs e)
        {
            var pdForm = new PestDiseaseForm();
            pdForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserManagement.Instance.UserNic = null;
            LoginForm.Instance.Show();
            Close();
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            var afForm = new AddFieldForm();
            afForm.ShowDialog();
        }

        [Obsolete]
        private void btnPredict_Click(object sender, EventArgs e)
        {
            var mdntest = new AdvancedAlgorithm();
            mdntest.PerformLinearRegression();
            MessageBox.Show("Predictions Entered to the Database");
            Form currentForm = new FarmerForm();
            Close();
            currentForm.Show();
        }
    }
}