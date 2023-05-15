using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.Optimization;

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
            // Retrieve the column values from your DataGridView or any other control
            double[] columnValues = GetColumnValues();

            // Define the objective function based on the column values
            Func<double, double> objectiveFunction = (x) =>
            {
                // Define your objective function here based on the column values
                // For example, you can calculate the sum of squared differences from a target value
                double sumOfSquaredDifferences = 0;
                foreach (var value in columnValues)
                {
                    double difference = value - x;
                    sumOfSquaredDifferences += difference * difference;
                }
                return sumOfSquaredDifferences;
            };

            // Use an optimization algorithm to find the optimal value
            //var result = NelderMeadMinimizer.Minimize(objectiveFunction, initialValue: 0);

            // Update the user interface with the optimal value
            //txtOptimalValue.Text = result.MinimizingPoint.ToString();
        }

        private double[] GetColumnValues()
        {
            // Retrieve the column values from your DataGridView or any other control
            // Return an array of double values representing the column data
            // Example:
            // double[] columnValues = dataGridView1.Rows.Cast<DataGridViewRow>()
            //     .Select(row => Convert.ToDouble(row.Cells["ColumnName"].Value))
            //     .ToArray();
            // return columnValues;
            throw new NotImplementedException(); // Implement this method based on your specific UI design
        }
    }
}
