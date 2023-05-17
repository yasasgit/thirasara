using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Python.Runtime;

namespace ThirasaraTest
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {
            // Initialize the Python runtime
            PythonEngine.Initialize();

            // Execute Python code
            using (Py.GIL())  // Acquire the Python Global Interpreter Lock (GIL)
            {
                dynamic np = Py.Import("numpy");
                dynamic array = np.array(new int[] { 1, 2, 3, 4, 5 });
                dynamic sum = np.sum(array);

                Console.WriteLine("Sum: " + sum);
            }

            // Shutdown the Python runtime
            PythonEngine.Shutdown();
        }
    }


}
