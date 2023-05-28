using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            txtEmail.Text = UserManagement.Instance.LoggedInUser;
        }
    }
}
