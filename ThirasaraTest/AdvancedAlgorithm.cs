using Accord.MachineLearning.Rules;
using Accord.Math;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression.Linear;
using CsvHelper;
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
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var crop_cycle_id = 1003;
            var train_df = new DataTable();
            train_df = new CsvReader("path/to/your/file.csv").ToTable();
            var test_query = $@" = {crop_cycle_id}";
            var test_df = new DataTable();
            using (var adapter = new SqlDataAdapter(test_query, conn))
            {
                adapter.Fill(test_df);
            }
            var inputs = new double[train_df.Rows.Count][];
            var outputs = new double[train_df.Rows.Count];
            double[][] input =
                {
                new double[] { 1, 1 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 0, 0 },
            };
            double[] output = { 1, 1, 1, 1 };
            for (var i = 0; i < train_df.Rows.Count; i++)
            {
                var row = train_df.Rows[i];
                inputs[i] = new double[]
                {
                    Convert.ToDouble(row["plant_density_ha"]),
                    Convert.ToDouble(row["soil_ph"]),
                    Convert.ToDouble(row["soil_texture_level"]),
                    Convert.ToDouble(row["nitrogen_kg_ha"]),
                    Convert.ToDouble(row["phosphorus_kg_ha"]),
                    Convert.ToDouble(row["potassium_kg_ha"]),
                    Convert.ToDouble(row["other_nutrients_kg_ha"]),
                    Convert.ToDouble(row["fertilizer_kg_ha"]),
                    Convert.ToDouble(row["severity_level"]),
                    Convert.ToDouble(row["temperature_c"]),
                    Convert.ToDouble(row["rainfall_irrigation_mm"]),
                    Convert.ToDouble(row["humidity_perc"]),
                    Convert.ToDouble(row["wind_speed_m_s"]),
                    Convert.ToDouble(row["sunlight_exposure_h_day"]),
                    Convert.ToDouble(row["human_hours_ha"])
                };
                outputs[i] = Convert.ToDouble(row["yield_kg_ha"]);
            }
            var ols = new OrdinaryLeastSquares()
            {
                UseIntercept = true
            };
            MultipleLinearRegression regression = ols.Learn(inputs, outputs);
            double a = regression.Weights[0]; // a = 0
            double b = regression.Weights[1]; // b = 0
            double c = regression.Intercept;  // c = 1
            double[] predicted = regression.Transform(inputs);
            double error = new SquareLoss(outputs).Loss(predicted);
            double r2 = new RSquaredLoss(numberOfInputs: inputs[0].Length, expected: outputs).Loss(predicted);
            var r2loss = new RSquaredLoss(numberOfInputs: inputs[0].Length, expected: outputs)
            {
                Adjust = true
            };
            double ar2 = r2loss.Loss(predicted); // should be 1
            double ur2 = regression.CoefficientOfDetermination(inputs, outputs, adjust: true); // should be 1
            var predictedValue = regression.Transform(test_df.Rows[0].ItemArray.Skip(1).Select(x => Convert.ToDouble(x)).ToArray());
            var update_query = $"UPDATE crop_cycle_data SET predicted_yield_kg_ha = {predictedValue} WHERE crop_cycle_id = {crop_cycle_id}";
            using (var command = new SqlCommand(update_query, conn))
            {
                command.ExecuteNonQuery();
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
