using Accord.IO;
using Accord.MachineLearning.Rules;
using Accord.Math;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression.Linear;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

public class AdvancedAlgorithm
{
    string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
    [Obsolete]
    public void PerformLinearRegression()
    {
            var crop_cycle_id = 1000;
            var reader = new CsvReader("model/train_data.csv", hasHeaders: true);
            var ols = new OrdinaryLeastSquares()
            {
                UseIntercept = true
            };

            List<string[]> contentList = reader.ReadToEnd();
            string[][] contents = contentList.ToArray();

            List<double[]> firstList = new List<double[]>();
            foreach (var row in contents)
            {
                double[] firstValues = row.Take(9).Select(Double.Parse).ToArray();
                firstList.Add(firstValues);
            }
            double[][] inputs = firstList.ToArray();

            List<double> secondList = new List<double>();
            foreach (var row in contents)
            {
                string secondValue = row[9];
                double parsedValue = double.Parse(secondValue);
                secondList.Add(parsedValue);
            }
            double[] outputs = secondList.ToArray();

            MultipleLinearRegression regression = ols.Learn(inputs, outputs);
            double a = regression.Weights[0];
            double b = regression.Weights[1];
            double c = regression.Intercept;
            double[] predicted = regression.Transform(inputs);
            double error = new SquareLoss(outputs).Loss(predicted);
            double r2 = new RSquaredLoss(numberOfInputs: 2, expected: outputs).Loss(predicted);
            var r2loss = new RSquaredLoss(numberOfInputs: 2, expected: outputs)
            {
                Adjust = true,
            };
            double ar2 = r2loss.Loss(predicted);
            double ur2 = regression.CoefficientOfDetermination(inputs, outputs, adjust: true);

        //testing
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var testReader = new CsvReader("model/test_data.csv", hasHeaders: true);
            List<string[]> testContentList = testReader.ReadToEnd();
            string[][] testContents = testContentList.ToArray();
            List<double[]> testInputs = new List<double[]>();
            foreach (var row in testContents)
            {
                double[] testValues = row.Take(9).Select(Double.Parse).ToArray();
                testInputs.Add(testValues);
            }
            double[] prediction = regression.Transform(testInputs.ToArray());
            Console.WriteLine("Predicted values:");
            foreach (var value in prediction)
            {
                Console.WriteLine(value);
            }
            conn.Close();
        }
    }

    public string PerformAprioriAnalysis()
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT crop_cycle, pest_disease FROM crop_cycle_pest_disease ORDER BY crop_cycle";
            using (var cmd = new SqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    DataTable datasetTable = new DataTable();
                    datasetTable.Load(reader);

                    Dictionary<int, SortedSet<int>> rowDictionary = new Dictionary<int, SortedSet<int>>();
                    foreach (DataRow row in datasetTable.Rows)
                    {
                        short firstElement = (short)row[0];
                        if (rowDictionary.ContainsKey(firstElement))
                        {
                            SortedSet<int> existingSet = rowDictionary[firstElement];
                            for (int i = 1; i < row.ItemArray.Length; i++)
                            {
                                existingSet.Add((short)row[i]);
                            }
                        }
                        else
                        {
                            SortedSet<int> newSet = new SortedSet<int>();
                            for (int i = 1; i < row.ItemArray.Length; i++)
                            {
                                newSet.Add((short)row[i]);
                            }
                            rowDictionary.Add(firstElement, newSet);
                        }
                    }
                    SortedSet<int>[] dataset = rowDictionary.Values.ToArray();
                    for (int i = 0; i < dataset.Length; i++)
                    {
                        foreach (int value in dataset[i])
                        {
                            Console.Write(value);
                        }
                    }

                    var apriori = new Apriori(threshold: 3, confidence: 0);
                    AssociationRuleMatcher<int> classifier = apriori.Learn(dataset);

                    int[][] matches = classifier.Decide(new[] { 1002, 1001 });

                    StringBuilder sb = new StringBuilder();

                    foreach (var match in matches)
                    {
                        sb.AppendLine($"new int[] {{ {string.Join(", ", match)} }}");
                    }

                    AssociationRule<int>[] rules = classifier.Rules;
                    

                    foreach (var rule in rules)
                    {
                        var antecedent = string.Join(", ", rule.X);
                        var consequent = string.Join(", ", rule.Y);
                        sb.AppendLine($"[{antecedent}] -> [{consequent}]; support: {rule.Support}, confidence: {rule.Confidence}");
                    }

                    return sb.ToString();
                }
            }
        }
    }
}
