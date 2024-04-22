using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VetClinic_UI
{
    public class AnimalManager
    {
        private MySQLConnectionManager connectionManager;

        public AnimalManager(MySQLConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
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

            using (MySqlConnection connection = connectionManager.GetConnection())
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
                        MessageBox.Show("Erro: " + e);
                    }
                }
            }

            return dataTable;
        }
        
        public void AddAnimal(string nomeDono, string contatoDono, DateTime dataNascimento, DateTime dataUltimaConsulta, string tipoAnimal, string raca, string sexo, decimal peso)
        {
            string query = "INSERT INTO Animal (nome_dono, contato_dono, data_nascimento, data_ultima_consulta, tipo_animal, raca, sexo, peso, data_registro, estado) " +
                           "VALUES (@nomeDono, @contatoDono, @dataNascimento, @dataUltimaConsulta, @tipoAnimal, @raca, @sexo, @peso, CURRENT_TIMESTAMP, 'ativo')";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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
                }
            }
        }
    }
}