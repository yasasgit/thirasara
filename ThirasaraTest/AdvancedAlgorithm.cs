using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Accord.IO;
using Accord.MachineLearning.Rules;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression.Linear;

public class AdvancedAlgorithm
{
    private readonly string connectionString =
        ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

    [Obsolete]
    public void PerformLinearRegression()
    {
        var reader = new CsvReader("model/train_data.csv", true);
        var ols = new OrdinaryLeastSquares
        {
            UseIntercept = true
        };

        var contentList = reader.ReadToEnd();
        var contents = contentList.ToArray();

        var firstList = new List<double[]>();
        foreach (var row in contents)
        {
            var firstValues = row.Take(9).Select(double.Parse).ToArray();
            firstList.Add(firstValues);
        }

        var inputs = firstList.ToArray();

        var secondList = new List<double>();
        foreach (var row in contents)
        {
            var secondValue = row[9];
            var parsedValue = double.Parse(secondValue);
            secondList.Add(parsedValue);
        }

        var outputs = secondList.ToArray();

        var regression = ols.Learn(inputs, outputs);
        var a = regression.Weights[0];
        var b = regression.Weights[1];
        var c = regression.Intercept;
        var predicted = regression.Transform(inputs);
        var error = new SquareLoss(outputs).Loss(predicted);
        var r2 = new RSquaredLoss(2, outputs).Loss(predicted);
        var r2loss = new RSquaredLoss(2, outputs)
        {
            Adjust = true
        };
        var ar2 = r2loss.Loss(predicted);
        var ur2 = regression.CoefficientOfDetermination(inputs, outputs, true);

        //testing
        using (var conn = new SqlConnection(connectionString))
        {
            var crop_cycle_id = 1163;
            conn.Open();

            var testDataQuery =
                "SELECT plant_density_ha, temperature_c, rainfall_irrigation_mm, humidity_perc, wind_speed_m_s, sunlight_exposure_h_day, soil_ph, soil_texture_level, severity_level FROM crop_cycle_data AS ccd JOIN environment_data AS ed ON ed.crop_cycle = ccd.crop_cycle_id JOIN field_data AS fd ON ccd.field = fd.field_id JOIN soil_texture_data AS std ON fd.soil_texture = std.soil_texture_id JOIN fertilizer_data AS fert ON ccd.fertilizer = fert.fertilizer_id JOIN crop_cycle_pest_disease AS ccpd ON ccd.crop_cycle_id = ccpd.crop_cycle JOIN pest_disease_data AS pd ON pd.pest_disease_id = ccpd.pest_disease WHERE crop_cycle_id = @CropCycleId";
            var testDataCommand = new SqlCommand(testDataQuery, conn);
            testDataCommand.Parameters.AddWithValue("@CropCycleId", crop_cycle_id);
            var testReader = testDataCommand.ExecuteReader();

            var testInputs = new List<double[]>();
            while (testReader.Read())
            {
                var testValues = new double[9];
                for (var i = 0; i < 9; i++) testValues[i] = Convert.ToDouble(testReader[i]);
                testInputs.Add(testValues);
            }

            testReader.Close();

            var prediction = regression.Transform(testInputs.ToArray());
            Console.WriteLine("Predicted values:");
            foreach (var value in prediction) Console.WriteLine(value);
            conn.Close();
        }
    }

    public string PerformAprioriAnalysis()
    {
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var query = "SELECT crop_cycle, pest_disease FROM crop_cycle_pest_disease ORDER BY crop_cycle";
            using (var cmd = new SqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var datasetTable = new DataTable();
                    datasetTable.Load(reader);

                    var rowDictionary = new Dictionary<int, SortedSet<int>>();
                    foreach (DataRow row in datasetTable.Rows)
                    {
                        var firstElement = (short)row[0];
                        if (rowDictionary.ContainsKey(firstElement))
                        {
                            var existingSet = rowDictionary[firstElement];
                            for (var i = 1; i < row.ItemArray.Length; i++) existingSet.Add((short)row[i]);
                        }
                        else
                        {
                            var newSet = new SortedSet<int>();
                            for (var i = 1; i < row.ItemArray.Length; i++) newSet.Add((short)row[i]);
                            rowDictionary.Add(firstElement, newSet);
                        }
                    }

                    var dataset = rowDictionary.Values.ToArray();
                    for (var i = 0; i < dataset.Length; i++)
                        foreach (var value in dataset[i])
                            Console.Write(value);

                    var apriori = new Apriori(3, 0);
                    var classifier = apriori.Learn(dataset);

                    var matches = classifier.Decide(new[] { 1002, 1001 });

                    var sb = new StringBuilder();

                    foreach (var match in matches) sb.AppendLine($"new int[] {{ {string.Join(", ", match)} }}");

                    var rules = classifier.Rules;


                    foreach (var rule in rules)
                    {
                        var antecedentLabels = GetLabelsForValues(rule.X.ToArray());
                        var consequentLabels = GetLabelsForValues(rule.Y.ToArray());
                        var antecedent = string.Join(", ", antecedentLabels);
                        var consequent = string.Join(", ", consequentLabels);
                        sb.AppendLine(
                            $"If your crop cycle have \"{antecedent}\" you may get \"{consequent}\" support: {rule.Support}, confidence: {rule.Confidence}");
                    }

                    return sb.ToString();
                }
            }
        }
    }

    private string[] GetLabelsForValues(int[] values)
    {
        var labels = new List<string>();
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            foreach (var value in values)
            {
                var query = "SELECT pest_disease_name FROM pest_disease_data WHERE pest_disease_id = @Value";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Value", value);
                var label = command.ExecuteScalar()?.ToString();
                labels.Add(label);
            }

            connection.Close();
        }

        return labels.ToArray();
    }
}