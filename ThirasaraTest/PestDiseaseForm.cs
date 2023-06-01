using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class PestDiseaseForm : Form
    {
        public PestDiseaseForm()
        {
            InitializeComponent();
        }
private void PestDiseaseForm_Load(object sender, System.EventArgs e)
        {
            AdvancedAlgorithm mdntest = new AdvancedAlgorithm();
            txtApriori.Text = mdntest.PerformAprioriAnalysis();
        }
    }
}
