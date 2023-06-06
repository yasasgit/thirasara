using System.Configuration;
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

        [System.Obsolete]
        private void btnPredict_Click(object sender, System.EventArgs e)
        {
            AdvancedAlgorithm mdntest = new AdvancedAlgorithm();
            mdntest.PerformLinearRegression();
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
            }
        }

        private void label7_Click(object sender, System.EventArgs e)
        {

        }

        private void button4_Click(object sender, System.EventArgs e)
        {

        }

        private void lblLandUsage_Click(object sender, System.EventArgs e)
        {

        }

        private void label6_Click(object sender, System.EventArgs e)
        {

        }
    }
}
