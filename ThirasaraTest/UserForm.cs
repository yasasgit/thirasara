using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            lblEmail.Text = UserManagement.Instance.LoggedInUser;
        }
    }
}
