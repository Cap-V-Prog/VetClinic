using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VetClinic_UI
{
    public class MedicActManager
    {
        private MySQLConnectionManager connectionManager;

        public MedicActManager(MySQLConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }
        
        public DataTable SearchMedicActs(int searchBy, string searchText)
        {
            string query = "";

            switch (searchBy)
            {
                case 0: // ID do animal
                    query = $"SELECT AtosMedicos.* FROM AtosMedicos JOIN Animal ON AtosMedicos.id_animal = Animal.id WHERE Animal.id = {searchText}";
                    break;
                case 1: // Nome do dono
                    query = "SELECT * FROM AtosMedicos JOIN Animal ON AtosMedicos.id_animal = Animal.id WHERE Animal.nome_dono LIKE @searchText";
                    break;
                case 2: // Nome do animal
                    query = "SELECT * FROM AtosMedicos JOIN Animal ON AtosMedicos.id_animal = Animal.id WHERE Animal.nome_animal LIKE @searchText";
                    break;
                case 3: // Search all medicacts
                    query = "SELECT * FROM AtosMedicos";
                    break;
                default:
                    throw new ArgumentException("Opção de pesquisa inválida");
            }

            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (searchBy != 3 && searchBy != 0) // Skip parameter assignment for search all option
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
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dataTable;
        }
        
        public void AddMedicAct(int animalId, string atoMedico, string descAtoMedico, decimal custoUnitario)
        {
            string query = "INSERT INTO AtosMedicos (id_animal, ato_medico, descricao_ato_medico, custo_unitario, data_insercao, estado) VALUES (@animalId, @atoMedico, @descAtoMedico, @custoUnitario, CURDATE(), 'ativo')";
            
            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@animalId", animalId);
                    command.Parameters.AddWithValue("@atoMedico", atoMedico);
                    command.Parameters.AddWithValue("@descAtoMedico", descAtoMedico);
                    command.Parameters.AddWithValue("@custoUnitario", custoUnitario);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        
        public void UpdateAnimal(int id, int animalId, string atoMedico, string descAtoMedico, decimal custoUnitario)
        {
            string query = "UPDATE AtosMedicos SET id_animal = @animalId, ato_medico = @atoMedico,descricao_ato_medico = @descAtoMedico, custo_unitario = @custoUnitario, data_ultima_alteracao = CURDATE() WHERE id_ato_medico = @id";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@animalId", animalId);
                    command.Parameters.AddWithValue("@atoMedico", atoMedico);
                    command.Parameters.AddWithValue("@descAtoMedico", descAtoMedico);
                    command.Parameters.AddWithValue("@custoUnitario", custoUnitario);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}