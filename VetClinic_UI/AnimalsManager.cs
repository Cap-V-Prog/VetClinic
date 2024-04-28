using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VetClinic_UI
{
    public class AnimalManager
    {
        private MySQLConnectionManager _connectionManager;

        public AnimalManager(MySQLConnectionManager connectionManager)
        {
           _connectionManager = connectionManager;
        }

        public DataTable SearchAnimals(int searchBy, string searchText)
        {
            string query = "";

            switch (searchBy)
            {
                case 0: // ID do animal
                    query = $"SELECT * FROM Animal WHERE id = {searchText}";
                    break;
                case 1: // Nome do dono
                    query = "SELECT * FROM Animal WHERE nome_dono LIKE @searchText";
                    break;
                case 2: // Contacto do dono
                    query = "SELECT * FROM Animal WHERE contato_dono LIKE @searchText";
                    break;
                case 3: // Tipo de animal
                    query = "SELECT * FROM Animal WHERE tipo_animal LIKE @searchText";
                    break;
                case 4: // Search all animals
                    query = "SELECT * FROM Animal";
                    break;
                default:
                    throw new ArgumentException("Opção de pesquisa inválida");
            }

            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = _connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (searchBy != 4 && searchBy != 0) // Skip parameter assignment for search all option
                    {
                        command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                    }

                    try
                    {
                        connection.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(@"Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dataTable;
        }
        
        public void AddAnimal(string nomeAnimal, string nomeDono, string contatoDono, DateTime dataNascimento, DateTime dataUltimaConsulta, string tipoAnimal, string raca, string sexo, decimal peso)
        {
            string query = "INSERT INTO Animal (nome_animal,nome_dono, contato_dono, data_nascimento, data_ultima_consulta, tipo_animal, raca, sexo, peso, data_registro, estado) " +
                           "VALUES (@nomeAnimal,@nomeDono, @contatoDono, @dataNascimento, @dataUltimaConsulta, @tipoAnimal, @raca, @sexo, @peso, CURRENT_TIMESTAMP, 'ativo')";

            using (MySqlConnection connection = _connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nomeAnimal", nomeAnimal);
                    command.Parameters.AddWithValue("@nomeDono", nomeDono);
                    command.Parameters.AddWithValue("@contatoDono", contatoDono);
                    command.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                    command.Parameters.AddWithValue("@dataUltimaConsulta", dataUltimaConsulta);
                    command.Parameters.AddWithValue("@tipoAnimal", tipoAnimal);
                    command.Parameters.AddWithValue("@raca", raca);
                    command.Parameters.AddWithValue("@sexo", sexo);
                    command.Parameters.AddWithValue("@peso", peso);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        
        public void UpdateAnimal(int id,string nomeAnimal, string nomeDono, string contatoDono, DateTime dataNascimento, DateTime dataUltimaConsulta, string tipoAnimal, string raca, string sexo, decimal peso)
        {
            string query = "UPDATE Animal SET nome_animal = @nomeAnimal, nome_dono = @nomeDono, contato_dono = @contatoDono, data_nascimento = @dataNascimento, " +
                           "data_ultima_consulta = @dataUltimaConsulta, tipo_animal = @tipoAnimal, raca = @raca, sexo = @sexo, " +
                           "peso = @peso WHERE id = @id";

            using (MySqlConnection connection = _connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nomeAnimal", nomeAnimal);
                    command.Parameters.AddWithValue("@nomeDono", nomeDono);
                    command.Parameters.AddWithValue("@contatoDono", contatoDono);
                    command.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                    command.Parameters.AddWithValue("@dataUltimaConsulta", dataUltimaConsulta);
                    command.Parameters.AddWithValue("@tipoAnimal", tipoAnimal);
                    command.Parameters.AddWithValue("@raca", raca);
                    command.Parameters.AddWithValue("@sexo", sexo);
                    command.Parameters.AddWithValue("@peso", peso);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(@"Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        
        public void DeactivateAnimal(int animalId)
        {
            string query = "UPDATE Animal SET estado = 'inativo' WHERE id = @animalId";

            using (MySqlConnection connection = _connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@animalId", animalId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(@"Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        
        // Method to retrieve owner name based on animal ID
        public string GetOwnerName(int animalId)
        {
            string ownerName = ""; // Initialize the variable to store the owner name

            // Write the SQL query to retrieve the owner name based on the animal ID
            string query = "SELECT nome_dono FROM Animal WHERE id = @AnimalID";

            // Create a MySqlConnection object and a MySqlCommand object
            using (MySqlConnection connection = _connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add parameter to the MySqlCommand to prevent SQL injection
                    command.Parameters.AddWithValue("@AnimalID", animalId);

                    // Open the connection
                    connection.Open();

                    // Execute the query to retrieve the owner name
                    object result = command.ExecuteScalar();

                    // Check if the result is not null
                    if (result != null)
                    {
                        // Convert the result to string and assign it to the ownerName variable
                        ownerName = result.ToString();
                    }
                }
            }
            
            _connectionManager.CloseConnection();
            // Return the retrieved owner name
            return ownerName;
        }
        
        // Method to retrieve animal name based on animal ID
        public string GetAnimalName(int animalId)
        {
            string animalName = ""; // Initialize the variable to store the animal name

            // Write the SQL query to retrieve the animal name based on the animal ID
            string query = "SELECT nome_animal FROM Animal WHERE id = @AnimalID";

            // Create a MySqlConnection object and a MySqlCommand object
            using (MySqlConnection connection = _connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add parameter to the MySqlCommand to prevent SQL injection
                    command.Parameters.AddWithValue("@AnimalID", animalId);

                    // Open the connection
                    connection.Open();

                    // Execute the query to retrieve the animal name
                    object result = command.ExecuteScalar();

                    // Check if the result is not null
                    if (result != null)
                    {
                        // Convert the result to string and assign it to the animalName variable
                        animalName = result.ToString();
                    }
                }
            }
            
            _connectionManager.CloseConnection();
            // Return the retrieved animal name
            return animalName;
        }
    }
}