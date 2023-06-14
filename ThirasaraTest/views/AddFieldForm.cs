using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThirasaraTest
{
    public partial class AddFieldForm : Form
    {
        private readonly string connectionString =
            ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public AddFieldForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var sizeHa = decimal.Parse(sizeHaTextBox.Text);
                var fieldLocation = fieldLocationTextBox.Text;
                var soilNitrogen = byte.Parse(soilNitrogenTextBox.Text);
                var soilPhosphorus = byte.Parse(soilPhosphorusTextBox.Text);
                var soilPotassium = byte.Parse(soilPotassiumTextBox.Text);
                var soilPh = decimal.Parse(soilPhTextBox.Text);
                var soilTexture = short.Parse(soilTextureComboBox.Text);

                Console.Out.WriteLine(soilTexture);

                var query = "INSERT INTO field_data (cultivator, size_ha, field_location, soil_nitrogen_kg_ha, " +
                            "soil_phosphorus_kg_ha, soil_potassium_kg_ha, soil_ph, soil_texture) " +
                            "VALUES (@cultivator, @sizeHa, @fieldLocation, @soilNitrogen, @soilPhosphorus, " +
                            "@soilPotassium, @soilPh, @soilTexture)";

                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cultivator", UserManagement.Instance.UserNic);
                    command.Parameters.AddWithValue("@sizeHa", sizeHa);
                    command.Parameters.AddWithValue("@fieldLocation", fieldLocation);
                    command.Parameters.AddWithValue("@soilNitrogen", soilNitrogen);
                    command.Parameters.AddWithValue("@soilPhosphorus", soilPhosphorus);
                    command.Parameters.AddWithValue("@soilPotassium", soilPotassium);
                    command.Parameters.AddWithValue("@soilPh", soilPh);
                    command.Parameters.AddWithValue("@soilTexture", soilTexture);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Data inserted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data: " + ex.Message);
            }

            Close();
        }
    }
}