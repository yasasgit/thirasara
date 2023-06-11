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
            var crop_cycle_id = 1163;
            conn.Open();

            var testDataQuery = "SELECT plant_density_ha, temperature_c, rainfall_irrigation_mm, humidity_perc, wind_speed_m_s, sunlight_exposure_h_day, soil_ph, soil_texture_level, severity_level FROM crop_cycle_data AS ccd JOIN environment_data AS ed ON ed.crop_cycle = ccd.crop_cycle_id JOIN field_data AS fd ON ccd.field = fd.field_id JOIN soil_texture_data AS std ON fd.soil_texture = std.soil_texture_id JOIN fertilizer_data AS fert ON ccd.fertilizer = fert.fertilizer_id JOIN crop_cycle_pest_disease AS ccpd ON ccd.crop_cycle_id = ccpd.crop_cycle JOIN pest_disease_data AS pd ON pd.pest_disease_id = ccpd.pest_disease WHERE crop_cycle_id = @CropCycleId";
            var testDataCommand = new SqlCommand(testDataQuery, conn);
            testDataCommand.Parameters.AddWithValue("@CropCycleId", crop_cycle_id);
            var testReader = testDataCommand.ExecuteReader();

            List<double[]> testInputs = new List<double[]>();
            while (testReader.Read())
            {
                double[] testValues = new double[9];
                for (int i = 0; i < 9; i++)
                {
                    testValues[i] = Convert.ToDouble(testReader[i]);
                }
                testInputs.Add(testValues);
            }
            testReader.Close();

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
