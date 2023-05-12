using System;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class TestClass : Form
{
    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\thirasaradb.mdf;Integrated Security=True");

    public TestClass()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // TODO: This line of code loads data into the 'yourDatabaseNameDataSet.YourTableName' table. You can move, or remove it, as needed.
        this.yourTableNameTableAdapter.Fill(this.yourDatabaseNameDataSet.test);
    }
}
