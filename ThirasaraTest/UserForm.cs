using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

namespace ThirasaraTest
{
    public partial class UserForm : Form
    {
        private SqlConnection connection;
        private DataTable dataTable;
        public UserForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtEmail.Text = UserManagement.Instance.LoggedInUser;
        }

        private void UserForm_Load(object sender, System.EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            connection = new SqlConnection(connectionString);

            dataTable = new DataTable();

            string storedProcedure = "GetCropCycleData";
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@email", UserManagement.Instance.LoggedInUser);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            cropCycleDataGridView.DataSource = dataTable;
        }

    }
}
